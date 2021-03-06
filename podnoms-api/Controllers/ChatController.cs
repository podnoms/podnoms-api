using System.Threading.Tasks;
using AutoMapper;
using Lib.Net.Http.WebPush;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using PodNoms.Data.Models;
using PodNoms.Common;
using PodNoms.Common.Auth;
using PodNoms.Common.Data.ViewModels.Resources;
using PodNoms.Common.Persistence;
using PodNoms.Common.Persistence.Repositories;
using PodNoms.Common.Services;
using WebPush = Lib.Net.Http.WebPush;
using PodNoms.Common.Data.Settings;
using PodNoms.Common.Services.Hubs;
using PodNoms.Common.Services.Push;
using static PodNoms.Data.Models.Notifications.Notification;
using System.Collections.Generic;

namespace PodNoms.Api.Controllers {
    [Authorize]
    [Route("[controller]")]
    public class ChatController : BaseAuthController {
        private readonly ISupportChatService _supportChatService;
        private readonly IChatRepository _chatRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ChatSettings _chatSettings;
        private readonly AppSettings _appSettings;
        private readonly IPushSubscriptionStore _subscriptionStore;
        private readonly IPushNotificationService _notificationService;
        private readonly HubLifetimeManager<ChatHub> _hub;
        private readonly IMapper _mapper;

        public ChatController(
                IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager,
                ILogger<ChatController> logger, IOptions<ChatSettings> chatSettings, IOptions<AppSettings> appSettings,
                IPushSubscriptionStore subscriptionStore, IPushNotificationService notificationService,
                HubLifetimeManager<ChatHub> hub, IMapper mapper, IUnitOfWork unitOfWork,
                IChatRepository chatRepository, ISupportChatService supportChatService) :
            base(contextAccessor, userManager, logger) {
            _chatRepository = chatRepository;
            _unitOfWork = unitOfWork;
            _chatSettings = chatSettings.Value;
            _appSettings = appSettings.Value;
            _subscriptionStore = subscriptionStore;
            _notificationService = notificationService;
            _hub = hub;
            _mapper = mapper;
            _supportChatService = supportChatService;
        }

        [HttpGet]
        public async Task<ActionResult<ChatViewModel>> Get() {
            var chats = await _chatRepository.GetAllChats(UserId);
            var response = _mapper.Map<ChatViewModel>(chats);
            return Ok(response);
        }
        [HttpGet("getadmin")]
        public async Task<ActionResult<List<ChatViewModel>>> GetAdmin(int take = 10) {
            var chatUser = await _userManager.FindByEmailAsync(_chatSettings.CurrentChatUser);
            if (chatUser != null) {
                var chats = await _chatRepository.GetChats(UserId, chatUser.Id, take);
                var response = _mapper.Map<List<ChatViewModel>>(chats);
                return Ok(response);
            }
            return StatusCode(503);
        }
        [AllowAnonymous]
        [HttpPost("initialise")]
        public async Task<ActionResult<ChatViewModel>> Initialise([FromBody]ChatViewModel message) {
            var chatUser = await _userManager.FindByEmailAsync(_chatSettings.CurrentChatUser);
            if (chatUser != null) {
                var chat = _mapper.Map<ChatMessage>(message);
                chat.FromUser = _applicationUser;
                chat.ToUser = chatUser;
                if (chat.FromUser != null) {
                    _chatRepository.AddOrUpdate(chat);
                    await _unitOfWork.CompleteAsync();
                } else {
                    // it's an anonymous chat, we don't need to save item
                    chat.Id = System.Guid.NewGuid();
                }

                var filledMessage = _mapper.Map<ChatViewModel>(chat);
                filledMessage.FromUserName = _applicationUser == null ?
                        message.FromUserName :
                        _applicationUser.GetBestGuessName();

                //send push message to whoever is registered as admin
                await _subscriptionStore.ForEachSubscriptionAsync(chatUser.Id, (PushSubscription subscription) => {
                    _notificationService.SendNotificationAsync(
                        subscription,
                        new PushMessage("New SUPPORT Request") {
                            Urgency = PushMessageUrgency.High,
                            Topic = "NewSupport"
                        },
                        _appSettings.SiteUrl);
                });
                if (await _supportChatService.InitiateSupportRequest(filledMessage)) {
                    await _hub.SendUserAsync(filledMessage.ToUserId, "support-message", new object[] { filledMessage });
                    return Ok(filledMessage);
                }
            }
            return StatusCode(503);
        }

        [HttpPost()]
        public async Task<ActionResult<ChatViewModel>> Post([FromBody]ChatViewModel message) {

            var chat = _mapper.Map<ChatMessage>(message);
            chat.FromUser = _applicationUser;
            chat.ToUser = await _userManager.FindByIdAsync(message.ToUserId);

            _chatRepository.AddOrUpdate(chat);
            await _unitOfWork.CompleteAsync();

            var filledMessage = _mapper.Map<ChatViewModel>(chat);
            filledMessage.FromUserName = _applicationUser.GetBestGuessName();

            await _hub.SendUserAsync(filledMessage.ToUserId, "support-message", new object[] { filledMessage });

            return Accepted(filledMessage);
        }
    }
}
