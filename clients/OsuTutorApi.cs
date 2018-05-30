using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using AngleSharp.Parser.Html;

using ScheduleServer.Converters;
using ScheduleServer.Models;
using ScheduleServer.Configs;

namespace ScheduleServer.Clients {
    public class OsuTutorApi : OsuClientApi {
        protected TutorJsonConverter tutorConverter;
        protected ScheduleHtmlConverter<TutorLessonHmtlConverter> scheduleConverter;

        public OsuTutorApi(OsuApiConfig config, TutorJsonConverter tutorConverter, ScheduleHtmlConverter<TutorLessonHmtlConverter> scheduleConverter) : base(config) {
            this.tutorConverter = tutorConverter;
            this.scheduleConverter = scheduleConverter;
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
                var tutor = tutorConverter.Convert(rawTutor);

                tutor.Department = department;

                tutors.Add(tutor);
            }

            return tutors;
        }

        public async Task<Schedule> GetSchedule(Tutor tutor) {
            var formData = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("request", "rasp"),
                new KeyValuePair<string, string>("what", "1"),
                new KeyValuePair<string, string>("who", "2"),
                new KeyValuePair<string, string>("mode", "full"),
                new KeyValuePair<string, string>("filial", "1"),
                new KeyValuePair<string, string>("prep", tutor.Code)
            });
            HttpResponseMessage response = await DefaultSend(config.ScheduleUri, formData, HttpMethod.Post);

            var responseData = await GetContent(response, DefaultEncoding);
            var document = new HtmlParser().Parse(responseData);
            var htmlTable = document.QuerySelector("tbody");
            var schedule = scheduleConverter.Convert(htmlTable);

            return schedule;
        }
    }
}