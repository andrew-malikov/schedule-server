using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using ScheduleServer.Models;
using ScheduleServer.Converters;
using ScheduleServer.Configs;

namespace ScheduleServer.Clients {
    public class OsuFacultyApi : OsuClientApi {
        protected FacultyJsonConverter converter;

        public OsuFacultyApi(OsuApiConfig config, FacultyJsonConverter converter) : base(config) {
            this.converter = converter;
        }

        public async Task<List<Faculty>> GetFaculties() {
            var formData = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("request", "facult"),
                new KeyValuePair<string, string>("filial", "1")
            });
            HttpResponseMessage response = await DefaultSend(formData);

            var data = JObject.Parse(await GetContent(response))["list"];
            var faculties = new List<Faculty>();

            foreach (var faculty in data) {
                faculties.Add(converter.Convert(faculty));
            }

            return faculties;
        }
    }
}