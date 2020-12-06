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
            Thread thread = new Thread(() =>
            {
                var sf = new SF();//WPF окно
                    sf.ShowDialog();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        public Work()
        {
        }
    }
}
