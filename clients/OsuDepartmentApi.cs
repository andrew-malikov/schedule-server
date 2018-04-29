using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using ScheduleServer.Converters;
using ScheduleServer.Models;
using ScheduleServer.Configs;

namespace ScheduleServer.Clients {
    public class OsuDepartmentApi : OsuClientApi {
        protected DepartmentJsonConverter converter;

        public OsuDepartmentApi(OsuApiConfig config, DepartmentJsonConverter converter) : base(config) {
            this.converter = converter;
        }

        public async Task<List<Department>> GetCourses(Faculty faculty) {
            var formData = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("who", "2"),
                new KeyValuePair<string, string>("request", "kafedra"),
                new KeyValuePair<string, string>("filial", "1"),
                new KeyValuePair<string, string>("facult", faculty.Code)
            });
            HttpResponseMessage response = await DefaultSend(formData);

            var data = JObject.Parse(await GetContent(response))["list"];
            var departments = new List<Department>();

            foreach (var department in data) {
                departments.Add(converter.Convert(department));
            }

            return departments;
        }
    }
}