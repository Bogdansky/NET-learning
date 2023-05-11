using System;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace GameOfLife
{
    public partial class MainWindow : Window
    {
        private Grid mainGrid;
        DispatcherTimer timer;   //  Generation timer
        private int genCounter;
        private AdWindow[] adWindows;


        public MainWindow()
        {
            InitializeComponent();
            mainGrid = new Grid(MainCanvas);

            timer = new DispatcherTimer();
            timer.Tick += OnTimer;
            timer.Interval = TimeSpan.FromMilliseconds(200);

            adWindows = new AdWindow[2];
        }

        private void StartAd()
        {
            for (int i = 0; i < 2; i++)
            {
                if (adWindows[i] == null) 
                {
                    adWindows[i] = new AdWindow(this);
                    adWindows[i].Closed += AdWindowOnClosed;
                    adWindows[i].Top = Top + (330 * i) + 70;
                    adWindows[i].Left = Left + 240;
                    adWindows[i].Show();
                }
            }
        }

        private void AdWindowOnClosed(object sender, EventArgs eventArgs)
        {
            var adWindow = sender as AdWindow;

            for (int i = 0; i < 2; i++)
            {
                if (adWindow == adWindows[i])
                {
                    adWindows[i].Closed -= AdWindowOnClosed;
                    adWindows[i] = null;
                    break;
                }
            }
        }

        private void Button_OnClick(object sender, EventArgs e)
        {
            if (!timer.IsEnabled)
            {
                timer.Start();
                ButtonStart.Content = "Stop";
                StartAd();
            }
            else
            {
                timer.Stop();
                ButtonStart.Content = "Start";
            }
        }

        private void OnTimer(object sender, EventArgs e)
        {
            mainGrid.Update();
            genCounter++;
            lblGenCount.Content = "Generations: " + genCounter;
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Clear();
        }
    }
}
