using System.Runtime.CompilerServices;

namespace MainCollection
{
    internal class Program
    {
        private static OtusDictionary dict = new OtusDictionary();
        static void Main(string[] args)
        {

            TestAddKeysAndValueAndGetValue();
            TestGetNonExistentValue();
            TestAddNullValue();
        }

        static void TestAddKeysAndValueAndGetValue()
        {
            try
            {
                dict.Add(1, "Привет");
                dict.Add(2, "Как дела");
                dict.Add(10, "Main");
                dict.Add(33, "three");

                Console.WriteLine(dict.Get(1));
                Console.WriteLine(dict.Get(2));
                Console.WriteLine(dict.Get(10));
                Console.WriteLine(dict.Get(33));

                dict[4] = "тру-ля-ля";
                Console.WriteLine(dict[4]);

                ShowResultTest(true);
            }
            catch(Exception ex)
            {
                ShowResultTest(false, ex.Message);
            }
        }

        static void TestGetNonExistentValue()
        {
            try
            {
                string? res1 = dict[500];
                string? res2 = dict[20];

                if (res1 == null && res2 == null)
                {
                    ShowResultTest(true);
                }
                else 
                {
                    ShowResultTest(false, "Вернулось не null");
                }
            }
            catch (Exception ex)
            {
                ShowResultTest(false, ex.Message);
            }
        }

        static void TestAddNullValue()
        {
            try
            {
                dict.Add(5, null);
                ShowResultTest(false,
                    "При добавлении null в качестве значения не было вызвано исключение ArgumentNullException");
            }
            catch (ArgumentNullException ex)
            {
                ShowResultTest(true);
            }
            catch (Exception ex)
            {
                ShowResultTest(false, ex.Message);
            }
        }

        static void ShowResultTest(bool res, string message = "", [CallerMemberName] string methodName = "")
        {
            if (res)
            {
                Console.WriteLine($"{methodName}: Проиден");
            }
            else
            {
                Console.WriteLine($"{methodName}: Провален ({message})");
            }
        }
    }
}