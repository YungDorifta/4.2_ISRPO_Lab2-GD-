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


namespace WpfApp
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            TB.Text = "Not Loaded. Fuck.";
            TB.Text = WPFfunctions.GetAllBooks();

            /*
                TB.Text = WPFfunctions.AddBook("Новая книга", "Новый Автор", 147);
                TB.Text = "\n";
                WPFfunctions.DeleteBook(9);
                BooksDTO book = new BooksDTO { ID=12, Author="Новый Автор", BookName="Новая книга", Pages=150};
                WPFfunctions.UpdateInfoBook(book);
                TB.Text += "\n";
                TB.Text += WPFfunctions.GetAllBooks();
                TB.Text = WPFfunctions.GetInfoBooksWithAuthor("А. Пушкин");
            */
        }
    }
}
