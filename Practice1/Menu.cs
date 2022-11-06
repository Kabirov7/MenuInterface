using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Practice1
{
    public class Menu
    {
        public Type m_type;
        private static List<Object> m_objects = new List<Object>();

        public Menu(Type type)
        {
            this.m_type = type;
        }

        public void MockObjects()
        {
            for (int i = 0; i < 5; i++)
            {
                m_objects.Add(new Capibara(i * 10, "male", "R" + i + "D" + i));
            }
        }

        public void Run()
        {
            MockObjects();
            // TestMethods();
            while (true)
            {
                Console.WriteLine("1. Create object.");
                Console.WriteLine("2. List objects.");
                Console.WriteLine("3. Use methods of some object.");
                int input = ValidateInput(1, 3);
                switch (input)
                {
                    case 1:
                        UseMethods(true);
                        break;
                    case 2:
                        ListObjects();
                        break;
                    case 3:
                        UseMethods(false);
                        break;
                    
                }
            }
        }

        public void ListObjects()
        {
            int i = 1;
            foreach (var obj in m_objects)
            {
                Console.WriteLine("#" + i++ + " " + obj);
            }
        }

        private void UseMethods(bool IsCtor = false)
        {
            MethodBase[] functions = new MethodBase[] { };
            if (IsCtor)
            {
                functions = this.m_type.GetConstructors();
            }
            else
            {
                functions = TestMethods();
            }

            ShowFunctions(functions);
            int max = functions.Length + 1;
            Console.WriteLine(" " + max + ". " + "Exit");

            int select = ValidateInput(1, max);
            ExecuteFunction(functions[select - 1], IsCtor);
        }

        private void ShowFunctions(MethodBase[] functions)
        {
            for (int i = 0; i < functions.Length; i++)
            {
                var func = functions[i];
                Console.WriteLine(" " + (i + 1) + ". " + MenuTool.BeautifyFunction(func.Name, func.GetParameters()));
            }
        }

        private int SelectObject()
        {
            Console.WriteLine("Please, select instance with what you want to work");
            ListObjects();
            int selected = ValidateInput(1, m_objects.Count)-1;
            return selected;
        }

        private MethodBase[] TestMethods()
        {
            int active = SelectObject();
            Console.WriteLine(m_objects[active]);
            Type currentType = m_objects[active].GetType();
            return currentType.GetMethods();
        }

        public void ExecuteFunction(MethodBase func, bool IsCtor=false)
        {
            object[] values = InputParameters(func.Name, func.GetParameters());
            if (IsCtor)
            {
                object new_instance = ((ConstructorInfo) func).Invoke(values);
                m_objects.Add(new_instance);
            }
            else
            {
            }
        }

        private object[] InputParameters(String name,ParameterInfo[] parameters)
        {
            object[] objects = new object[parameters.Length];
            Console.WriteLine(MenuTool.BeautifyFunction(name, parameters));

            for (int i = 0; i < parameters.Length; i++)
            {
                Console.WriteLine("Input "+ parameters[i].Name);
                Object value = InputParam(parameters[i].ParameterType);
                objects[i] = value;
            }

            return objects;
        }

        private int ValidateInput(int start, int end)
        {
            while (true)
            {
                int i = (int) InputParam(typeof(int));
                if (i >= start && i <= end)
                {
                    return i;
                } 
                Console.WriteLine("You cannot call this command");
            }
        }

        private Object InputParam(Type t)
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