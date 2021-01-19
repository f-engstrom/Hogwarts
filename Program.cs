using System;
using System.Threading;
using Hogwarts.Data;
using Hogwarts.Domain.Models;
using static System.Console;
using System.Linq;

namespace Hogwarts
{
    class Program
    {
      public static HogwartsContext context = new HogwartsContext();

        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            bool shouldNotExit = true;

            while (shouldNotExit)
            {


                Clear();




                WriteLine("1. Registrera elev");
                WriteLine("2. Lista elever");
                WriteLine("3. Lägg till lärare");
                WriteLine("4. Lägg till kurs");
                WriteLine("5. Lägg till elev till kurs");
                WriteLine("6. Lista kurser");
                WriteLine("7. Avsluta");

                ConsoleKeyInfo keyPressed = ReadKey(true);


                switch (keyPressed.Key)
                {

                    case ConsoleKey.D1:

                        RegisterStudent();

                        break;

                    case ConsoleKey.D2:
                        ListStudents();

                        break;

                    case ConsoleKey.D3:
                        AddTeacher();
                        break;

                    case ConsoleKey.D4:
                        AddCourse();
                        break;
                    case ConsoleKey.D5:
                        AddStudentToCourse();
                        break;
                    case ConsoleKey.D6:
                        ListCourses();
                        break;



                    case ConsoleKey.D7:

                        shouldNotExit = false;

                        break;

                }

            }
        }

        private static void ListCourses()
        {
            Clear();


            var TeacherCourses = context.Teachers.OrderBy(t => t.FirstName).Select(t => new
            {
                TeacherName = t.Courses.Count > 0 ? $"{t.FirstName} {t.LastName}" : string.Empty,
                Courses = t.Courses.OrderBy(t => t.Title).Select(c => new
                {

                    c.Title,
                    c.Description,
                    c.Credits,
                    Students = c.Students.OrderBy(s => s.Student.LastName).Select(s => new
                    {
                        FullName = $"{s.Student.LastName}, {s.Student.FirstName}"

                    })

                })


            }).ToList();





            foreach (var teacher in TeacherCourses.Where(x => string.IsNullOrEmpty(x.TeacherName) == false))
            {
                WriteLine("\n".PadRight(50, '*'));
                WriteLine(teacher.TeacherName);

                WriteLine("".PadRight(50, '-'));
                foreach (var course in teacher.Courses)
                {
                    WriteLine($"Kurstitle:{course.Title}");
                    WriteLine($"Beskrivning:{course.Description}");
                    WriteLine($"Poäng:{course.Credits}");


                    WriteLine("Elever:");
                    foreach (var student in course.Students)
                    {
                        WriteLine(student.FullName);


                    }

                    if (teacher.Courses.Count() > 1)
                    {
                        WriteLine("".PadRight(50, '-'));

                    }

                }


            }
            WriteLine("\n".PadRight(50, '*'));


            ReadKey();
        }

        private static void AddStudentToCourse()
        {
            Clear();

            bool customerExists = false;
            bool incorrectKey = true;
            bool shouldNotExit = true;




            do
            {


                WriteLine("Elev (personnummer): ");
                string socialSecurityNumber = ReadLine();

                WriteLine("Kurs: ");
                string courseTitle = ReadLine();




                WriteLine("Är detta korrekt? (J)a eller (N)ej");

                ConsoleKeyInfo consoleKeyInfo;

                do
                {

                    consoleKeyInfo = ReadKey(true);

                    incorrectKey = !(consoleKeyInfo.Key == ConsoleKey.J || consoleKeyInfo.Key == ConsoleKey.N);



                } while (incorrectKey);


                if (consoleKeyInfo.Key == ConsoleKey.J)
                {

                    var student = context.Students.FirstOrDefault(a => a.SocialSecurityNumber == socialSecurityNumber);
                    var course = context.Courses.FirstOrDefault(m => m.Title == courseTitle);




                    if (student != null)
                    {
                        StudentCourse studentCourse = new StudentCourse(student);
                        course.Students.Add(studentCourse);
                        context.SaveChanges();

                        shouldNotExit = false;
                        Clear();
                        WriteLine("Student registrerad på kurs");
                        Thread.Sleep(1000);

                    }
                    else if (student == null)
                    {
                        shouldNotExit = false;
                        Clear();
                        WriteLine("Student ej registrerad");
                        Thread.Sleep(1000);

                    }


                }

                Clear();


            } while (shouldNotExit);
        }

        private static void AddCourse()
        {
            Clear();

            bool customerExists = false;
            bool isCorrectKey = true;
            bool shouldNotExit = true;




            do
            {

                WriteLine("Titel: ");
                string title = ReadLine();
                WriteLine("Beskrivning: ");
                string description = ReadLine();
                WriteLine("Credits: ");
                int credits = Convert.ToInt32(ReadLine());
                WriteLine("Lärare (personummer): ");
                string socialSecurityNumber = ReadLine();


                Course course = new Course(title, description, credits);

                WriteLine("Är detta korrekt? (J)a eller (N)ej");

                ConsoleKeyInfo consoleKeyInfo;

                do
                {

                    consoleKeyInfo = ReadKey(true);

                    isCorrectKey = !(consoleKeyInfo.Key == ConsoleKey.J || consoleKeyInfo.Key == ConsoleKey.N);



                } while (isCorrectKey);


                if (consoleKeyInfo.Key == ConsoleKey.J)
                {


                    bool isCourse = context.Courses.Any(m => m.Title == title);

                    bool isTeacher = context.Teachers.Any(p => p.SocialSecurityNumber == socialSecurityNumber);

                    var teacher = context.Teachers.FirstOrDefault(x => x.SocialSecurityNumber == socialSecurityNumber);

                    if (!isCourse && isTeacher)
                    {
                        teacher.Courses.Add(course);
                        context.SaveChanges();

                        shouldNotExit = false;
                        Clear();
                        WriteLine("Kurs registrerad");
                        Thread.Sleep(1000);

                    }
                    else if (isCourse)
                    {
                        shouldNotExit = false;
                        Clear();
                        WriteLine("Kurs redan registrerad");
                        Thread.Sleep(1000);

                    }
                    else if (!isTeacher)
                    {
                        shouldNotExit = false;
                        Clear();
                        WriteLine("Ogiltig Lärare");
                        Thread.Sleep(1000);

                    }


                }

                Clear();


            } while (shouldNotExit);
        }

        private static void AddTeacher()
        {
            Clear();

            bool customerExists = false;
            bool inCorrectKey = true;
            bool shouldNotExit = true;




            do
            {


                WriteLine("Förnamn: ");
                string firstName = ReadLine();
                WriteLine("Efternamn: ");
                string lastName = ReadLine();
                WriteLine("Personummer: ");
                string socialSecurityNumber = ReadLine();

                Teacher teacher = new Teacher(firstName,lastName,socialSecurityNumber);

                WriteLine("Är detta korrekt? (J)a eller (N)ej");

                ConsoleKeyInfo consoleKeyInfo;

                do
                {

                    consoleKeyInfo = ReadKey(true);
                    inCorrectKey = !(consoleKeyInfo.Key == ConsoleKey.J || consoleKeyInfo.Key == ConsoleKey.N);



                } while (inCorrectKey);


                if (consoleKeyInfo.Key == ConsoleKey.J)
                {


                    bool isTeacher = context.Teachers.Any(p => p.SocialSecurityNumber == socialSecurityNumber);

                    if (!isTeacher)
                    {
                        context.Teachers.Add(teacher);
                        context.SaveChanges();

                        shouldNotExit = false;
                        Clear();
                        WriteLine("Lärare registrerad");
                        Thread.Sleep(1000);

                    }
                    else if (isTeacher)
                    {
                        shouldNotExit = false;
                        Clear();
                        WriteLine("Lärare redan registrerad");
                        Thread.Sleep(1000);

                    }


                }

                Clear();


            } while (shouldNotExit);



        }

        private static void ListStudents()
        {
            Clear();

            var students = context.Students.OrderBy(a => a.LastName).Select(a => new
            {
                FullName = $"{a.LastName} {a.FirstName}",
                SocialSecurityNumber = a.SocialSecurityNumber,
                Address = $"{a.Address.Street}, {a.Address.PostCode} {a.Address.City}"

            }).ToList();

            WriteLine("Elever");
            WriteLine("".PadRight(50, '-'));

            foreach (var student in students)
            {
                WriteLine($"{student.FullName}");
                WriteLine($"personummer: {student.SocialSecurityNumber} ");
                WriteLine($"Address: { student.Address}");
                WriteLine("".PadRight(50, '-'));

            }

            ReadKey();
        }

        private static void RegisterStudent()
        {

            Clear();

            bool customerExists = false;
            bool shoudNotExit = true;
            bool inCorrectKey = true;



            do
            {

                WriteLine("Förnamn: ");
                string firstName = ReadLine();
                WriteLine("Efternamn: ");
                string lastName = ReadLine();
                WriteLine("Personummer: ");
                string socialSecurityNumber = ReadLine();

                WriteLine("Street: ");
                string street = ReadLine();
                WriteLine("Postcode: ");
                string postcode = ReadLine();
                WriteLine("City: ");
                string city = ReadLine();

                Address address = new Address(street, postcode, city);


                Student student = new Student(firstName, lastName, socialSecurityNumber, address);

                WriteLine("Är detta korrekt? (J)a eller (N)ej");

                ConsoleKeyInfo consoleKeyInfo;

                do
                {

                    consoleKeyInfo = ReadKey(true);

                    inCorrectKey = !(consoleKeyInfo.Key == ConsoleKey.J || consoleKeyInfo.Key == ConsoleKey.N);



                } while (inCorrectKey);


                if (consoleKeyInfo.Key == ConsoleKey.J)
                {


                    bool isActor = context.Students.Any(r => r.SocialSecurityNumber == socialSecurityNumber);

                    if (!isActor)
                    {
                        context.Students.Add(student);
                        context.SaveChanges();

                        shoudNotExit = false;
                        Clear();
                        WriteLine("Elev registrerad");
                        Thread.Sleep(1000);

                    }
                    else if (isActor)
                    {
                        shoudNotExit = false;
                        Clear();
                        WriteLine("Elev redan registrerad");
                        Thread.Sleep(1000);

                    }


                }

                Clear();


            } while (shoudNotExit);
        }
    }
}
