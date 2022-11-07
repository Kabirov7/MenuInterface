using System;

namespace Practice1
{
    public abstract class Robent : IRobent
    {

        public int height, width;
        public String sex;
        
        protected Robent(){}

        protected Robent(int height, int width, string sex)
        {
            this.height = height;
            this.width = width;
            this.sex = sex;
        }

        public static Robent operator +(Robent a, Robent b)
        {
            throw new NotImplementedException();
        }

        public virtual void Species()
        {
            Console.WriteLine("Грызун");
        }

        public virtual string About()
        {
            return "Я какой-то грызун";
        }

        public void ShowClassName()
        {
            Console.WriteLine("Грызун");
        }

        public string Sex { get; set; }
        public float Weight { get; set; }
        public int Age { get; set; }
        public virtual void Gnaw() {
            Console.WriteLine("Грызун грызет");
        }
    }
}