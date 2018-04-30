using System.Text.RegularExpressions;
using AngleSharp.Dom;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class LessonTypeHtmlConverter : IModelHtmlConverter<LessonType> {
        public LessonType Convert(IElement element) {
            var type = new LessonType() {
                Name = Regex.Match(element.InnerHtml, @"\w+").Value
            };

            return type;
        }
    }
}