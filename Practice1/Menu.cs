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

        public void run()
        {
            InitObject();
        }

        private Object InitObject()
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
            int d = 2;

            ExecuteFunction(constructors[d - 1]);

            foreach (var obj in m_objects)
            {
                Console.WriteLine(obj);
            }

            return new Object();
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