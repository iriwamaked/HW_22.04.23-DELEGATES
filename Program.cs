using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HW_22._04._23
{
    public delegate void MyDelegate(string str);

    //Для задания 2
    public delegate int CalculateDelegate(int x, int y);
    class Calculate
    {
        public int Sum(int x, int y) { return x + y; }
        public int Sub(int x, int y) { return x - y; }
        public int Mult(int x, int y) { return x * y; }
    }
    //Для задания 3
    class CheckNumber
    {
        public bool IsPair(int number) { return number % 2 == 0; }
        public bool IsNotPair(int number) { return number % 2 != 0; }
        public bool IsPrime(int number)
        {
            if (number < 2) return false;
            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        public bool IsFibonacci(int number)
        {
            if (number == 0 || number == 1) return true;
            int a = 0; int b = 1; int c;
            while (b <= number)
            {
                c = a + b;
                if (c == number) return true;
                a = b; b = c;
            }
            return false;
        }
    }
    internal class Program
    {
        //Для задания 1
        public static void ShowStr(string str)
        {
            Console.WriteLine(str);
        }

        public static void CountStrLetters(string str)
        {
            int count = str.Length;
            Console.WriteLine($"The string has {count} letters.");

        }

        public static void ReverseStr(string str)
        {
            char[] chars = str.ToCharArray();
            Array.Reverse(chars);
            Console.WriteLine(new string(chars));
        }

        //Для 3 задания
        static bool CheckNumberFunc(int number, Predicate<int> check)
            {
                bool result=check(number);
                return result;
            }


        static void Main(string[] args)
        {
            int choice=0;
            do
            {
                Console.WriteLine("\n\tВыберите задание:");
                Console.WriteLine("\n\t1-Приложение, которое отображает текстовое сообщение;");
                Console.WriteLine("\n\t2+4-Приложение для выполнения арифметических операций: сложение, отнимание, умножение;" +
                    "4-реализуйте приложение из второго задания с помощью вызова Invoke");
                Console.WriteLine("\n\t3-Приложение для операций: проверка числа на парность, непарность, простое число," +
                    "число Фиббоначчи. Обязательно использовать делегат типа Predicate." );
                choice=Convert.ToInt32(Console.ReadLine());
                switch (choice) 
                {
                    case 1:
                        MyDelegate str = ShowStr;
                        str("The first static test message.");
                        str(Console.ReadLine());
                        str(Console.ReadLine());
                        string str2 = Console.ReadLine();
                        str(str2);
                        str += ReverseStr;
                        str += CountStrLetters;
                        str(str2);
                        break;
                    case 2:
                        Calculate calc = new Calculate();
                        Console.Write("\n\tВведите два числа: ");
                        int a=Convert.ToInt32(Console.ReadLine());
                        int b=Convert.ToInt32(Console.ReadLine());
                        Console.Write("\n\tВведите операцию для чисел (+, -, *): ");
                        char operation =Convert.ToChar(Console.ReadLine());
                        CalculateDelegate del = null;
                        switch (operation)
                        {
                            case '+':
                                del = calc.Sum;
                                break;
                            case '-':
                                del = calc.Sub;
                                break;
                            case '*':
                                del = calc.Mult;
                                break;
                            default:
                                Console.WriteLine("\n\tВы ввели недопустимую операцию. Допустимы только следующие:");
                                del = calc.Sum;
                                del += calc.Sub;
                                del+= calc.Mult;
                                foreach (CalculateDelegate i in del.GetInvocationList())
                                {
                                    Console.WriteLine("\n\t"+i.Method.Name);
                                }
                                del = null;
                                break;
                        }
                        try
                        {
                            Console.WriteLine($"\n\tРезультат: {del(a, b)}");
                            Console.WriteLine($"\n\tРезультат: {del.Invoke(a, b)}");//вызов через Invoke()
                        }
                        catch (Exception e) { Console.WriteLine("\n\tНеверно введенная операция" + e.Message); }
                        
                        break;
                 case 3:
                        Console.WriteLine("\n\tВведите число: ");
                        int number=Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("\n\tВведите операцию для проверки числа: 1 - Парное число, 2 - Непарное, 3 - Простое, 4 - Фиббоначчи" );
                        int choice2 = Convert.ToInt32(Console.ReadLine());
                        CheckNumber num = new CheckNumber();
                        Predicate<int> check= null;
                        switch(choice2)
                            {
                                case 1:
                                    check = num.IsPair;
                                    bool result=CheckNumberFunc(number, check);
                                    if (result) { Console.WriteLine($"\n\tЧисло {number} парное");}
                                    else { Console.WriteLine($"\n\tЧисло {number} непарное"); }
                                    break;
                                case 2:
                                    check= num.IsNotPair;
                                    bool result2 = CheckNumberFunc(number, check);
                                    if (result2) { Console.WriteLine($"\n\tЧисло {number} непарное"); }
                                    else { Console.WriteLine($"\n\tЧисло {number} парное"); }
                                    break;
                                case 3:
                                    check= num.IsPrime;
                                    bool result3=CheckNumberFunc(number, check);
                                    if (result3) { Console.WriteLine($"\n\tЧисло {number} простое"); }
                                    else { Console.WriteLine($"\n\tЧисло {number} не простое"); }
                                    break;
                                case 4:
                                    check = num.IsFibonacci;
                                    bool result4 = CheckNumberFunc(number, check);
                                    if (result4) { Console.WriteLine($"\n\tЧисло {number} является числом Фибоначчи"); }
                                    else { Console.WriteLine($"\n\tЧисло {number} не является числом Фибоначчи"); }
                                    break;
                                default: Console.WriteLine("\n\tВведена недопустимая операция, попробуйте снова.");
                                    break;
                            }
                        break;

                }
            } 
            while (choice>0&&choice<=3);
            
            
        }
    }
}
