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

        public async Task<List<Department>> GetDepartments(Faculty faculty) {
            var formData = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("who", "2"),
                new KeyValuePair<string, string>("request", "kafedra"),
                new KeyValuePair<string, string>("filial", "1"),
                new KeyValuePair<string, string>("facult", faculty.Code)
            });
            HttpResponseMessage response = await DefaultSend(formData);

            var data = JObject.Parse(await GetContent(response, DefaultEncoding))["list"];
            var departments = new List<Department>();

            foreach (var token in data) {
                var department = converter.Convert(token);

                department.Faculty = faculty;
                department.Tutors = new List<Tutor>();

                departments.Add(department);
            }

            return departments;
        }
    }
}