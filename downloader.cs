using System.Net;
internal partial class Downloader
{
    private static void Main(string[] args)
    {
        const string DownloadURL ="https://github.com/EXBO-Studio/stalcraft-database/archive/refs/heads/main.zip";
        const string FileName ="stalcraft-database-main";
        using  (WebClient client = new WebClient()){
            client.DownloadFile(DownloadURL,FileName);
        }
        
    }
}