using BLL.Services;
using DLL.Context;
using DLL.IRepository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
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

namespace Downloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DownloadManager _downloadManager;
        public MainWindow(DownloadManager downloadManager)
        {
            InitializeComponent();
            _downloadManager = downloadManager;
            lbMyFiles.DataContext = _downloadManager;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAddURL_Click(object sender, RoutedEventArgs e)
        {
            _downloadManager.AddMyFile(txtURL.Text);
            txtURL.Text = null;
        }

        private void btnDeleteURL_Click(object sender, RoutedEventArgs e)
        {
            var file = lbMyFiles.SelectedItem as MyFile;
            _downloadManager.DeleteMyFile(file.Id);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {

            _downloadManager.IsPause = false;
            _downloadManager.StartDownload(int.Parse(txtThreads.Text));

        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {           
            _downloadManager.IsPause= true;
        }

        private void btnAddThread_Click(object sender, RoutedEventArgs e)
        {
            int countThreads = int.Parse(txtThreads.Text);
            if (countThreads <100)
            {
                txtThreads.Text = (++countThreads).ToString();
            }

        }

        private void btnDelThread_Click(object sender, RoutedEventArgs e)
        {
            int countThreads = int.Parse(txtThreads.Text);
            if (countThreads > 1)
            {
                txtThreads.Text = (--countThreads).ToString();
            }
        }
    }
}
