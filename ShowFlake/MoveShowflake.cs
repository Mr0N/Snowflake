using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;

namespace ShowFlake
{
    class MoveShowflake : IMove
    {
        readonly static List<Window> windowLs;
        static MoveShowflake()
        {
            if (windowLs == null) 
                windowLs = new List<Window>();
            if (random == null) random = new Random();
        }
       
        public void Add(Window window)
        {
            windowLs.Add(window);
        }
        public static void Run()
        {
           
            Task task = new Task(() => Worker());
            task.Start();
        }
        static Random random;
        private static void Worker()
        {
            windowLs.ForEach(x => x.Dispatcher.Invoke(()=> Initialization(x)));
            while (true)
            {
                windowLs.ForEach(x =>
                {
                    x.Dispatcher.Invoke(() =>
                    {
                        Logica(x);
                    });
                });
                Thread.Sleep(40);
            }
        }
        static int count = 0;
        static void Initialization(Window wind)
        {
            wind.Left = random.Next(0, Convert.ToInt32(SystemParameters.PrimaryScreenWidth));
        }
        private static void Logica(Window window)
        {
            count++;
            if (SystemParameters.PrimaryScreenHeight < window.Top) window.Top = 0;
            if (SystemParameters.PrimaryScreenWidth < window.Left) window.Left = 0;
            if (window.Left < 0) window.Left = SystemParameters.PrimaryScreenWidth;
            window.Top+=1;
            if (count > 15)
            {
                if (random.Next(0, 10) == 1)
                    window.Left += 2;
                else window.Left -= 3;
                count = 0;
            }
        }
    }
}
