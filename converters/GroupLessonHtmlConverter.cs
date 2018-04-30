using System.Collections.Generic;
using AngleSharp.Dom;

using ScheduleServer.Models;

namespace ScheduleServer.Converters {
    public class GroupLessonHmtlConverter : IModelHtmlConverter<Lesson> {
        protected DisciplineHtmlConverter disciplineConverter;
        protected LessonTypeHtmlConverter typeConverter;
        protected TutorHtmlConverter tutorConverter;
        protected RoomHtmlConverter roomConverter;


        public GroupLessonHmtlConverter(DisciplineHtmlConverter disciplineConverter, LessonTypeHtmlConverter typeConverter, TutorHtmlConverter tutorConverter, RoomHtmlConverter roomConverter) {
            this.disciplineConverter = disciplineConverter;
            this.typeConverter = typeConverter;
            this.tutorConverter = tutorConverter;
            this.roomConverter = roomConverter;
        }

        public Lesson Convert(IElement element) {
            var lesson = new Lesson() {
                Time = new Time() { Number = int.Parse(element.GetAttribute("pare_id")) },
                Discipline = disciplineConverter.Convert(element.QuerySelector(".dis")),
                Type = typeConverter.Convert(element.QuerySelector(".lestype")),
                Tutors = new List<Tutor>(),
                Rooms = new List<Room>()
            };

            foreach (var item in element.QuerySelectorAll(".p")) {
                lesson.Tutors.Add(tutorConverter.Convert(item));
            }

            foreach (var item in element.QuerySelectorAll(".aud")) {
                lesson.Rooms.Add(roomConverter.Convert(item));
            }

            return lesson;
        }
    }
}