using Microsoft.AspNetCore.Mvc;

using ScheduleServer.Models;
using ScheduleServer.Libs;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;

namespace ScheduleServer.Controllers {
    [Route("groups/schedules")]
    public class GroupScheduleController : Controller {

        protected UniversityContext context;
        protected ScheduleManager manager;

        public GroupScheduleController(UniversityContext context, ScheduleManager manager) {
            this.context = context;
            this.manager = manager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id) {
            var group = context.Groups.Find(id);

            if (group is null)
                return NotFound();

            var schedule = await manager.GetGroupSchedule(group);

            return Json(schedule, new JsonSerializerSettings() {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}