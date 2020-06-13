using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PodNoms.Common.Services.PageParser {
    public interface IPageParser {
        Task<bool> Initialise(string url);
        string GetPageTitle();
        string GetHeadTag(string tagName);
        Task<Dictionary<string, string>> GetAllAudioLinks();
    }
}
