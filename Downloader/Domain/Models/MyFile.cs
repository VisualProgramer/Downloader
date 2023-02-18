using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MyFile : INotifyPropertyChanged
    {
        private int progress;
        public int Id { get; set; }
        public string FileName { get; set; }
        public double FileSize { get; set; }
        public string Path { get; set; }
        public int Progress 
        {
            get { return progress; }
            set
            {
                progress = value;
                OnPropertyChanged();
            }
        }
        public Uri URL { get; set; }
        public bool IsDownloaded { get; set; }
        public DateTime DownloadDate { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
