using System.Net;

DownLoadFileInBackground4("https://dl.last.fm/static/1676566396/126178030/d58d17cfe1eda9efa44b71d32cda46fe0216f4c322fac8cecbfd6f1a837bf923/Death+Grips+-+Spread+Eagle+Cross+the+Block.mp3");

static void DownLoadFileInBackground4(string address)
{
    WebClient client = new WebClient();
    Uri uri = new Uri(address);

    // Specify a DownloadFileCompleted handler here...

    // Specify a progress notification handler.
    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressCallback4);

    client.DownloadFile(uri, "d:/s1erverdata.txt");
}

static void DownloadProgressCallback4(object sender, DownloadProgressChangedEventArgs e)
{
    // Displays the operation identifier, and the transfer progress.
    Console.WriteLine("{0}    downloaded {1} of {2} bytes. {3} % complete...",
        (string)e.UserState,
        e.BytesReceived,
        e.TotalBytesToReceive,
        e.ProgressPercentage);
    Console.ReadLine();
}