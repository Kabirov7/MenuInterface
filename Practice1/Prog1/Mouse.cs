using System;

namespace Practice1
{
    sealed class Mouse : Robent
    {
        public int killedCats;

        public Mouse(int height, int weight, string sex, int killedCats) : base(height, weight, sex)
        {
            this.killedCats = killedCats;
        }

        public void Swim()
        {
            Console.WriteLine("Я убил " + killedCats + " кошек! Но ты мне нравишься)");
        }

        public override void Gnaw()
        {
            Console.WriteLine("Мышка чешется");
        }

        public override void Species()
        {
            base.Species();
            Console.WriteLine("Мышка");
        }

        public string About()
        {
            return "Я крыска";
        }

        public static Mouse operator +(Mouse a, Mouse b)
        {
            return new Mouse(
                (a.height + b.height) / 2,
                (b.weight + a.weight), "male",
                (a.killedCats + b.killedCats) / 2);
        }
        
        public override string ToString()
        {
            return "Хомяк: " + sex + ", " + weight + ", " + height + ", " + killedCats;
        }
    }
}