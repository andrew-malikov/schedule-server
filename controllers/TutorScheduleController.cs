using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using ScheduleServer.Models;
using ScheduleServer.Libs;

namespace ScheduleServer.Controllers {
    [Route("tutors/schedules")]
    public class TutorScheduleController : Controller {
        protected UniversityContext context;
        protected TutorScheduleManager manager;

        public TutorScheduleController(UniversityContext context, TutorScheduleManager manager) {
            this.context = context;
            this.manager = manager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id) {
            var tutor = context.Tutors.Find(id);

            if (tutor is null)
                return NotFound();

            var schedule = await manager.GetSchedule(tutor);

            return Content(schedule.Value);
        }
    }
}