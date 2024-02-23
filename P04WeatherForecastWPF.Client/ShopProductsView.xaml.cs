﻿using P04WeatherForecastWPF.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace P04WeatherForecastWPF.Client
{
    /// <summary>
    /// Interaction logic for ShopProductsView.xaml
    /// </summary>
    public partial class ShopProductsView : Window
    {
        public ShopProductsView(ProductsViewModel productsViewModel)
        {
            DataContext = productsViewModel;
            InitializeComponent();
        }
    }
}
