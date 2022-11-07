using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Practice1
{
    public class Menu
    {
        public Type m_type;
        private int m_active;
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
                        UseMethods(m_type.GetType(),true, true);
                        break;
                    case 2:
                        ListObjects();
                        break;
                    case 3:
                        UseMethods(m_type.GetType(),false, false);
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

        private Object UseMethods(Type type, bool IsCtor = false, bool AddInObjects=false)
        {
            MethodBase[] functions = new MethodBase[] { };
            if (IsCtor)
            {
                functions = type.GetConstructors();
            }
            else
            {
                functions = TestMethods();
            }

            ShowFunctions(functions);
            int max = functions.Length + 1;
            Console.WriteLine(" " + max + ". " + "Exit");

            int select = ValidateInput(1, max);
            object newInstance = ExecuteFunction(functions[select - 1], IsCtor);
            if (AddInObjects)
                m_objects.Add(newInstance);
            return newInstance;
        }
        
        private Object CreateObject(object[] values, ConstructorInfo ctor)
        {
            return ctor.Invoke(values);
        }

        private void ShowFunctions(MethodBase[] functions)
        {
            for (int i = 0; i < functions.Length; i++)
            {
                var func = functions[i];
                Console.WriteLine(" " + (i + 1) + ". " + MenuTool.BeautifyFunction(func.Name, func.GetParameters()));
            }
        }

        private void SelectObject()
        {
            Console.WriteLine("Please, select instance with what you want to work");
            ListObjects();
            m_active = ValidateInput(1, m_objects.Count)-1;
        }

        private MethodBase[] TestMethods()
        {
            SelectObject();
            Console.WriteLine(m_objects[m_active]);
            Type currentType = m_objects[m_active].GetType();
            return currentType.GetMethods();
        }

        public Object ExecuteFunction(MethodBase func, bool IsCtor=false)
        {
            object[] values = InputParameters(func.Name, func.GetParameters());
            if (IsCtor)
            {
                object new_instance = CreateObject(values, (ConstructorInfo) func); 
                return new_instance;
            }
            else
            {
                return func.Invoke(m_objects[m_active], values);
            }
        }

        private object[] InputParameters(String name,ParameterInfo[] parameters)
        {
            object[] objects = new object[parameters.Length];
            Console.WriteLine(MenuTool.BeautifyFunction(name, parameters));

            for (int i = 0; i < parameters.Length; i++)
            {
                Console.WriteLine("Input "+ parameters[i].Name);
                Object value; 
                if (MenuTool.NativeTypes.Contains(parameters[i].ParameterType))
                {
                    value = InputParam(parameters[i].ParameterType);    
                }
                else
                {
                    value = InputCustom(parameters[i].ParameterType);
                }

                
                objects[i] = value;
            }

            return objects;
        }

        private object InputCustom(Type t)
        {
            Console.WriteLine("1. Use new object.");
            Console.WriteLine("2. Use previous object.");

            int select = ValidateInput(1, 2);

            object value = null;
            switch (select)
            {
                case 1:
                    value = UseMethods(t, true, false);
                    break;
                case 2:
                    ListObjects();
                    while (true)
                    {
                        int objectIdx = ValidateInput(1, m_objects.Count);
                        if (m_objects[objectIdx].GetType() == t)
                        {
                            value = m_objects[objectIdx];
                            break;
                        }
                    }

                    break;
            }

            return value;
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