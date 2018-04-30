using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using AngleSharp.Dom;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class DayHtmlConveter<T> : IModelHtmlConverter<Day> where T : IModelHtmlConverter<Lesson> {

        protected T lessonConverter;

        public DayHtmlConveter(T lessonConverter) {
            this.lessonConverter = lessonConverter;
        }

        public Day Convert(IElement element) {
            var date = Regex.Match(element.InnerHtml, @"\d{2}[.]\d{2}[.]\d{4}").Value;

            var day = new Day() {
                Date = DateTime.ParseExact(date, "dd.mm.yyyy", CultureInfo.InvariantCulture),
                Lessons = new List<Lesson>()
            };

            foreach (var item in element.QuerySelectorAll("td[pare_id]")) {
                day.Lessons.Add(lessonConverter.Convert(item));
            }

            return day;
        }
    }
}