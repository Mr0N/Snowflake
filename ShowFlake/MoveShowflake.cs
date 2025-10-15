using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;

namespace ShowFlake
{
    class MoveShowflake : IMove
    {
        readonly static ConcurrentBag<Window> _listWindows;
        static MoveShowflake()
        {
            if (_listWindows == null) 
                _listWindows = new ConcurrentBag<Window>();
            if (random == null) random = new Random();
        }
        static object objLock = new object();
        public void Add(Window window)
        {
            lock(objLock)
                _listWindows.Add(window);
        }
        public static async Task Run()
        {
            await Worker();
        }
        static Random random;
        private static async Task Worker()
        {
            //_listWindows.ForEach(window => );
            foreach (var window in _listWindows)
            {
                window.Dispatcher.Invoke(() => BeginSettings(window));
            }
            
            while (true)
            {
                foreach (var window in _listWindows)
                {
                    window.Dispatcher.Invoke(() =>
                    {
                        LogicaChangePositionWindow(window);
                    });
                }
                await Task.Delay(10);
            }
        }
        static int count = 0;
        static void BeginSettings(Window window)
        {
            window.Left = random.Next(0, Convert.ToInt32(SystemParameters.PrimaryScreenWidth));
        }
        private static void LogicaChangePositionWindow(Window window)
        {
            count++;
            if (SystemParameters.PrimaryScreenHeight < window.Top) window.Top = 0;
            if (SystemParameters.PrimaryScreenWidth < window.Left) window.Left = 0;
            if (window.Left < 0) window.Left = SystemParameters.PrimaryScreenWidth;
            window.Top+=1;
            if (count > 15)
            {
                if (random.Next(0, 10) == 1)
                    window.Left += 1;
                else window.Left -= 1;
                count = 0;
            }
        }
    }
}
