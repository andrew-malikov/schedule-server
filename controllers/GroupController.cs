using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            Group group;
            try { group = context.Groups.IncludeDependent().Single(g => g.Id == id); }
            catch (ArgumentNullException) { return NotFound(); }
            catch (InvalidOperationException) { return NotFound(); }

            return Json(group, new JsonSerializerSettings() {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}