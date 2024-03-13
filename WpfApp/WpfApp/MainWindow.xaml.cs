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
        //выбранный элемент
        private int SelectedItemID = -1;
        
        //для книг
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

        /// <summary>
        /// Добавить книгу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBook(object sender, RoutedEventArgs e)
        {
            try
            {
                int argPages;
                Int32.TryParse(AddPage.Text.ToString(), out argPages);
                WPFfunctions.AddBook(AddBookname.Text, AddAuthor.Text, argPages);
                RefreshElemTableBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Удалить книгу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBook(object sender, RoutedEventArgs e)
        {
            BooksDTO book = WPFfunctions.GetInfoBook(SelectedItemID);
            int ID = book.ID;
            WPFfunctions.DeleteBook(ID);
            RefreshElemTableBooks();
        }

        //Изменить книгу
        private void UpdateBook(object sender, RoutedEventArgs e)
        {

        }
        
        /// <summary>
        /// Изменение выбранного элемента в таблице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ElemTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //SelectedItemID = ElemTable.SelectedItem.ID;
        }

        //вспомогательные методы
        /// <summary>
        /// Обновить таблицу (книги)
        /// </summary>
        private void RefreshElemTableBooks()
        {
            List<BooksDTO> books = WPFfunctions.GetAllBooks();
            ElemTable.ItemsSource = books;
        }


        /// <summary>
        /// Проверка на ввод числа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckIfNum(object sender, KeyEventArgs e)
        {
            
            string str = AddPage.Text;
            if (e.Key <= Key.D9 && e.Key >= Key.D0){
                return;
            } 
            if (e.Key == Key.Back)
            {
                return;
            }
            else e.Handled = true; // отклоняем ввод
        }



        //загрузка настроек по умолчанию
        public MainWindow()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Загрузка таблицы по умолчанию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ElemTableLoaded(object sender, RoutedEventArgs e)
        {
            RefreshElemTableBooks();
        }

        /// <summary>
        /// Загрузка списков книг по умолчанию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BookListLoaded(object sender, RoutedEventArgs e)
        {
            (sender as ListBox).Items.Clear();
            List<BooksDTO> books = WPFfunctions.GetAllBooks();
            foreach (BooksDTO book in books)
            {
                (sender as ListBox).Items.Add(book.BookName);
            }
        }

        
    }
}
