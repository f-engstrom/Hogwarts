namespace Hogwarts.Domain.Models
{
    class StudentCourse
    {

        public int StudentId { get; protected set; }
        public int CourseId { get; protected set; }
        public Student Student { get; protected set; }

        public Course Course { get; protected set; }


        public StudentCourse()
        {

        }

        public StudentCourse(Student student)
        {
            Student = student;
        }

    }
}
