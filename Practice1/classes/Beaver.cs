using System;

namespace Practice1
{
    public class Beaver : Robent
    {
        public int speed;

        public Beaver(int height, int weight, string sex, int speed) : base(height, weight, sex)
        {
            this.speed = speed;
        }

        public void Swim()
        {
            Console.WriteLine("Я плаваю " + speed + " в секунду! Я тебя утоплю.");
        }

        public override void Gnaw()
        {
            Console.WriteLine("Хомяк чешется");
        }

        public override void Species()
        {
            base.Species();
            Console.WriteLine("Бобер");
        }

        public string About()
        {
            return "Я бобер";
        }

        public static Beaver operator +(Beaver a, Beaver b)
        {
            return new Beaver(
                (a.height + b.height) / 2,
                (b.weight + a.weight), "male",
                (a.speed + b.speed) / 2);
        }
        
        public override string ToString()
        {
            return "Бобер: " + sex + ", " + weight + ", " + height + ", " + speed;
        }
    }
}