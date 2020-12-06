using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Threading.Tasks;

namespace ShowFlake
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class SF : Window
    {
        
        public void Start() =>  this.move.Add(this);

        public IMove move {private set; get; }
        public SF(IMove move)
        {
            this.move = move;
            this.Start();
           
        }
    }
}
