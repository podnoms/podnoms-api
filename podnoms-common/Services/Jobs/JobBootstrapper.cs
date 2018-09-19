using System;
using Hangfire;

namespace PodNoms.Common.Services.Jobs {
    public static class JobBootstrapper {
        public static void BootstrapJobs() {

            RecurringJob.AddOrUpdate<DeleteOrphanAudioJob>(x => x.Execute(), Cron.Daily(1));
            RecurringJob.AddOrUpdate<UpdateYouTubeDlJob>(x => x.Execute(), Cron.Daily(1, 30));
            RecurringJob.AddOrUpdate<ProcessPlaylistsJob>(x => x.Execute(), Cron.Daily(2));
            RecurringJob.AddOrUpdate<ProcessFailedPodcastsJob>(x => x.Execute(), Cron.Daily(2, 30));
            RecurringJob.AddOrUpdate<DeleteOrphanAudioJob>(x => x.Execute(), Cron.Daily(3));

            RecurringJob.AddOrUpdate<ProcessRemoteAudioFileAttributesJob>(
                x => x.Execute(),
                Cron.Hourly());
        }
    }
}