using Assigment.Entity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Assigment.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Information : Page
    {
        private string INFORMATION_URL = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/information";
        public Information()
        {
            this.InitializeComponent();
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = storageFolder.GetFileAsync("sample.txt").GetAwaiter().GetResult();
            var token = Windows.Storage.FileIO.ReadTextAsync(sampleFile).GetAwaiter().GetResult();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var responseContent = client.GetAsync(INFORMATION_URL).Result.Content.ReadAsStringAsync().Result;
            Member member = Newtonsoft.Json.JsonConvert.DeserializeObject<Member>(responseContent);
            ls1.ItemsSource = member.firstName;

        }

    }
}
