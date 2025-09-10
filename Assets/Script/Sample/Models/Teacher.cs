namespace Sample.Models
{
    public class Teacher : Person, IWorker
    {
        public string Subject { get; set; }

        public Teacher(string name, int age, string subject)
            : base(name, age)
        {
            Subject = subject;
        }

        public override void Introduce()
        {
            System.Console.WriteLine($"Hello, I'm {Name}, I teach {Subject}.");
        }

        public void Work()
        {
            System.Console.WriteLine($"{Name} is teaching {Subject}.");
        }
    }
}
