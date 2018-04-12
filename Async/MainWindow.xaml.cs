using Async.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Async
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool mLive = true;
        private bool mPaused = false;
        private object mSyncObj = new object();
        private World mWorld = new World();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var ticks = Task.Factory.StartNew(() =>
            {
                while (mLive)
                {
                    mWorld.Tick(0.100f);
                    Thread.Sleep(100);
                }
            });

            var rendering = Task.Factory.StartNew(() =>
            {
                List<Ellipse> ellipses = new List<Ellipse>();

                while (mLive)
                {
                    var balls = mWorld.Balls;

                    var cnt = balls.Count();

                    while (ellipses.Count != cnt)
                        if (ellipses.Count < cnt)
                        {
                            ellipses.Add(new Ellipse() { Fill = new SolidColorBrush(Colors.Red) });
                        }
                        else
                        {
                            ellipses.RemoveAt(ellipses.Count - 1);
                        }

                    var num = 0;

                    foreach (var a in balls)
                    {
                        ellipses[num].Width = a.Radius;
                        ellipses[num].Height = a.Radius;
                        ellipses[num].Margin = new Thickness(a.X.X - a.Radius, a.X.Y - a.Radius, 0.0, 0.0);
                    }
                }
            });
        }

        private void View_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
