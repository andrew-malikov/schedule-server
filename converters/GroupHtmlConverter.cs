using AngleSharp.Dom;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class GroupHtmlConverter : IModelHtmlConverter<Group> {
        public Group Convert(IElement element) {
            var group = new Group() {
                Name = element.InnerHtml
            };

            return group;
        }
    }
}