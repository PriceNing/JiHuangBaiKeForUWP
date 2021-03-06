﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using JiHuangBaiKeForUWP.Model;

namespace JiHuangBaiKeForUWP.View.SettingChildPage
{
    /// <summary>
    /// 意见与建议
    /// </summary>
    public sealed partial class FeedbackChildPage : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Global.GetOsVersion() >= 16299)
            {
				var dimGrayAcrylicBrush = new AcrylicBrush
				{
					BackgroundSource = AcrylicBackgroundSource.HostBackdrop,
					FallbackColor = Colors.Transparent,
					TintColor = Global.TinkColor,
					TintOpacity = Global.TinkOpacity
				};
                RootGrid.Background = dimGrayAcrylicBrush;
            }
        }

        public FeedbackChildPage()
        {
            this.InitializeComponent();
        }

        private void QQTextBoxNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            StringProcess.QqNumTextCheck(textbox);
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(FeedbackTextBox.Text))
            {
                var contentDialog = new ContentDialog()
                {
                    Title = "这样子不行啊...",
                    Content = "您还没有输入任何内容呢~",
                    PrimaryButtonText = "我知道了",
                    FullSizeDesired = false,
                    Background = null,
                    BorderThickness = new Thickness(0),
                };
                Global.ShowDialog(contentDialog);
            }
            else
            {
                await ReportError(FeedbackTextBox.Text, QqTextBox.Text);
            }
        }

        public static async Task ReportError(string feedbackText, string qqText)
        {
            const string to = "351765204@qq.com";
            const string subject = "《饥荒百科全书 by tpxxn》意见与建议";
            var body = feedbackText;
            if (qqText != null)
                body += " QQ：" + qqText;
            await CallExternalContent.OpenEmailComposeAsync(to, subject, body);
        }

        private async void FeedbackButton_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var launcher = Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault();
            await launcher.LaunchAsync();
        }

        private async void GithubButton_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("https://github.com/tpxxn/JiHuangBaiKeForUWP/issues"));
        }

        private async void QqButton_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("http://shang.qq.com/wpa/qunwpa?idkey=c7bd1fac7312bb1afbfde97bec4095e68465b04dc1b262759518cbb876a3bae1"));
        }

    }
}
