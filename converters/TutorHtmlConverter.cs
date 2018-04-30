using AngleSharp.Dom;

using ScheduleServer.Models;

namespace ScheduleServer.Converters
{
    public class TutorHtmlConverter : IModelHtmlConverter<Tutor> {
        public Tutor Convert(IElement element) {
            var tutor = new Tutor(){
                ShortName = element.InnerHtml
            };

            return tutor;
        }
    }
}