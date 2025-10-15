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
        readonly static List<Window> _listWindows;
        static MoveShowflake()
        {
            if (_listWindows == null) 
                _listWindows = new List<Window>();
            if (random == null) random = new Random();
        }
       
        public void Add(Window window)
        {
            _listWindows.Add(window);
        }
        public static async Task Run()
        {
            await Worker();
        }
        static Random random;
        private static async Task Worker()
        {
            _listWindows.ForEach(window => window.Dispatcher.Invoke(()=> BeginSettings(window)));
            while (true)
            {
                _listWindows.ForEach(window =>
                {
                    window.Dispatcher.Invoke(() =>
                    {
                        LogicaChangePositionWindow(window);
                    });
                });
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
                    window.Left += 2;
                else window.Left -= 3;
                count = 0;
            }
        }
    }
}
