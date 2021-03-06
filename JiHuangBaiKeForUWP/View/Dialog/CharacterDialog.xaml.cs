﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using JiHuangBaiKeForUWP.Model;

namespace JiHuangBaiKeForUWP.View.Dialog
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class CharacterDialog : Page
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
                RootScrollViewer.Background = dimGrayAcrylicBrush;
            }
            base.OnNavigatedTo(e);
            Global.FrameTitle.Text = "人物详情";
            if (e.Parameter != null)
            {
                LoadData((Character)e.Parameter);
            }
            var imageAnimation = ConnectedAnimationService.GetForCurrentView().GetAnimation("Image");
            imageAnimation?.TryStart(CharacterImage);
        }

        public CharacterDialog()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        public void LoadData(Character c)
        {
            CharacterImage.Source = new BitmapImage(new Uri(c.Picture));
            CharacterName.Text = c.Name;
            CharacterEnName.Text = c.EnName;
            if (c.Motto != null)
            {
                CharacterMotto.Text = c.Motto;
            }
            else
            {
                CharacterMotto.Visibility = Visibility.Collapsed;
            }
            CharacterHealth.Value = c.Health;
            CharacterHealth.BarColor = Global.ColorGreen;
            CharacterHunger.Value = c.Hunger;
            CharacterHunger.BarColor = Global.ColorKhaki;
            CharacterSanity.Value = c.Sanity;
            CharacterSanity.BarColor = Global.ColorRed;
            if (c.Name == "阿比盖尔")
            {
                CharacterDamage.Visibility = Visibility.Collapsed;
                CharacterHealth.LabelWidth = 75;
                CharacterDayDamage.Visibility = Visibility.Visible;
                CharacterDayDamage.Value = c.DamageDay;
                CharacterDayDamage.BarColor = Global.ColorBlue;
                CharacterDuskDamage.Visibility = Visibility.Visible;
                CharacterDuskDamage.Value = c.DamageDusk;
                CharacterDuskDamage.BarColor = Global.ColorBlue;
                CharacterNightDamage.Visibility = Visibility.Visible;
                CharacterNightDamage.Value = c.DamageNight;
                CharacterNightDamage.BarColor = Global.ColorBlue;
            }
            if (c.Name == "海獭伍迪")
            {
                CharacterDamage.Text = $"伤害：{c.Damage} 点";
                CharacterHealth.LabelWidth = 45;
                CharacterSanity.LabelWidth = 45;
                CharacterLog.Visibility = Visibility.Visible;
                CharacterLog.Value = c.LogMeter;
                CharacterLog.BarColor = Global.ColorPurple;
            }
            else
            {
                CharacterDamage.Text = $"伤害：{c.Damage} 倍";
            }
            if (c.Descriptions != null)
            {
                CharacterDescription1.Text = c.Descriptions[0];
                CharacterDescription1.Visibility = Visibility.Visible;
                if (c.Descriptions.Count >= 2)
                {
                    CharacterDescription2.Text = c.Descriptions[1];
                    CharacterDescription2.Visibility = Visibility.Visible;
                }
                if (c.Descriptions.Count == 3)
                {
                    CharacterDescription3.Text = c.Descriptions[2];
                    CharacterDescription3.Visibility = Visibility.Visible;
                }
            }
            if (c.Unlock != null)
            {
                CharacterUnlockStackPanel.Visibility = Visibility.Visible;
                UnlockTextBlock.Text = c.Unlock;
            }
            CharacterIntroduction.Text = c.Introduce;
        }

        private void ScrollViewer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var list = new List<DependencyObject>();
            Global.FindChildren(list, (ScrollViewer)sender);
            var scrollViewerGrid = (from dependencyObject in list where dependencyObject.ToString() == "Windows.UI.Xaml.Controls.Grid" select dependencyObject.GetHashCode()).FirstOrDefault();
            if (e.OriginalSource.GetHashCode() == scrollViewerGrid)
            {
                Global.App_BackRequested();
            }
        }
    }
}
