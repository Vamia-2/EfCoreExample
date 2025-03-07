﻿using EfCoreExample;
using Microsoft.Data.SqlClient;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;


InsertStudent(connectionString, new Student
{
    FirstName = null, 
    LastName = null,
    Birthday = new DateTime(1995,12,3),
    GroupId = 1,
    Course = 3
});
InsertGroup(connectionString, new Group {Title = "P34" })

static void InsertStudent(string connectionString, Student student)
{
    //INSERT
    try
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("Connection open");

            string query = $"" +
                $"INSERT INTO Students (FirstName,LastName, Birthday, Course, GroupId) " +
                $"VALUES ('{student.FirstName}', '{student.LastName}', '{student.Birthday.ToString("yyyy-MM-dd")}', {student.Course}, {student.GroupId})";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }

    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }

}

static void InsertGroup(string connectionString, Group group)
{
    //INSERT
    try
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("Connection open");

            string query = $"" +
                $"INSERT INTO Groups (Title) " +
                $"VALUES ('{group.GroupTitle}')";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }

    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }

}

static List<Student> SelectAllStudents(string connectionString)
{
    List<Student> students = [];
    // SELECT
    try
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT Id,FirstName,LastName,Birthday,Course,GroupId FROM Students";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {


                    while (reader.Read())
                    {
                        Student student = new();
                        student.Id = reader.GetInt32(0);
                        student.FirstName = reader.GetString(1);
                        student.LastName = reader.GetString(2);
                        student.Birthday = reader.GetDateTime(3);
                        student.Course = reader.GetInt32(4);
                        student.GroupId = reader.GetInt32(5);
                        students.Add(student);
                    }
                }
            }
        }

    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
    return students;
}


string connectionString = "Data Source=SILVERSTONE\\SQLEXPRESS;Integrated Security=True;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False; DataBase=Database1";

//InsertStudent(connectionString, new Student
//{
//    FirstName = "",
//    LastName = "",
//    Birthday = new DateTime(1995, 12, 3),
//    GroupId = 1,
//    Course = 3
//}
//    );
//InsertGroup(connectionString, new Group { Title = "P34" });



var students = SelectAllStudents(connectionString);
students.ForEach(x => Console.WriteLine($"{x.Id} {x.LastName} {x.FirstName} {x.Birthday.ToString("dd.MM.yyyy")} curs:{x.Course} gr:{x.GroupId}"));