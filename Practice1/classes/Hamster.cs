using System;

namespace Practice1
{
    public class Hamster : Robent
    {
        public int teethCount;

        public Hamster(int height, int width, string sex, int teethCount) : base(height, width, sex)
        {
            this.teethCount = teethCount;
        }

        public void Bite()
        {
            Console.WriteLine("у меня "+ teethCount +"! Я тебя съем.");
        }
        
        public override void Gnaw()
        {
            Console.WriteLine("Хомяк чешется");
        }

        public override void Species()
        {
            base.Species();
            Console.WriteLine("Хомяк");
        }

        public override string  About()
        {
            return "Я хомяк";
        }
        
        public static Hamster operator +(Hamster a, Hamster b)
        {
            return new Hamster(
                (a.height + b.height) / 2, 
                (b.width + a.width), "male", 
                a.teethCount + b.teethCount);
        }

        public override string ToString()
        {
            return "Хомяк: " + sex + ", " + width + ", " + height + ", " + teethCount;
        }
    }
}