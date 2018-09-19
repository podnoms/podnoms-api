﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PodNoms.Common.Utils {
    public static class HttpUtils {
        public static async Task<string> DownloadFile(string url, string file = "") {
            if (string.IsNullOrEmpty(file))
                file = System.IO.Path.GetTempFileName();

            using (var client = new HttpClient()) {
                using (var response = await client.GetAsync(url)) {
                    using (var content = response.Content) {
                        var result = await content.ReadAsByteArrayAsync();
                        System.IO.File.WriteAllBytes(file, result);
                    }
                }
            }
            return file;
        }

        internal static HttpClientHandler GetFiddlerProxy() {
            var handler = new HttpClientHandler {
                Proxy = new WebProxy("localhost", 8888),
                UseProxy = true
            };
            return handler;
        }

        public static string UrlCombine(string url1, string url2) {
            if (url1.Length == 0) {
                return url2;
            }

            if (url2.Length == 0) {
                return url1;
            }

            url1 = url1.TrimEnd('/', '\\');
            url2 = url2.TrimStart('/', '\\');

            return string.Format("{0}/{1}", url1, url2);
        }
    }
}