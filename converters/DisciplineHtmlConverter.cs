using System.Text.RegularExpressions;
using AngleSharp.Dom;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class DisciplineHtmlConverter : IModelHtmlConverter<Discipline> {
        public Discipline Convert(IElement element) {
            var discipline = new Discipline() {
                Title = element.Attributes["title"].Value,
                Name = Regex.Match(element.InnerHtml, @"[А-я]+.?[А-я .]+").Value
            };

            return discipline;
        }
    }
}