namespace Sample.Models
{
    public class Student : Person
    {
        public string Major { get; set; }

        public Student(string name, int age, string major) 
            : base(name, age)
        {
            Major = major;
        }

        public override void Introduce()
        {
            System.Console.WriteLine($"Hi, I'm {Name}, a {Major} student.");
        }
    }
}
