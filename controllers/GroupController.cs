using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using ScheduleServer.Models;

namespace ScheduleServer.Controllers {
    [Route("groups")]
    public class GroupController : Controller {
        protected UniversityContext context;

        public GroupController(UniversityContext context) {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get() {
            return Json(context.Groups.ToList(), new JsonSerializerSettings() {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            var group = context.Groups.Find(id);

            if (group is null) return NotFound();

            return Json(group, new JsonSerializerSettings() {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}