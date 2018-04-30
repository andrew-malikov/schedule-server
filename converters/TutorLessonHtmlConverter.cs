using System.Collections.Generic;
using AngleSharp.Dom;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class TutorLessonHmtlConverter : IModelHtmlConverter<Lesson> {
        protected DisciplineHtmlConverter disciplineConverter;
        protected LessonTypeHtmlConverter typeConverter;
        protected GroupHtmlConverter groupConverter;
        protected RoomHtmlConverter roomConverter;


        public TutorLessonHmtlConverter(DisciplineHtmlConverter disciplineConverter, LessonTypeHtmlConverter typeConverter, GroupHtmlConverter groupConverter, RoomHtmlConverter roomConverter) {
            this.disciplineConverter = disciplineConverter;
            this.typeConverter = typeConverter;
            this.groupConverter = groupConverter;
            this.roomConverter = roomConverter;
        }

        public Lesson Convert(IElement element) {
            var lesson = new Lesson() {
                Time = new Time() { Number = int.Parse(element.GetAttribute("pare_id")) },
                Discipline = disciplineConverter.Convert(element.QuerySelector(".dis")),
                Type = typeConverter.Convert(element.QuerySelector(".lestype")),
                Groups = new List<Group>(),
                Rooms = new List<Room>()
            };

            foreach (var item in element.QuerySelectorAll(".p")) {
                lesson.Groups.Add(groupConverter.Convert(item));
            }

            foreach (var item in element.QuerySelectorAll(".aud")) {
                lesson.Rooms.Add(roomConverter.Convert(item));
            }

            return lesson;
        }
    }
}