﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ServerApp;
using ServerApp.Models;

namespace WpfApp
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public void GetAllProducts()
        {
            ServerApp.WPFfunctions.GetAllProducts();
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
