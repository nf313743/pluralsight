using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using CSharpFunctionalExtensions;
using Dapper;
using Logic.Dtos;
using Logic.Students;
using Logic.Utils;

namespace Logic.AppServices
{
    public sealed class GetListQuery : IQuery<List<StudentDto>>
    {
        public string EnrolledIn { get; }

        public int? NumberOfCourses { get; }

        public GetListQuery(string enrolledIn, int? numberOfCourses)
        {
            EnrolledIn = enrolledIn;
            NumberOfCourses = numberOfCourses;
        }

        internal sealed class GetListQueryHandler : IQueryHandler<GetListQuery, List<StudentDto>>
        {
            private readonly ConnectionString _connectionString;

            public GetListQueryHandler(ConnectionString connectionString)
            {
                _connectionString = connectionString;
            }

            public List<StudentDto> Handle(GetListQuery query)
            {
                string sql = @"
                    SELECT s.StudentID Id,
                            s.Name,
                            s.Email,
	                        s.FirstCourseName Course1,
                            s.FirstCourseCredits Course1Credits,
                            s.FirstCourseGrade Course1Grade,
	                        s.SecondCourseName Course2,
                            s.SecondCourseCredits Course2Credits,
                            s.SecondCourseGrade Course2Grade
                    FROM dbo.Student s
                    WHERE (s.FirstCourseName = @Course
		                    OR s.SecondCourseName = @Course
		                    OR @Course IS NULL)
                        AND (s.NumberOfEnrollments = @Number
                            OR @Number IS NULL)
                    ORDER BY s.StudentID ASC";

                using (var connection = new SqlConnection(_connectionString.Value))
                {
                    var students = connection.Query<StudentInDb>(sql, new
                    {
                        Course = query.EnrolledIn,
                        Number = query.NumberOfCourses
                    })
                    .ToList();

                    var ids = students
                                .GroupBy(x => x.StudentId)
                                .Select(x => x.Key)
                                .ToList();

                    var result = new List<StudentDto>();

                    foreach (long id in ids)
                    {
                        var data = students
                                    .Where(x => x.StudentId == id)
                                    .ToList();

                        var dto = new StudentDto
                        {
                            Id = data[0].StudentId,
                            Name = data[0].Name,
                            Email = data[0].Email,
                            Course1 = data[0].CourseName,
                            Course1Credits = data[0].Credits,
                            Course1Grade = data[0]?.Grade.ToString()
                        };

                        if (data.Count > 1)
                        {
                            dto.Course2 = data[1].CourseName;
                            dto.Course2Credits = data[1].Credits;
                            dto.Course2Grade = data[1]?.Grade.ToString();
                        }

                        result.Add(dto);
                    }

                    return result;
                }
            }

            private class StudentInDb
            {
                public StudentInDb(long studentId, string name, string email, Grade? grade, string courseName, int? credits)
                {
                    StudentId = studentId;
                    Name = name;
                    Email = email;
                    Grade = grade;
                    CourseName = courseName;
                    Credits = credits;
                }

                public long StudentId { get; }

                public string Name { get; }

                public string Email { get; }

                public Grade? Grade { get; }

                public string CourseName { get; }

                public int? Credits { get; }
            }
        }
    }
}