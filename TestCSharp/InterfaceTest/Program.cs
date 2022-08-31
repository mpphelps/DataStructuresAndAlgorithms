using System;

namespace test
{
    class Program
    {
        public static void Main(string[] args)
        {
            var myDog = new Dog();
            var myAnimalList = new List<IAnimal>(){myDog};
            ChangeAnimalName(myAnimalList, "Luke");
        }
        public static void ChangeAnimalName(List<IAnimal> animals, string name)
        {
            foreach (var animal in animals)
            {
                animal.Name = name;
                
            }
        }
    }
    public interface IAnimal
    {
        string Name { get; set; }
    }
    public class Dog : IAnimal
    {
        public string Name { get; set; }

    }
    public class Cat : IAnimal
    {
        public string Name { get; set; }

        public void Meow()
        {
            Console.WriteLine("Meow");
        }
    }
}