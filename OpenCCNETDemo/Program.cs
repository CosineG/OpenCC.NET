using System;
using OpenCCNET;

namespace OpenCCNETDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = "为我的电脑换了内存，开启电脑后感觉网络速度更快了";
            Console.WriteLine(s.ToHantFromHans());
            Console.WriteLine(s.ToTWFromHans(true));
            Console.WriteLine(s.ToTWFromHans());
            Console.WriteLine(s.ToHKFromHans());
            var t = "為我的電腦換了記憶體，開啟電腦后感覺網路速度更快了";
            Console.WriteLine(t.ToHantFromTW());
            Console.WriteLine(t.ToHantFromTW(true));
            Console.WriteLine(t.ToHansFromTW());
            Console.WriteLine(t.ToHansFromTW(true));
            Console.ReadLine();
        }
    }
}