using System;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {            
            StringBuilder txt = new StringBuilder();
            Thread t = new Thread(() =>
            {
                DemoThread(1);
            });
            t.Start();
            Thread t2 = new Thread(() =>
            {
                DemoThread(2);
            });
            t2.Start();
            Thread t3 = new Thread(() =>
            {
                DemoThread(3);
            });
            t3.Start();
        }
        static string DemoThread(int threadIndex)
        {
            int begin = threadIndex * 10;
            int end = begin + 10;
            StringBuilder txt = new StringBuilder();
            for (int i = begin; i < end; i++)
            {
                Console.WriteLine(threadIndex + " - " + i);
            }
            return txt.ToString();
        }
    }
}
