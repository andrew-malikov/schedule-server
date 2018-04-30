using System.Collections.Generic;
using AngleSharp.Dom;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class TutorScheduleHtmlConverter : IModelHtmlConverter<TutorSchedule> {
        protected TimeHtmlConverter timeConverter;
        protected DayHtmlConveter<TutorLessonHmtlConverter> dayConverter;

        public TutorScheduleHtmlConverter(TimeHtmlConverter timeConverter, DayHtmlConveter<TutorLessonHmtlConverter> dayConverter) {
            this.timeConverter = timeConverter;
            this.dayConverter = dayConverter;
        }

        public TutorSchedule Convert(IElement element) {
            var schedule = new TutorSchedule() {
                Days = new List<Day>()
            };
            var periods = GetPeriods(element.QuerySelector("#tableheader"));

            foreach (var item in element.Children) {
                if (item.QuerySelectorAll("td[pare_id]").Length == 0)
                    continue;

                var day = dayConverter.Convert(item);
                schedule.Days.Add(BindTimeToLessons(day, periods));
            }

            return schedule;
        }

        protected List<Time> GetPeriods(IElement element) {
            var periods = new List<Time>();

            foreach (var item in element.QuerySelectorAll(".timezao")) {
                periods.Add(timeConverter.Convert(item));
            }

            return periods;
        }

        protected Day BindTimeToLessons(Day day, List<Time> periods) {
            foreach (var lesson in day.Lessons) {
                lesson.Time.Period = periods[lesson.Time.Number - periods[0].Number].Period;
            }

            return day;
        }
    }
}