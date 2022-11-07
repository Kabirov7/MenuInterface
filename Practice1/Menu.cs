using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Practice1
{
    public class Menu
    {
        public Type[] m_types;
        private int m_active;
        private static List<Object> m_objects = new List<Object>();

        public Menu(Type[] types)
        {
            this.m_types = types;
        }

        public void MockObjects()
        {
            for (int i = 0; i < 5; i++)
            {
                m_objects.Add(new Capibara(i * 10, "male", "R" + i + "D" + i));
            }
        }

        private void SelectTypes()
        {
            Console.WriteLine("Выберите тип для работы:");
            ShowTypes();
            int select = SelectOption(m_types) - 1;
            if (select > m_types.Length - 1) return;

            Console.WriteLine(m_types[select]);
            Type currentType = m_types[select];
            while (true)
            {
                Console.WriteLine("1. Создать объект.");
                Console.WriteLine("2. Отобразить объекты.");
                Console.WriteLine("3. Использовать функции объекта.");
                Console.WriteLine("4. Выход.");
                int input = ValidateInput(1, 4);
                switch (input)
                {
                    case 1:
                        UseMethods(currentType,true, true);
                        break;
                    case 2:
                        ListObjects();
                        break;
                    case 3:
                        UseMethods(currentType,false, false);
                        break;
                    case 4:
                        return;
                }
            }
        }
        
        public void Run()
        {
            MockObjects();
            while (true)
            {
                SelectTypes();
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
            var functions = new MethodBase[] { };
            if (IsCtor)
            {
                functions = type.GetConstructors();
            }
            else
            {
                functions = TestMethods();
            }

            ShowFunctions(functions);
            int select = SelectOption(functions) - 1;

            if (select > functions.Length - 1) return null;
            object newInstance = ExecuteFunction(functions[select], IsCtor);
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
        private void ShowTypes()
        {
            for (int i = 0; i < m_types.Length; i++)
            {
                var t = m_types[i];
                Console.WriteLine(" " + (i + 1) + ". " + t.Name);
            }
        }

        private int SelectOption(object[] arr)
        {
            int max = arr.Length + 1;
            Console.WriteLine(" " + max + ". Выход");

            return ValidateInput(1, max);
        }

        private void SelectObject()
        {
            Console.WriteLine("Пожалуйста, выберите объект с которым хотите работать.");
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
                object result = func.Invoke(m_objects[m_active], values);
                if (result!=null)
                {
                    Console.WriteLine(result);
                }
                return result;
            }
        }

        private object[] InputParameters(String name,ParameterInfo[] parameters)
        {
            object[] objects = new object[parameters.Length];
            Console.WriteLine(MenuTool.BeautifyFunction(name, parameters));

            for (int i = 0; i < parameters.Length; i++)
            {
                Console.WriteLine("Введите "+ parameters[i].Name);

                objects[i] = InputObject(parameters[i].ParameterType);
            }

            return objects;
        }

        private object InputObject(Type parameterType)
        {
            Object value; 
            if (MenuTool.NativeTypes.Contains(parameterType))
            {
                value = InputParam(parameterType);    
            }
            else
            {
                value = InputCustom(parameterType);
            }

            return value;
        }
        
        public object InputCustom(Type t)
        {
            if (t.BaseType != typeof(object[]).BaseType)
            {
                Console.WriteLine("1. Использовать новый объект.");
                Console.WriteLine("2. Использовать предыдущие объекты.");
            }

            int select = ValidateInput(1, 2);
            object value = null;
            switch (select)
            {
                case 1:
                    if (t.BaseType == typeof(object[]).BaseType) 
                        value = FillArray(t);
                    else 
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
                Console.WriteLine("Вы не можете вызвать эту команду");
            }
        }

        private object FillArray(Type arrType)
        {
            Type elType = arrType.GetElementType();

            Console.WriteLine("Сколкьо значений хотите ввести?");
            int count = (int)InputParam(typeof(int));
            object[] objects = new object[count];
            Console.WriteLine("Начинайте вводить ваши данные:");
            for (int i = 0; i < count; i++)
            {
                objects[i] = InputObject(elType);
            }
            return objects;

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
                    Console.WriteLine("Некорректный ввод.");
                }
            }
        }
    }
}