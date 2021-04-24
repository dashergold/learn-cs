using System;
using System.Collections.Generic;

namespace UnitTests
{
    class Student
    {
        private int age;
        string name;

        private List<Grade> grades;

        public Student(string name, int age)
        {
            this.name = name;
            this.age = age;
            this.grades = new List<Grade>();
        }

        public void SetGrade(string subject, string letter)
        {
            var grade = new Grade(subject, letter);
            this.grades.Add(grade);
        }

        public string GetGrade(string subject)
        {
            foreach (var grade in this.grades)
            {
                if (grade.Subject == subject)
                    return grade.Letter;
            }
            return null;
        }

        public void Print()
        {
            Console.WriteLine("{0}, age {1} ======", name, age);
            Console.WriteLine("===========");
            foreach (var grade in grades)
            {
                grade.Print();
            }
        }
    }

    class Grade
    {
        public string Letter;
        public string Subject;

        public Grade(string subject, string letter)
        {
            this.Letter = letter;
            this.Subject = subject;
        }
        public void Print()
        {
            Console.WriteLine("  {0,-20} {1}", Subject, Letter);
        }
    }
}
