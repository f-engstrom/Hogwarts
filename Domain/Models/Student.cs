using System.Collections.Generic;

namespace Hogwarts.Domain.Models
{
    class Student
    {

        public int Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string SocialSecurityNumber { get; protected set; }

        public Address Address { get; protected set; }



        public Student()
        {

        }

        public Student(string firstName, string lastName, string socialSecurityNumber, Address address)
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
            Address = address;
        }

        public List<StudentCourse> Courses { get; protected set; } = new List<StudentCourse>();
    }
}
