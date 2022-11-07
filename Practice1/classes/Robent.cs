using System;

namespace Practice1
{
    public abstract class Robent : IRobent
    {

        public int height, weight;
        public String sex;
        
        protected Robent(){}

        protected Robent(int height, int weight, string sex)
        {
            this.height = height;
            this.weight = weight;
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

        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        public int Weight { 
            get { return weight; }
            set { weight = value; }
        }
        public int Height { 
            get => height;
            set { height = value; }
        }
        public virtual void Gnaw() {
            Console.WriteLine("Грызун грызет");
        }
    }
}