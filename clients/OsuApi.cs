using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

using ScheduleServer.Models;
using Microsoft.AspNetCore.Http;

namespace ScheduleServer.Clients {
    public class OsuApi {
        protected List<Faculty> faculties;

        protected OsuFacultyApi facultyApi;
        protected OsuCourseApi courseApi;
        protected OsuGroupApi groupApi;
        protected OsuDepartmentApi departmentApi;
        protected OsuTutorApi tutorApi;

        public OsuApi(OsuFacultyApi facultyApi, OsuCourseApi courseApi, OsuGroupApi groupApi, OsuDepartmentApi departmentApi, OsuTutorApi tutorApi) {
            this.facultyApi = facultyApi;
            this.courseApi = courseApi;
            this.groupApi = groupApi;
            this.departmentApi = departmentApi;
            this.tutorApi = tutorApi;
        }

        public async Task<List<Group>> GetRelatedGroups() {
            var allCourses = new List<Course>();
            var faculties = new Dictionary<Faculty, int>();

            foreach (var faculty in await GetFaculties()) {
                var courses = await courseApi.GetCourses(faculty);

                if (allCourses.Count < courses.Count) allCourses = courses;
                faculties.Add(faculty, courses.Count);
            }

            return await GetRelatedGroups(faculties, allCourses);
        }

        protected async Task<List<Group>> GetRelatedGroups(Dictionary<Faculty, int> faculties, List<Course> courses) {
            var allGroups = new List<Group>();

            foreach (var pair in faculties) {
                for (int i = 0; i < pair.Value; i++)
                    allGroups.AddRange(await groupApi.GetGroups(pair.Key, courses[i]));
            }

            return allGroups;
        }

        public async Task<List<Tutor>> GetRelatedTutors() {
            var allTutors = new List<Tutor>();

            foreach (var faculty in await GetFaculties())
                foreach (var department in await departmentApi.GetDepartments(faculty))
                    allTutors.AddRange(await tutorApi.GetTutors(department));

            return allTutors;
        }

        protected async Task<List<Faculty>> GetFaculties() {
            if (faculties is null) faculties = await facultyApi.GetFaculties();
            return faculties;
        }

        public void ResetFaculties() {
            faculties = null;
        }
    }
}