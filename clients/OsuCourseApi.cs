using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using ScheduleServer.Models;
using ScheduleServer.Converters;
using ScheduleServer.Configs;

namespace ScheduleServer.Clients {
    public class OsuCourseApi : OsuClientApi {
        protected CourseJsonConverter converter;
        public OsuCourseApi(OsuApiConfig config, CourseJsonConverter converter) : base(config) {
            this.converter = converter;
        }

        public async Task<List<Course>> GetCourses(Faculty faculty) {
            var formData = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("request", "potok"),
                new KeyValuePair<string, string>("filial", "1"),
                new KeyValuePair<string, string>("facult", faculty.Code)
            });
            HttpResponseMessage response = await DefaultSend(formData);

            var data = JObject.Parse(await GetContent(response, DefaultEncoding))["list"];
            var courses = new List<Course>();

            foreach (var token in data) {
                var course = converter.Convert(token);
                course.Groups = new List<Group>();
                courses.Add(course);
            }

            return courses;
        }
    }
}