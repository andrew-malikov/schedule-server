using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using ScheduleServer.Models;

namespace ScheduleServer.Controllers {
    [Route("tutors")]
    public class TutorController : Controller {
        protected UniversityContext context;

        public TutorController(UniversityContext context) {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get() {
            return Json(context.Tutors.IncludeDependent().ToList(), new JsonSerializerSettings() {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            var tutor = context.Tutors.IncludeDependent().Find(id);

            if (tutor is null) return NotFound();

            return Json(tutor, new JsonSerializerSettings() {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}