/*
Вариант 5. Грызуны.
Класс для первой части – капибара.
Варианты свойств: вес, пол, возраст, имя.
Варианты методов: капибариться, плавать, чесать, получить совет
от капибары (статический).
Возможные классы иерархии: грызуны (базовый), хомяк, бобр,
мышь.
Возможный интерфейс: IMammal, дополнительный класс – кош-
ка.

 
 */
 
 using System;

namespace Practice1
{
    class Program
    {
        static void Main(string[] args)
        {
            Type[] usingTypes = new[] {typeof(Robent), typeof(Hamster), typeof(Beaver), typeof(Capibara), typeof(Mouse)};
            var menu = new Menu(usingTypes);
            menu.Run();
        }
    }
}