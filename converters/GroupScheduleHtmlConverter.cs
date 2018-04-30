using System.Collections.Generic;
using AngleSharp.Dom;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class GroupScheduleHtmlConverter : IModelHtmlConverter<GroupSchedule> {
        protected TimeHtmlConverter timeConverter;
        protected DayHtmlConveter<GroupLessonHmtlConverter> dayConverter;

        public GroupScheduleHtmlConverter(TimeHtmlConverter timeConverter, DayHtmlConveter<GroupLessonHmtlConverter> dayConverter) {
            this.timeConverter = timeConverter;
            this.dayConverter = dayConverter;
        }

        public GroupSchedule Convert(IElement element) {
            var schedule = new GroupSchedule() {
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