using NUnit.Framework;

namespace UnitTests
{
    public class Tests
    {
        [Test]
        public void Student_SetGrade()
        {
            var student = new Student("Gunilla", 13);
            student.SetGrade("Mathematics", "B");
        }

        [Test]
        public void Student_GetGrade()
        {
            var student = new Student("Per", 14);
            student.SetGrade("History", "C");

            var grade = student.GetGrade("History");
            Assert.AreEqual("C", grade);
        }
    }
}