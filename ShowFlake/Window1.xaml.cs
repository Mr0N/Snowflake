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
    public partial class LogicaExecuteWindows1 : Window
    {

        public void Start()
        {
            this.move.Add(this);
        }
        public void RecolorSnowflake(Color color)
        {
            var source = SnowflakeImage.Source as BitmapSource;
            if (source == null) return;

            // Створюємо WriteableBitmap для редагування
            var writable = new WriteableBitmap(source);

            int stride = writable.PixelWidth * (writable.Format.BitsPerPixel / 8);
            byte[] pixels = new byte[writable.PixelHeight * stride];

            // Копіюємо пікселі
            writable.CopyPixels(pixels, stride, 0);

            // Проходимо по всіх пікселях
            for (int i = 0; i < pixels.Length; i += 4)
            {
                byte b = pixels[i];
                byte g = pixels[i + 1];
                byte r = pixels[i + 2];
                byte a = pixels[i + 3];

                // Міняємо тільки непрозорі пікселі (альфа > 0)
                if (a > 0)
                {
                    pixels[i] = color.B;     // Blue
                    pixels[i + 1] = color.G; // Green
                    pixels[i + 2] = color.R; // Red
                    pixels[i + 3] = color.A; // Alpha
                }
            }

            // Записуємо назад
            writable.WritePixels(new Int32Rect(0, 0, writable.PixelWidth, writable.PixelHeight), pixels, stride, 0);

            // Оновлюємо Image
            SnowflakeImage.Source = writable;
        }
        public IMove move { private set; get; }
        private static Random _random = new Random();

        public Color GetRandomColor()
        {
            // Генеруємо RGB
            byte r = (byte)_random.Next(50, 256); // мінімум 50 щоб не було занадто темно
            byte g = (byte)_random.Next(50, 256);
            byte b = (byte)_random.Next(50, 256);

            return Color.FromRgb(r, g, b);
        }
        public LogicaExecuteWindows1(IMove move)
        {
            InitializeComponent();  // ❗️Це важливо
            this.move = move;
            this.Start();
            RecolorSnowflake(this.GetRandomColor());
            // var source = SnowflakeImage.Source as BitmapSource;

        }
    }
}
