using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Await
{
    class Program
    {
        static void Main(string[] args)
        {
            //声明一个Func的委托，有一个传入参数和一个返回值
            Func<int,int> func = (int a) => {
                Console.WriteLine("run async,sleep 3 seconds");
                Thread.Sleep(3000);
                //返回小于参数的数值之和
                int sum = 0;
                for (int i = 0; i <= a; i++) {
                    sum += i;
                }
                return sum;
            };

            //声明一个Action的委托，用于方法执行完成之后的回调
            Action<int> back = (reslut) => {
                Console.WriteLine("the result is {0}", reslut);
            };

            //在异步线程执行方法
            RunAsync<int>(func,back);

            Console.ReadKey();
        }

        //在异步线程执行方法(没有返回值，有两个传入参数:func委托和action委托)
        public static async void RunAsync<TResult>(Func<int,TResult> function, Action<TResult> callback) {
            //重新声明一个返回值为Task<TResult>的func委托
            Func<Task<TResult>> taskFunc = () =>
            {
                return Task.Run(() =>
                {
                    return function(6);
                });
            };

            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

            //调用上面声明的委托,await只有在返回值为Task<>的方法中可以使用
            //此处开启新线程
            TResult rlt = await taskFunc();

            //等待上面线程执行完毕执行下面的回调方法
            if (callback != null) {
                callback(rlt);
            }
        }
    }
}
