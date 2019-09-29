using Assigment.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
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
    public sealed partial class AllPost : Page
    {
        private string API_ALL_SONG = "https://2-dot-backup-server-003.appspot.com/_api/v2/songs";
        private MediaPlayer mediaPlayer;

        public AllPost()
        {

            this.InitializeComponent();
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = storageFolder.GetFileAsync("sample.txt").GetAwaiter().GetResult();
            var token = Windows.Storage.FileIO.ReadTextAsync(sampleFile).GetAwaiter().GetResult();

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);
            var responseContent = client.GetAsync(API_ALL_SONG).Result.Content.ReadAsStringAsync().Result;
            var convertSongs = JsonConvert.DeserializeObject<List<Song>>(responseContent);
            ls1.ItemsSource = convertSongs;
        }
        private void Start_Song(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            mediaPlayer = new MediaPlayer();
            mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"{button.Tag}", UriKind.RelativeOrAbsolute));
            mediaPlayer.Play();
        }
        private void Stop_Song(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }
    }
}
