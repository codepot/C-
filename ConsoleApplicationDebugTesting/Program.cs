using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationDebugTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 3;
            int a = 5;
            int n = 4;
            n--;
            ++n;
            i=++n;
            i += n++;
            i += ++n;
            a += a;
            a *= 2;
            i -= 5;
            a += --i;
            a = n--;
            Console.WriteLine("i="+i);
            Console.WriteLine("a=" + a);
            Console.WriteLine("n=" + n);

        }
    }
}
