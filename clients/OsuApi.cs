using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

using ScheduleServer.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace ScheduleServer.Clients {
    public class OsuApi {

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

        protected void AddCourses(List<Faculty> faculties) {
            var allCourses = new List<Course>();
            var facultyCourses = new Dictionary<Faculty, int>();

            foreach (var faculty in faculties) {
                var courses = courseApi.GetCourses(faculty).Result;

                if (allCourses.Count < courses.Count) allCourses = courses;
                facultyCourses.Add(faculty, courses.Count);
            }

            AddGroups(facultyCourses, allCourses);
        }

        protected void AddGroups(Dictionary<Faculty, int> faculties, List<Course> courses) {
            foreach (var pair in faculties) {
                for (int i = 0; i < pair.Value; i++)
                    pair.Key.Groups.AddRange(groupApi.GetGroups(pair.Key, courses[i]).Result);
            }
        }

        protected void AddDepartments(List<Faculty> faculties) {
            foreach (var faculty in faculties) {
                var departments = departmentApi.GetDepartments(faculty).Result;
                faculty.Departments.AddRange(departments);
                AddTutors(departments);
            }
        }

        protected void AddTutors(List<Department> departments) {
            foreach (var department in departments)
                department.Tutors.AddRange(tutorApi.GetTutors(department).Result);
        }

        public async Task<List<Faculty>> GetFaculties() {
            var faculties = await facultyApi.GetFaculties();

            AddCourses(faculties);
            AddDepartments(faculties);

            return faculties;
        }
    }
}