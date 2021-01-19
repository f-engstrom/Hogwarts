using System.Collections.Generic;

namespace Hogwarts.Domain.Models
{
    class Course
    {


        public int Id { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }

        public int Credits { get; protected set; }


        public Teacher Teacher { get; protected set; }

        public int TeacherId { get; protected set; }


        public Course()
        {

        }

        public Course(string title, string description, int credits)
        {
            Title = title;
            Description = description;
            Credits = credits;
        }


        public List<StudentCourse> Students { get; protected set; } = new List<StudentCourse>();
    }
}

