using System;
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
            return Json(context.Tutors.ToList(), new JsonSerializerSettings() {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            Tutor tutor;
            try { tutor = context.Tutors.IncludeDependent().Single(t => id == t.Id); }
            catch (ArgumentNullException) { return NotFound(); }
            catch (InvalidOperationException) { return NotFound(); }

            return Json(tutor, new JsonSerializerSettings() {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}