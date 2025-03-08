//using EfCoreExample;
//using Microsoft.Data.SqlClient;
//using System.Reflection;
//using System.Runtime.ExceptionServices;
//using System.Text.RegularExpressions;

//InsertStudent(connectionString, new Student
//{
//    FirstName = null, 
//    LastName = null,
//    Birthday = new DateTime(1995,12,3),
//    GroupId = 1,
//    Course = 3
//});
//InsertGroup(connectionString, new Group { Title = "P34" });

//static void InsertStudent(string connectionString, Student student)
//{
//    //INSERT
//    try
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            Console.WriteLine("Connection open");

//            string query = $"" +
//                $"INSERT INTO Students (FirstName,LastName, Birthday, Course, GroupId) " +
//                $"VALUES ('{student.FirstName}', '{student.LastName}', '{student.Birthday.ToString("yyyy-MM-dd")}', {student.Course}, {student.GroupId})";

//            using (SqlCommand command = new SqlCommand(query, connection))
//            {
//                command.ExecuteNonQuery();
//            }
//        }

//    }
//    catch (Exception e)
//    {
//        Console.WriteLine(e.Message);
//    }

//}

//static void InsertGroup(string connectionString, Group group)
//{
//    //INSERT
//    try
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            Console.WriteLine("Connection open");

//            string query = $"" +
//                $"INSERT INTO Groups (Title) " +
//                $"VALUES ('{group.GroupTitle}')";

//            using (SqlCommand command = new SqlCommand(query, connection))
//            {
//                command.ExecuteNonQuery();
//            }
//        }

//    }
//    catch (Exception e)
//    {
//        Console.WriteLine(e.Message);
//    }

//}

//static List<Student> SelectAllStudents(string connectionString)
//{
//    List<Student> students = [];
//    // SELECT
//    try
//    {
//        using (SqlConnection connection = new SqlConnection(connectionString))
//        {
//            connection.Open();
//            string query = "SELECT Id,FirstName,LastName,Birthday,Course,GroupId FROM Students";

//            using (SqlCommand command = new SqlCommand(query, connection))
//            {
//                using (SqlDataReader reader = command.ExecuteReader())
//                {


//                    while (reader.Read())
//                    {
//                        Student student = new();
//                        student.Id = reader.GetInt32(0);
//                        student.FirstName = reader.GetString(1);
//                        student.LastName = reader.GetString(2);
//                        student.Birthday = reader.GetDateTime(3);
//                        student.Course = reader.GetInt32(4);
//                        student.GroupId = reader.GetInt32(5);
//                        students.Add(student);
//                    }
//                }
//            }
//        }

//    }
//    catch (Exception e)
//    {
//        Console.WriteLine(e.Message);
//    }
//    return students;
//}


//string connectionString = "Data Source=DESKTOP-MTDDLEC;Initial Catalog=Database1;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Command Timeout=0";
////InsertStudent(connectionString, new Student
////{
////    FirstName = "",
////    LastName = "",
////    Birthday = new DateTime(1995, 12, 3),
////    GroupId = 1,
////    Course = 3
////}
////    );
////InsertGroup(connectionString, new Group { Title = "P34" });



//var students = SelectAllStudents(connectionString);
//students.ForEach(x => Console.WriteLine($"{x.Id} {x.LastName} {x.FirstName} {x.Birthday.ToString("dd.MM.yyyy")} curs:{x.Course} gr:{x.GroupId}"));





using EfCoreExample;

string connectionString = "Data Source=DESKTOP-MTDDLEC;Initial Catalog=Database1;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Command Timeout=0";

/*
Scaffold-DbContext 'Data Source=DESKTOP-MTDDLEC;Initial Catalog=Database1;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Command Timeout=0' Microsoft.EntityFrameworkCore.SqlServer-project EfCoreExample

 */
//using (var context = new Database1Context())
//{
//    var students = context.Students.ToList();
//    students.ForEach(x => Console.WriteLine($"{x.Id} {x.LastName} {x.FirstName} {x.Birthday} curs:{x.Course} {x.GroupId}"));
//}

using(var context = new Database1Context())
{
    var student = new Student
    {
        FirstName = "Ivan",
        LastName = "Ivanov",
        Birthday = new DateOnly(1995, 12, 3),
        Course = 1,
        GroupId = 3
    };
    context.Students.Add(student);
    context.SaveChanges();
}