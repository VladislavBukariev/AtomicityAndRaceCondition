using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AtomicityandRaceCondition
{
    class Program
    {
        struct Cube
        {
            public int size;
            public int square;
            public int volume;
        }

        static Cube cube = new Cube();
        const int sequenceCount = 20;
        object lockInstance = new object();

        static void Main(string[] args)
        {           
            Program obj = new Program();
            obj.AtomicityandRaceCondition();
        }

        private void AtomicityandRaceCondition()
        {            
            var evenThread = new Thread(updateData);                        
            var oddThread = new Thread(updateData);           
            var readThread = new Thread(() =>
            {
                for (int k = 1; k <= sequenceCount; k++)
                {
                    Console.WriteLine("==================================");
                    Console.WriteLine($"Size: {cube.size}");
                    Thread.Sleep(100);
                    Console.WriteLine($"Square: {cube.square}");
                    Thread.Sleep(100);
                    Console.WriteLine($"Volume: {cube.volume}");
                    Console.WriteLine();
                }
            });
            evenThread.Start(1);
            Thread.Sleep(100);
            oddThread.Start(2);

            readThread.Start();
            
            evenThread.Join();
            oddThread.Join();
            readThread.Join();
        }

        private void updateData(object startIndex)
        {           
            for (int i = ((int)startIndex); i <= sequenceCount; i += 2)
            {              
                cube.size = i;
                Thread.Sleep(200);
                cube.square = i * i;
                Thread.Sleep(200);
                cube.volume = i * i * i;
            }
        }
    }
}
