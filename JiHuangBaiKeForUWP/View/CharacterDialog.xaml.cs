﻿using System;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using JiHuangBaiKeForUWP.Model;
using LiveCharts;

namespace JiHuangBaiKeForUWP.View
{
    /// <summary>
    /// Character对话框
    /// </summary>
    public sealed partial class CharacterDialog : Page
    {
        public CharacterDialog(Character c)
        {
            this.InitializeComponent();
            CharacterImage.Source = new BitmapImage(new Uri(c.Picture));
            CharacterName.Text = c.Name;
            CharacterEnName.Text = c.EnName;
            if (c.Motto != null)
            {
                CharacterMotto.Text = c.Motto;
            }
            Hunger.Values = new ChartValues<double>(new[] { c.Hunger });
            Health.Values = new ChartValues<double>(new[] { c.Health });
            Sanity.Values = new ChartValues<double>(new[] { c.Sanity });
            CharacterDamage.Text = $"伤害：{c.Damage} 倍";
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
            CharacterIntroduction.Text = c.Introduce;
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
