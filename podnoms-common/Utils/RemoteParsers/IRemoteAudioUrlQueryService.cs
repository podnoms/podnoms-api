using System.Collections.Generic;
using System.Threading.Tasks;

namespace PodNoms.Common.Utils.RemoteParsers {
    public interface IRemoteAudioUrlQueryService {
        RemoteUrlType GetUrlType(string url);
        Task<List<ParsedItemResult>> GetPlaylistItems(string url, int count = 10);
        bool ValidateUrl(string url);
    }
    public interface IYouTubeParser : IRemoteAudioUrlQueryService {
        Task<string> GetChannelId(string channelName);
        string GetChannelName(string url);
    }
    public interface IMixCloudParser : IRemoteAudioUrlQueryService {

    }
}
