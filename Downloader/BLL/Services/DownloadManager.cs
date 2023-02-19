using DLL.IRepository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class DownloadManager : INotifyPropertyChanged
    {
        private readonly MyFileRepository _myFileRepository;
        private List<MyFile> myFiles;
        private Semaphore pool;
        private AutoResetEvent autoResetEvent;
        public bool IsPause = false;
        private int filesCount;
        public event PropertyChangedEventHandler? PropertyChanged;


        public List<MyFile> MyFiles
        {
            get { return myFiles; }
            set
            {
                myFiles = value;
                OnPropertyChanged();
            }
        }
        public Thread[] threads { get; set; }
        public DownloadManager(MyFileRepository myFileRepository)
        {
            _myFileRepository = myFileRepository;
            myFiles = _myFileRepository.GetFromCondition(x => x.Id > 0) as List<MyFile>;
        }

        protected void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public void AddMyFile(string link)
        {
            var webClient = new WebClient();
            var myFile = new MyFile();
            webClient.OpenRead(link);

            string header_contentDisposition = webClient.ResponseHeaders["content-disposition"];

            myFile.FileName = new ContentDisposition(header_contentDisposition).FileName;
            myFile.FileSize = Math.Round(Convert.ToInt32(webClient.ResponseHeaders["Content-Length"]) * Math.Pow(10, -6), 2);
            myFile.Path = "d:/Download/" + myFile.FileName;
            myFile.URL = new Uri(link);
            myFile.IsDownloaded = false;
            myFile.DownloadDate = DateTime.Now;

            _myFileRepository.Creat(myFile);
            MyFiles = _myFileRepository.GetFromCondition(x => x.Id > 0) as List<MyFile>;
        }
        public void DeleteMyFile(int Id)
        {
            _myFileRepository.Delete(Id);
            MyFiles = _myFileRepository.GetFromCondition(x => x.Id > 0) as List<MyFile>;
        }
        public void StartDownload(int threadsCount)
        {
            filesCount = _myFileRepository.GetFromCondition(x => x.IsDownloaded == false).Count();

            threads = new Thread[filesCount];
            pool = new Semaphore(threadsCount, filesCount);
            autoResetEvent = new AutoResetEvent(false);
            int i = 0;

            foreach (var item in MyFiles)
            {
                threads[i] = new Thread(Task);
                threads[i].IsBackground = true;
                threads[i].Start(item);
                i++;
            }

        }
        void Task(object item)
        {
            var myFile = item as MyFile;
            pool.WaitOne();
            var webClient = new WebClient();

            webClient.DownloadFileAsync(myFile.URL, myFile.Path);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);

            void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
            {
                myFile.Progress = e.ProgressPercentage;
            }
            void webClient_DownloadFileCompleted(object? sender, AsyncCompletedEventArgs e)
            {
                lock (this)
                {
                    DeleteMyFile(myFile.Id);
                }
                if (IsPause)
                {
                    autoResetEvent.WaitOne();
                }
                pool.Release();
            }
        }
    }
}
