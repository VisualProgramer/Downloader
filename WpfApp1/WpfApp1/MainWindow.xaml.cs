using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            string Path = "d:/3111Death Grips - Get Got.mp3";

            string URL = "https://dl.last.fm/static/1676566396/131211148/eea8a6042d14b77ceac68ed1726565bf8ec5dac251120e7e0b8465189dce5556/Death+Grips+-+Get+Got.mp3";

            var uri = new Uri(URL);
            var client = new WebClient();

            client.DownloadFile(uri, Path);
        }
    }
}
