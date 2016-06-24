using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Asyn2
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayValue();
            Console.ReadKey();
        }

        public static async void DisplayValue() {
            //此处会开新线程处理GetValueAsync任务，并等新线程中的任务执行完毕执行下面的打印方法
            double result = await GetValueAsync(1, 10);  
            Console.WriteLine("Value is : " + result);
        }

        //异步执行任务，返回值是一个Task
        public static Task<int> GetValueAsync(int num1, int num2) {
            Thread.Sleep(5000);
            return Task.Run(()=> {
                int sum = 0;
                for (int i = num1; i <= num2; i++) {
                    sum += i;
                }
                return sum;
            });
        }
    }
}
