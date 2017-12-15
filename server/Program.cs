﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PodNoms.Api {
    public class Program {
        public static void Main (string[] args) {
            BuildWebHost (args).Run ();
        }
        public static IWebHost BuildWebHost (string[] args) =>
            WebHost.CreateDefaultBuilder (args)
            .UseStartup<Startup> ()
            .UseKestrel (options => {
                options.Listen (IPAddress.Any, 5000, listenOptions =>
                    listenOptions.UseHttps ("localhost.pfx"));
                options.Limits.MaxRequestBodySize = 1073741824;
            })
            .Build ();
    }
}