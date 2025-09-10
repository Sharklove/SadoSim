using Sample.Models;

namespace Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Student("Alice", 20, "Computer Science");
            IWorker worker = new Teacher("Bob", 40, "Mathematics");

            p.Introduce();
            worker.Work();
        }
    }
}
