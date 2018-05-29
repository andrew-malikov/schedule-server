using ScheduleServer.Models;
using ScheduleServer.Clients;

namespace ScheduleServer.Libs {
    public abstract class UniversityUpdate : IUpdatable {
        protected UniversityContext context;
        protected OsuApi osuApi;

        public UniversityUpdate(UniversityContext context, OsuApi osuApi) {
            this.context = context;
            this.osuApi = osuApi;
        }

        public async void Update() {
            Clear();

            context.AddRange(await osuApi.GetFaculties());

            context.SaveChanges();
        }


        protected abstract void Clear();
    }
}