using System.Text.RegularExpressions;
using AngleSharp.Dom;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class TimeHtmlConverter : IModelHtmlConverter<Time> {
        public Time Convert(IElement element) {
            var time = new Time() {
                Period = Regex.Match(element.InnerHtml, @"\d{2}:\d{2}-\d{2}:\d{2}").Value,
                Number = int.Parse(Regex.Match(element.InnerHtml, @"\d").Value)
            };

            return time;
        }
    }
}