using AngleSharp.Dom;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class RoomHtmlConverter : IModelHtmlConverter<Room> {
        public Room Convert(IElement element) {
            var room = new Room() {
                Number = element.InnerHtml,
            };

            return room;
        }
    }
}