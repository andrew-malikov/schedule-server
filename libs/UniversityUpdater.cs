using ScheduleServer.Models;
using ScheduleServer.Clients;

namespace ScheduleServer.Libs {
    public abstract class UniversityUpdater : IFullUpdatable {
        protected UniversityContext context;
        protected OsuApi osuApi;

        public UniversityUpdater(UniversityContext context, OsuApi osuApi) {
            this.context = context;
            this.osuApi = osuApi;
        }

        public async void FullUpdate() {
            Clear();

            context.Groups.AddRange(await osuApi.GetRelatedGroups());
            context.Tutors.AddRange(await osuApi.GetRelatedTutors());

            context.SaveChanges();

            osuApi.ResetFaculties();
        }


        protected abstract void Clear();
    }
}