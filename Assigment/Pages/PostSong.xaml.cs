﻿using Assigment.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
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
    public sealed partial class PostSong : Page
    {
        private const string ApiUrl = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs";
        public PostSong()
        {
            this.InitializeComponent();


        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if(this.name.Text == "" && this.description.Text == "" && this.thumbnail.Text == "" && this.link.Text == "")
            {
                this.checkErrorAll.Text = "Du lieu khong duoc bo trong";
            }
            else if(this.name.Text == "")
            {
                this.checkErrorName.Text = "Ten khong duoc bo trong";
            }
            else if (this.name.Text.Length>=50)
            {
                this.checkErrorName.Text = "Ten khong duoc dai qua 50 ki tu";
            }
            else if (Regex.IsMatch(this.link.Text, ".mp3$") == false)
            {
                this.checkerrorMp3.Text = "Link khong dung dinh dang";
            }
            else if (this.thumbnail.Text =="")
            {
                Debug.WriteLine("Thumnail khong duoc de trong");
            }
            else if (this.link.Text == "")
            {
                this.checkerrorMp3.Text = "Link khong duoc de trong";
            }

            else
            {
                this.checkerrorMp3.Text = "";
                this.checkErrorName.Text = "";
                this.checkErrorAll.Text = "";
                var song = new Song
                {
                    name = this.name.Text,
                    description = this.description.Text,
                    singer = this.singer.Text,
                    author = this.author.Text,
                    thumbnail = this.thumbnail.Text,
                    link = this.link.Text
                };
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile sampleFile = storageFolder.GetFileAsync("sample.txt").GetAwaiter().GetResult();
                var token = Windows.Storage.FileIO.ReadTextAsync(sampleFile).GetAwaiter().GetResult();

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
                HttpContent content = new StringContent(JsonConvert.SerializeObject(song), Encoding.UTF8,
                    "application/json");
                Task<HttpResponseMessage> httpRequestMessage = httpClient.PostAsync(ApiUrl, content);
                String responseContent = httpRequestMessage.Result.Content.ReadAsStringAsync().Result;
                Debug.WriteLine("Response: " + responseContent);
            }


        }
    }
}
