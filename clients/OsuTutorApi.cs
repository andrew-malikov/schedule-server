using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

using ScheduleServer.Converters;
using ScheduleServer.Models;
using ScheduleServer.Configs;

namespace ScheduleServer.Clients {
    public class OsuTutorApi : OsuClientApi {
        protected TutorJsonConverter converter;

        public OsuTutorApi(OsuApiConfig config, TutorJsonConverter converter) : base(config) {
            this.converter = converter;
        }

        public async Task<List<Tutor>> GetTutors(Department department) {
            var formData = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("who", "2"),
                new KeyValuePair<string, string>("request", "prep"),
                new KeyValuePair<string, string>("filial", "1"),
                new KeyValuePair<string, string>("facult", department.Faculty.Code),
                new KeyValuePair<string, string>("kafedra", department.Code)
            });
            HttpResponseMessage response = await DefaultSend(formData);

            var data = JObject.Parse(await GetContent(response, DefaultEncoding))["list"];
            var tutors = new List<Tutor>();

            foreach (var rawTutor in data) {
                var tutor = converter.Convert(rawTutor); 
                
                tutor.Department = department;
                
                tutors.Add(tutor);
            }

            return tutors;
        }
    }
}