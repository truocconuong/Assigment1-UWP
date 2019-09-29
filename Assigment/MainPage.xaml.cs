using Assigment.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Assigment
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var hyperlink = sender as HyperlinkButton;
            if (hyperlink != null)
            {
                switch (hyperlink.Tag)
                {
                    case "Login":
                        this.MainContent.Navigate(typeof(Login));
                        break;
                    case "Register":
                        this.MainContent.Navigate(typeof(Register));
                        break;
                    case "Information":
                        this.MainContent.Navigate(typeof(Information));
                        break;
                    case "allplaylist":
                        this.MainContent.Navigate(typeof(AllPost));
                        break;
                }
            };
        }

        private void ToogleMenu_OnClick(object sender, RoutedEventArgs e)
        {
            this.SplitView.IsPaneOpen = !this.SplitView.IsPaneOpen;
        }
    }
}
