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

            InputParameters(constructors[d-1].GetParameters());
            
            return new Object();
        }

        private void InputParameters(ParameterInfo[] parameters)
        {
            IDictionary<String, Object> values = new Dictionary<String, Object>();
            Console.WriteLine(MenuTool.BeautifyFunction(m_type.Name, parameters));
            foreach (var parameter in parameters)
            {
                Console.WriteLine("Input "+ parameter.Name);
                
                Object value = inputParam(parameter.ParameterType);
                values.Add(parameter.Name, value);
            }
            
            Console.WriteLine(values);
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