using Assigment.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
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
    public sealed partial class Login : Page
    {
        private string LOGIN_URL = "https://2-dot-backup-server-003.appspot.com/_api/v2/members/authentication";
        public Login()
        {
            this.InitializeComponent();
        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            var member = new Member
            {
                email = this.Email.Text,
                password = this.Password.Password
            };
            var httpClient = new HttpClient();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(member), Encoding.UTF8,
                "application/json");
            Task<HttpResponseMessage> httpRequestMessage = httpClient.PostAsync(LOGIN_URL, content);
            String responseContent = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
            JObject jsonJObject = JObject.Parse(responseContent);
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            Windows.Storage.StorageFile sampleFile = storageFolder.CreateFileAsync("sample.txt",
                Windows.Storage.CreationCollisionOption.ReplaceExisting).GetAwaiter().GetResult();

            Windows.Storage.FileIO.WriteTextAsync(sampleFile, jsonJObject["token"].ToString()).GetAwaiter().GetResult();
            Debug.WriteLine(sampleFile.Path);

        }
    }

}
