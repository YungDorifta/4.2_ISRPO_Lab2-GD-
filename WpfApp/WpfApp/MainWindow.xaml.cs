using System;
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
using WpfApp.DTOs;


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

        /// <summary>
        /// Просмотр книг
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeeAllBooks(object sender, RoutedEventArgs e)
        {
            if (ElemTable.IsLoaded)
            {
                List<BooksDTO> books = WPFfunctions.GetAllBooks();
                ElemTable.ItemsSource = books;
            }
        }

        //Добавить книгу
        private void AddBook(object sender, RoutedEventArgs e)
        {

        }

        //Удалить книгу
        private void DeleteBook(object sender, RoutedEventArgs e)
        {

        }

        //Изменить книгу
        private void UpdateBook(object sender, RoutedEventArgs e)
        {

        }
        
        /// <summary>
        /// Загрузка таблицы по умолчанию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ElemTable_Loaded(object sender, RoutedEventArgs e)
        {
            List<BooksDTO> books = WPFfunctions.GetAllBooks();
            ElemTable.ItemsSource = books;
        }

        /// <summary>
        /// Загрузка списков книг по умолчанию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefreshBookList(object sender, RoutedEventArgs e)
        {
            List<BooksDTO> books = WPFfunctions.GetAllBooks();
            foreach (BooksDTO book in books)
            {
                (sender as ListBox).Items.Add(book.BookName);
            }
        }

        private void CheckIfNum(object sender, KeyEventArgs e)
        {
            int val;
            string str = PageBox.Text;
            if (Int32.TryParse(str, out val)){
                return;
            } 
            if (e.Key == Key.Back)
            {
                return;
            }
            else e.Handled = true; // отклоняем ввод
        }
    }
}
