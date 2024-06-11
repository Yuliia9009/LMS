using System;
using System.Collections.Generic;

namespace LearningManagementSystem
{
    class Program
    {
        static List<Student> students = new List<Student>();
        static List<Teacher> teachers = new List<Teacher>();
        static List<Admin> admins = new List<Admin>();
        static List<Course> courses = new List<Course>();

        static void Main(string[] args)
        {
            SeedData();
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Добро пожаловать в систему управления учебным процессом!");
                Console.WriteLine("1. Войти как Студент");
                Console.WriteLine("2. Войти как Преподаватель");
                Console.WriteLine("3. Войти как Администратор");
                Console.WriteLine("4. Выход");
                Console.Write("Выберите опцию: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        User student = AuthenticateUser(students);
                        if (student != null)
                        {
                            StudentMenu((Student)student);
                        }
                        break;
                    case "2":
                        User teacher = AuthenticateUser(teachers);
                        if (teacher != null)
                        {
                            TeacherMenu((Teacher)teacher);
                        }
                        break;
                    case "3":
                        User admin = AuthenticateUser(admins);
                        if (admin != null)
                        {
                            AdminMenu((Admin)admin);
                        }
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неправильный выбор, попробуйте снова.");
                        break;
                }
            }
        }

        static User AuthenticateUser<T>(List<T> users) where T : User
        {
            Console.Write("Введите логин: ");
            string login = Console.ReadLine();
            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();

            foreach (var user in users)
            {
                if (user.Login == login && user.Password == password)
                {
                    Console.WriteLine($"Добро пожаловать, {user.FullName}!");
                    return user;
                }
            }

            Console.WriteLine("Неправильный логин или пароль.");
            return null;
        }

        static void StudentMenu(Student student)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("1. Просмотреть курсы");
                Console.WriteLine("2. Просмотреть оценки за курсы");
                Console.WriteLine("3. Просмотреть посещаемость курса");
                Console.WriteLine("4. Просмотреть домашние задания");
                Console.WriteLine("5. Просмотреть комментарии преподавателя");
                Console.WriteLine("6. Оставить комментарий администратору");
                Console.WriteLine("7. Назад в главное меню");
                Console.Write("Выберите опцию: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewCourses(student);
                        break;
                    case "2":
                        ViewGrades(student);
                        break;
                    case "3":
                        ViewAttendance(student);
                        break;
                    case "4":
                        ViewAssignments(student);
                        break;
                    case "5":
                        ViewTeacherComments(student);
                        break;
                    case "6":
                        LeaveCommentForAdmin(student);
                        break;
                    case "7":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Неправильный выбор, попробуйте снова.");
                        break;
                }
            }
        }

        static void ViewCourses(Student student)
        {
            Console.WriteLine("Ваши курсы:");
            foreach (var course in courses)
            {
                if (course.Students.Contains(student))
                {
                    Console.WriteLine(course.CourseName);
                }
            }
        }

        static void ViewGrades(Student student)
        {
            Console.WriteLine("Ваши оценки:");
            foreach (var course in courses)
            {
                if (course.Students.Contains(student))
                {
                    Console.WriteLine($"{course.CourseName}: {course.Grades[student]}");
                }
            }
        }

        static void ViewAttendance(Student student)
        {
            Console.WriteLine("Ваша посещаемость:");
            foreach (var course in courses)
            {
                if (course.Students.Contains(student))
                {
                    int attended = course.Attendance.ContainsKey(student) ? course.Attendance[student] : 0;
                    double attendancePercentage = (double)attended / course.TotalLessons * 100;
                    Console.WriteLine($"{course.CourseName}: {attendancePercentage}% ({attended}/{course.TotalLessons})");
                }
            }
        }

        static void ViewAssignments(Student student)
        {
            Console.WriteLine("Ваши домашние задания:");
            foreach (var course in courses)
            {
                if (course.Students.Contains(student))
                {
                    Console.WriteLine($"{course.CourseName}: {course.Assignments[student]}");
                }
            }
        }

        static void ViewTeacherComments(Student student)
        {
            Console.WriteLine("Комментарии преподавателя:");
            foreach (var course in courses)
            {
                if (course.Students.Contains(student) && course.Comments.ContainsKey(student))
                {
                    Console.WriteLine($"{course.CourseName}: {course.Comments[student]}");
                }
            }
        }

        static void LeaveCommentForAdmin(User user)
        {
            Console.WriteLine("Оставить комментарий администратору:");
            Console.Write("Комментарий: ");
            string comment = Console.ReadLine();

            foreach (var admin in admins)
            {
                if (!admin.Comments.ContainsKey(user))
                {
                    admin.Comments[user] = new List<string>();
                }
                admin.Comments[user].Add(comment);
            }

            Console.WriteLine("Комментарий оставлен.");
        }

        static void TeacherMenu(Teacher teacher)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("1. Отметить посещаемость студентов");
                Console.WriteLine("2. Выставить оценки");
                Console.WriteLine("3. Оставить комментарий студенту");
                Console.WriteLine("4. Задать домашние задания");
                Console.WriteLine("5. Оставить комментарий администратору");
                Console.WriteLine("6. Назад в главное меню");
                Console.Write("Выберите опцию: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        MarkAttendance(teacher);
                        break;
                    case "2":
                        GradeStudents(teacher);
                        break;
                    case "3":
                        LeaveCommentForStudent(teacher);
                        break;
                    case "4":
                        AssignHomework(teacher);
                        break;
                    case "5":
                        LeaveCommentForAdmin(teacher);
                        break;
                    case "6":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Неправильный выбор, попробуйте снова.");
                        break;
                }
            }
        }

        static void MarkAttendance(Teacher teacher)
        {
            Console.WriteLine("Отметка посещаемости:");
            foreach (var course in teacher.Courses)
            {
                Course currentCourse = courses.Find(c => c.CourseName == course);
                Console.WriteLine($"Курс: {currentCourse.CourseName}");
                foreach (var student in currentCourse.Students)
                {
                    Console.WriteLine($"Студент: {student.FullName}");
                    Console.Write("Посетил (да/нет): ");
                    string attended = Console.ReadLine();
                    if (attended.ToLower() == "да")
                    {
                        if (!currentCourse.Attendance.ContainsKey(student))
                        {
                            currentCourse.Attendance[student] = 0;
                        }
                        currentCourse.Attendance[student]++;
                    }
                }
            }
        }

        static void GradeStudents(Teacher teacher)
        {
            Console.WriteLine("Выставление оценок:");
            foreach (var course in teacher.Courses)
            {
                Course currentCourse = courses.Find(c => c.CourseName == course);
                Console.WriteLine($"Курс: {currentCourse.CourseName}");
                foreach (var student in currentCourse.Students)
                {
                    Console.WriteLine($"Студент: {student.FullName}");
                    Console.Write("Оценка: ");
                    int grade = int.Parse(Console.ReadLine());
                    currentCourse.Grades[student] = grade;
                }
            }
        }

        static void LeaveCommentForStudent(Teacher teacher)
        {
            Console.WriteLine("Оставить комментарий студенту:");
            foreach (var course in teacher.Courses)
            {
                Course currentCourse = courses.Find(c => c.CourseName == course);
                Console.WriteLine($"Курс: {currentCourse.CourseName}");
                foreach (var student in currentCourse.Students)
                {
                    Console.WriteLine($"Студент: {student.FullName}");
                    Console.Write("Комментарий: ");
                    string comment = Console.ReadLine();
                    currentCourse.Comments[student] = comment;
                }
            }
        }

        static void AssignHomework(Teacher teacher)
        {
            Console.WriteLine("Задать домашние задания:");
            foreach (var course in teacher.Courses)
            {
                Course currentCourse = courses.Find(c => c.CourseName == course);
                Console.WriteLine($"Курс: {currentCourse.CourseName}");
                foreach (var student in currentCourse.Students)
                {
                    Console.WriteLine($"Студент: {student.FullName}");
                    Console.Write("Домашнее задание: ");
                    string assignment = Console.ReadLine();
                    currentCourse.Assignments[student] = assignment;
                }
            }
        }

        static void AdminMenu(Admin admin)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("1. Добавить курс");
                Console.WriteLine("2. Удалить курс");
                Console.WriteLine("3. Добавить пользователя (Студент/Преподаватель");
                Console.WriteLine("4. Просмотреть комментарии от студентов и преподавателей)");
                Console.WriteLine("5. Редактировать оценки студентов");
                Console.WriteLine("6. Редактировать посещаемость студентов");
                Console.WriteLine("7. Назад в главное меню");
                Console.Write("Выберите опцию: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCourse();
                        
                        break;
                    case "2":
                        RemoveCourse();
                        break;
                    case "3":
                        AddUser();
                        break;
                    case "4":
                        ViewCommentsForAdmin(admin);
                        break;
                    case "5":
                        EditGrades();
                        break;
                    case "6":
                        EditAttendance();
                        break;
                    case "7":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Неправильный выбор, попробуйте снова.");
                        break;
                }
            }
        }

        static void EditGrades()
        {
            Console.WriteLine("Редактирование оценок студентов:");
            foreach (var course in courses)
            {
                Console.WriteLine($"Курс: {course.CourseName}");
                foreach (var student in course.Students)
                {
                    Console.WriteLine($"Студент: {student.FullName}");
                    Console.Write("Новая оценка: ");
                    int newGrade = int.Parse(Console.ReadLine());
                    course.Grades[student] = newGrade;
                }
            }
        }

        static void AddCourse()
        {
            Console.Write("Введите название курса: ");
            string courseName = Console.ReadLine();
            Console.Write("Введите количество уроков: ");
            int totalLessons = int.Parse(Console.ReadLine());
            courses.Add(new Course { CourseName = courseName, TotalLessons = totalLessons });
            Console.WriteLine("Курс добавлен.");
        }

        static void RemoveCourse()
        {
            Console.Write("Введите название курса для удаления: ");
            string courseName = Console.ReadLine();
            courses.RemoveAll(c => c.CourseName == courseName);
            Console.WriteLine("Курс удален.");
        }

        static void AddUser()
        {
            Console.WriteLine("1. Добавить студента");
            Console.WriteLine("2. Добавить преподавателя");
            Console.Write("Выберите опцию: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddStudent();
                    break;
                case "2":
                    AddTeacher();
                    break;
                default:
                    Console.WriteLine("Неправильный выбор, попробуйте снова.");
                    break;
            }
        }

        static void AddStudent()
        {
            Student student = new Student();
            Console.Write("Введите ФИО: ");
            student.FullName = Console.ReadLine();
            Console.Write("Введите дату рождения: ");
            student.BirthDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Введите e-mail: ");
            student.Email = Console.ReadLine();
            Console.Write("Введите номер телефона: ");
            student.PhoneNumber = Console.ReadLine();
            Console.Write("Введите логин: ");
            student.Login = Console.ReadLine();
            Console.Write("Введите пароль: ");
            student.Password = Console.ReadLine();
            Console.Write("Введите курс (1-5): ");
            student.Course = int.Parse(Console.ReadLine());

            students.Add(student);
            Console.WriteLine("Студент добавлен.");
        }

        static void AddTeacher()
        {
            Teacher teacher = new Teacher();
            Console.Write("Введите ФИО: ");
            teacher.FullName = Console.ReadLine();
            Console.Write("Введите дату рождения: ");
            teacher.BirthDate = DateTime.Parse(Console.ReadLine());
            Console.Write("Введите e-mail: ");
            teacher.Email = Console.ReadLine();
            Console.Write("Введите номер телефона: ");
            teacher.PhoneNumber = Console.ReadLine();
            Console.Write("Введите логин: ");
            teacher.Login = Console.ReadLine();
            Console.Write("Введите пароль: ");
            teacher.Password = Console.ReadLine();
            teacher.Courses = new List<string>();

            Console.WriteLine("Выберите курсы, которые преподает:");
            foreach (var course in courses)
            {
                Console.WriteLine($"Добавить курс {course.CourseName}? (да/нет)");
                if (Console.ReadLine().ToLower() == "да")
                {
                    teacher.Courses.Add(course.CourseName);
                }
            }

            teachers.Add(teacher);
            Console.WriteLine("Преподаватель добавлен.");
        }

        static void ViewCommentsForAdmin(Admin admin)
        {
            Console.WriteLine("Комментарии от студентов и преподавателей:");
            foreach (var commentPair in admin.Comments)
            {
                User user = commentPair.Key;
                List<string> comments = commentPair.Value;
                Console.WriteLine($"{user.FullName} ({user.GetType().Name}):");
                foreach (var comment in comments)
                {
                    Console.WriteLine($"- {comment}");
                }
            }
        }

        static void EditAttendance()
        {
            Console.WriteLine("Редактирование посещаемости студентов:");
            foreach (var course in courses)
            {
                Console.WriteLine($"Курс: {course.CourseName}");
                foreach (var student in course.Students)
                {
                    Console.WriteLine($"Студент: {student.FullName}");
                    Console.Write("Новое количество посещенных уроков: ");
                    int newAttendance = int.Parse(Console.ReadLine());
                    course.Attendance[student] = newAttendance;
                }
            }
        }

        static void SeedData()
        {
            admins.Add(new Admin { FullName = "Admin", Login = "admin", Password = "admin" });

            Course course1 = new Course { CourseName = "Python", TotalLessons = 20 };
            Course course2 = new Course { CourseName = "C++", TotalLessons = 20 };
            Course course3 = new Course { CourseName = "C#", TotalLessons = 20 };
            courses.Add(course1);
            courses.Add(course2);
            courses.Add(course3);

            Student student1 = new Student
            {
                FullName = "Юлия Девятка",
                BirthDate = new DateTime(1987, 10, 28),
                Email = "ua0685433776@gmail.com",
                PhoneNumber = "0685433776",
                Login = "yuliia",
                Password = "9009",
                Course = 3
            };

            Teacher teacher1 = new Teacher
            {
                FullName = "Иван Петров",
                BirthDate = new DateTime(1985, 5, 5),
                Email = "petrov@google.com",
                PhoneNumber = "0987654321",
                Login = "ivan",
                Password = "0000",
                Courses = new List<string> { "Python", "C++", "C#" }
            };

            students.Add(student1);
            teachers.Add(teacher1);

            course1.Students.Add(student1);
            course2.Students.Add(student1);
            course3.Students.Add(student1);
            course1.Teachers.Add(teacher1);
            course2.Teachers.Add(teacher1);
            course3.Teachers.Add(teacher1);

            course1.Grades[student1] = 12;
            course2.Grades[student1] = 11;
            course3.Grades[student1] = 10;
            course1.Attendance[student1] = 18; 
            course2.Attendance[student1] = 17; 
            course3.Attendance[student1] = 18; 
            course1.Assignments[student1] = "Домашнее задание по Python";
            course2.Assignments[student1] = "Домашнее задание по C++";
            course3.Assignments[student1] = "Домашнее задание по C#";
            course1.Comments[student1] = "Хорошая работа по Python";
            course2.Comments[student1] = "Нужно больше практики по C++";
            course3.Comments[student1] = "Отличные результаты по C#";
        }
    }

    public abstract class User
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class Student : User
    {
        public int Course { get; set; } 
    }

    public class Teacher : User
    {
        public List<string> Courses { get; set; } 
    }

    public class Admin : User
    {
        public Dictionary<User, List<string>> Comments { get; set; } = new Dictionary<User, List<string>>();
    }


    public class Course
    {
        public string CourseName { get; set; }
        public int TotalLessons { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
        public List<Teacher> Teachers { get; set; } = new List<Teacher>();
        public Dictionary<Student, int> Grades { get; set; } = new Dictionary<Student, int>();
        public Dictionary<Student, int> Attendance { get; set; } = new Dictionary<Student, int>();
        public Dictionary<Student, string> Assignments { get; set; } = new Dictionary<Student, string>();
        public Dictionary<Student, string> Comments { get; set; } = new Dictionary<Student, string>();
    }
}
