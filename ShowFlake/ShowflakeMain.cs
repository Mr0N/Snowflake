using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;
namespace ShowFlake
{
    class ShowflakeMain
    {
        public async Task ShowSnowflake(int count)
        {
          //  var ls = new List<Thread>();
            for (int i = 0; i < count; i++)
            {
                Thread thread = new Thread(() =>
                {
                    Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
                    var sf = new LogicaExecuteWindows1(new MoveShowflake());//WPF окно
                    sf.InitializeComponent();
                    sf.ShowDialog();
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.IsBackground = true;
                thread.Start();
            }
            await MoveShowflake.Run();

        }
    }
}
