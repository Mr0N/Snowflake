using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Threading;
using System.Threading.Tasks;
namespace ShowFlake
{
    class Work
    {
        public void Start(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Thread thread = new Thread(() =>
                {
                    Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
                    var sf = new SF(new MoveShowflake());//WPF окно
                    sf.InitializeComponent();
                    sf.ShowDialog();
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                MoveShowflake.Run();
            }

        }


        public Work()
        {
        }
    }
}
