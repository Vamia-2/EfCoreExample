using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreExample.Models;

public class App
{
    private readonly Students2Context context;
    public App(Students2Context students2Context)
    {
        context = students2Context;
    }

    public void Run()
    {
        while (true)
        {
            ShowMenu();
            Console.WriteLine("Enter your choice: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Add student");
                    AddStudent();
                    break;
                case "2":
                    Console.WriteLine("Update student");
                    UpdateStudent();
                    break;
                case "3":
                    Console.WriteLine("Delete student");
                    DeleteStudent();
                    break;
                case "4":
                    Console.WriteLine("Show students");
                    ShowStudents();
                    break;
                case "5":
                    Console.WriteLine("Show groups");
                    ShowGroups();
                    break;
                case "6":
                    Console.WriteLine("Search students");
                    SearchStudents();
                    break;
                case "7":
                    Console.WriteLine("Stats");
                    ShowStats();
                    break;
                case "8":
                    Console.WriteLine("Add subject to group");
                    AddSubjectToGroup();
                    break;
                case "10":
                    Console.WriteLine("Remove subject from group");
                    RemoveSubjectFromGroup();
                    break;
                case "9":
                    Console.WriteLine("Exit");
                    return;
            }
        }

    }


    private void RemoveSubjectFromGroup()
    {
        ShowGroups();
        Console.WriteLine("Enter group id: ");
        var groupId = int.Parse(Console.ReadLine());
        var group = context.Groups.Include(x => x.Subjects).FirstOrDefault(x => x.Id == groupId);

        if (group == null)
        {
            Console.WriteLine("Group not found");
            return;
        }

        Console.WriteLine("Subjects:");
        foreach (var subject in group.Subjects)
        {
            Console.WriteLine($"{subject.Id} {subject.Title}");
        }

        Console.WriteLine("Enter subject id to remove: ");
        var subjectId = int.Parse(Console.ReadLine());
        var removeSubject = group.Subjects.First(x => x.Id == subjectId);

        group.Subjects.Remove(removeSubject);
        context.SaveChanges();

    }

    private void AddSubjectToGroup()
    {
        ShowGroups();
        Console.WriteLine("Enter group id: ");
        var groupId = int.Parse(Console.ReadLine());
        var group = context.Groups.Include(x => x.Subjects).First(x => x.Id == groupId);

        var subjects = context.Subjects.ToList();
        
        Console.WriteLine("Subjects:");
        foreach (var subject in subjects)
        {
            Console.WriteLine($"{subject.Id} {subject.Title}");
        }

        Console.WriteLine("Enter subject id: ");
        var subjectId = int.Parse(Console.ReadLine());
        var addSubject = context.Subjects.First(x => x.Id == subjectId);

        group.Subjects.Add(addSubject);
        context.SaveChanges();
    }

    private void DeleteStudent()
    {
        Console.WriteLine("Enter student id: ");
        int id = int.Parse(Console.ReadLine());
        var student = context.Students.Find(id);
        if (student == null)
        {
            Console.WriteLine("Student not found");
            return;
        }
        context.Students.Remove(student);
        context.SaveChanges();
        Console.WriteLine("Student deleted");
    }

    private void AddStudent()
    {
        var student = new Student();
        Console.WriteLine("Enter first name: ");
        student.FirstName = Console.ReadLine();
        Console.WriteLine("Enter last name: ");
        student.LastName = Console.ReadLine();
        Console.WriteLine("Enter birthday (YYYY-MM-DD): ");
        student.Birthday = DateOnly.FromDateTime(DateTime.Parse(Console.ReadLine()));
        Console.WriteLine("Enter course: ");
        student.Course = int.Parse(Console.ReadLine());
        ShowGroups();
        Console.WriteLine("Enter group id: ");
        student.GroupId = int.Parse(Console.ReadLine());
        context.Students.Add(student);
        context.SaveChanges();
        Console.WriteLine("Student added");
    }

    private void UpdateStudent()
    {
        Console.WriteLine("Enter student id: ");
        int id = int.Parse(Console.ReadLine());
        var student = context.Students.Find(id);
        if (student == null)
        {
            Console.WriteLine("Student not found");
            return;
        }
        Console.WriteLine("Enter first name: ");
        student.FirstName = Console.ReadLine();
        Console.WriteLine("Enter last name: ");
        student.LastName = Console.ReadLine();
        Console.WriteLine("Enter phone: ");
        student.Phone = Console.ReadLine();
        Console.WriteLine("Enter birthday (YYYY-MM-DD): ");
        student.Birthday = DateOnly.FromDateTime(DateTime.Parse(Console.ReadLine()));
        Console.WriteLine("Enter course: ");
        student.Course = int.Parse(Console.ReadLine());
        ShowGroups();
        Console.WriteLine("Enter group id: ");
        student.GroupId = int.Parse(Console.ReadLine());
        context.SaveChanges();
        Console.WriteLine("Student updated");
    }

    private void ShowGroups()
    {
        var groups = context.Groups.Include(x => x.Subjects).ToList();
        foreach (var group in groups)
        {
            Console.WriteLine($"{group.Id} {group.Title}");
            foreach (var subject in group.Subjects)
            {
                Console.WriteLine($"          {subject.Title}");
            }
        }
    }

    private void ShowStudents()
    {
        var students = context.Students.Include(x => x.Group).ToList();
        foreach (var student in students)
        {
            Console.WriteLine($"{student.Id} {student.FirstName} {student.LastName} {student.Phone} {student.Birthday} {student.Course} {student.Group.Title}");
        }
    }

    private void SearchStudents()
    {
        Console.WriteLine("Enter search term: ");
        string term = Console.ReadLine();

        // SELECT * FROM Students WHERE FirstName LIKE '%term%' OR LastName LIKE '%term%'
        var students = context.Students
            .Include(x => x.Group)
            .Where(x => x.FirstName.Contains(term) || x.LastName.Contains(term))
            .ToList();

        if (students.Count == 0)
        {
            Console.WriteLine("No students found");
            return;
        }
        foreach (var student in students)
        {
            Console.WriteLine($"{student.Id} {student.FirstName} {student.LastName} {student.Phone} {student.Birthday} {student.Course} {student.Group.Title}");
        }
    }

    private void ShowStats()
    {
        Console.WriteLine("Total students count");

        // SELECT COUNT(Id) FROM Students
        var totalStudents = context.Students.Count();
        Console.WriteLine(totalStudents);

        Console.WriteLine("Students count by group");

        // SELECT Groups.Title, COUNT(Students.Id)
        // FROM Students
        // LEFT JOIN Groups ON Students.GroupId=Group.Id
        // GROUP BY Groups.Title

        var studentsByGroup = context.Students
            .GroupBy(x => x.Group.Title)
            .Select(x => new { Group = x.Key, Count = x.Count() })
            .ToList();

        foreach (var item in studentsByGroup)
        {
            Console.WriteLine($"{item.Group} {item.Count}");
        }

    }

    private void ShowMenu()
    {
        Console.WriteLine("--------------------------");
        Console.WriteLine("1. Add student");
        Console.WriteLine("2. Update student");
        Console.WriteLine("3. Delete student");
        Console.WriteLine("4. Show students");
        Console.WriteLine("5. Show groups");
        Console.WriteLine("6. Search students");
        Console.WriteLine("7. Show stats");
        Console.WriteLine("8. Add subject to group");
        Console.WriteLine("10. Remove subject from group");
        Console.WriteLine("9. Exit");
        Console.WriteLine("--------------------------");
    }

}
