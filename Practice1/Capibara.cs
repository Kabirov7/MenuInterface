using System;

namespace Practice1
{
    public class Capibara
    {
        public static String species = "Capibara";

        public static string Species => species;

        public int height, weight, age;
        public String sex, nickname;
        private static String[] SEXES = {"девочка", "мальчик"};

        private String gender;
        
        public Capibara(int height, int weight, int age, string sex, string nickname, string gender)
        {
            this.height = height;
            this.weight = weight;
            this.age = age;
            this.sex = sex;
            this.nickname = nickname;
            this.gender = gender;
        }

        public Capibara(int age, string sex, string nickname)
        {
            this.age = age;
            this.sex = sex;
            this.nickname = nickname;
        }

        public Capibara(Capibara obj)
        {
            this.height = obj.height;
            this.weight = obj.weight;
            this.age = obj.age;
            this.sex = obj.sex;
            this.nickname = obj.nickname;
            this.gender = obj.gender;
        }

        public void capibaring()
        {
            Console.WriteLine("Я сейчас капибарюсь...");
        }

        public void spiting()
        {
            Console.WriteLine("Плюю...");
        }

        public void scratching()
        {
            Console.WriteLine("Чешусь...");
        }

        public void PrintArray(object[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }
        }

        private static int getRandomSex()
        {
            Random random = new Random();
            return random.Next(0, SEXES.Length);
        }

        public override string ToString()
        {
            return Species + ": " + nickname + ", " + sex + ", " + age;
        }

        public static Capibara operator +(Capibara a, Capibara b)
        {
            return new Capibara(0, SEXES[getRandomSex()], a.nickname + " " + b.nickname);
        }

        public void hello(Capibara count)
        {
            Console.WriteLine(this);
            Console.WriteLine("Hello "+count+" times");
        }
    }
}