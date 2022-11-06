using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Practice1
{
    public class Menu
    {
        public Type m_type;
        private Object m_instance;
        private static List<Object> m_objects = new List<Object>();

        public Menu(Type type)
        {
            this.m_type = type;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("1. Create object.");
                Console.WriteLine("2. List objects.");
                Console.WriteLine("3. Use methods of some object.");
                int input = ValidateInput(1, 3);
                switch (input)
                {
                    case 1:
                        CreateObject();
                        break;
                    case 2:
                        ListObjects();
                        break;
                    case 3:
                        Console.WriteLine("In progress");
                        break;
                    
                }
            }
        }

        public void ListObjects()
        {
            foreach (var obj in m_objects)
            {
                Console.WriteLine(obj);
            }
        }

        private void CreateObject()
        {
            ConstructorInfo[] constructors = this.m_type.GetConstructors();
            Console.WriteLine("How do you want to init your object?");
            int i;
            for (i = 0; i < constructors.Length; i++)
            {
                var ctor = constructors[i];


                Console.WriteLine(" " + (i + 1) + ". " + MenuTool.BeautifyFunction(m_type.Name, ctor.GetParameters()));
            }

            Console.WriteLine(" " + (i + 1) + ". " + "Exit");
            int d = ValidateInput(1, i);

            ExecuteFunction(constructors[d - 1]);
        }

        public void ExecuteFunction(MethodBase func)
        {
            object[] values = InputParameters(func.GetParameters());
            object new_instance = ((ConstructorInfo) func).Invoke(values);
            m_objects.Add(new_instance);
        }

        private object[] InputParameters(ParameterInfo[] parameters)
        {
            object[] objects = new object[parameters.Length];
            Console.WriteLine(MenuTool.BeautifyFunction(m_type.Name, parameters));

            for (int i = 0; i < parameters.Length; i++)
            {
                Console.WriteLine("Input "+ parameters[i].Name);
                Object value = inputParam(parameters[i].ParameterType);
                objects[i] = value;
            }

            return objects;
        }

        private int ValidateInput(int start, int end)
        {
            while (true)
            {
                int i = (int) inputParam(typeof(int));
                if (i >= start && i <= end)
                {
                    return i;
                } 
                Console.WriteLine("You cannot call this command");
            }
        }

        private Object inputParam(Type t)
        {
            Object result;
            while (true)
            {
                try
                {
                    Object r = Console.ReadLine();
                    result = Convert.ChangeType(r, t);
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }
    }
}