using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _6._16Asyn
{
    class Program
    {
        static void Main(string[] args)
        {
            //声明一个委托
            Func<int, int, int> f = OneClass.SlowFunction;

            //Func<int, int, int> f = (int x,int y) =>
            //{
            //    Thread.Sleep(3000);
            //    return x + y;
            //};

            //异步线程执行完毕调用的回掉方法
            AsyncCallback back = async => {
                Console.WriteLine("asyn over@@@@@@@@");
                Console.WriteLine("current thread number is {0}",Thread.CurrentThread.ManagedThreadId);

                //获取异步操作的结果
                int result = f.EndInvoke(async);
                Console.WriteLine("result is {0}", result);
            };

            //开启异步线程
            IAsyncResult asResult = f.BeginInvoke(5, 3, back, null);

            //判断线程是否执行完毕
            while (!asResult.IsCompleted) {
                Console.WriteLine("ttttttttt");
                Thread.Sleep(1000);
            }
            Console.ReadKey();
        }
    }

    public class OneClass {
        //这个方法是在异步线程中使用的 
        public static int SlowFunction(int x, int y) {
            Thread.Sleep(3000);
            return x + y;
        }
    }
}
