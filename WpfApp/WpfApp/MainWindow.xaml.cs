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

//переделать удаление на унифицированное
//доделать все необходимые функции для остальных таблиц

namespace WpfApp
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //выбранный элемент
        private int SelectedItemID = -1;

        //тип просматриваемых элементов
        private object typeObject = new BooksDTO();



        //для всех типов объектов
        /// <summary>
        /// Удалить выбранный
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBook(object sender, RoutedEventArgs e)
        {
            if (SelectedItemID > -1)
            {
                WPFfunctions.DeleteObj(SelectedItemID, typeObject);
                RefreshElemTable();
                SelectedItemID = -1;
            }
            else throw new Exception("Элемент для удаления не выбран!");
        }
        
        //доделать, когда будет добавлена общая таблица и элементы WPF
        /// <summary>
        /// Изменение выбранного элемента в таблице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ElemTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ElemTable.SelectedItem != null)
                {
                    if (typeObject is BooksDTO)
                    {
                        BooksDTO selectedBook = ElemTable.SelectedItem as BooksDTO;
                        SelectedItemID = selectedBook.ID;
                        SelectedLabel.Content = "Выбранный элемент (книга): " + selectedBook.ID.ToString() + " '" + selectedBook.BookName.ToString().TrimEnd() + "' (" + selectedBook.Author.ToString().TrimEnd() + ", " + selectedBook.Pages.ToString() + " страниц)";
                        UpBookname.Text = selectedBook.BookName.ToString().TrimEnd();
                        UpAuthor.Text = selectedBook.Author.ToString().TrimEnd();
                        UpPages.Text = selectedBook.Pages.ToString();
                    }
                    else if (typeObject is ReadersDTO)
                    {
                        ReadersDTO selectedReader = ElemTable.SelectedItem as ReadersDTO;
                        SelectedItemID = selectedReader.ID;
                        SelectedLabel.Content = "Выбранный элемент (читатель): " + selectedReader.ID.ToString() + " " + selectedReader.FIO.ToString().TrimEnd();
                        //доделать для WPF элементов изменения
                    }
                    else if (typeObject is BookReadersDTO)
                    {
                        BookReadersDTO selectedBookReader = ElemTable.SelectedItem as BookReadersDTO;
                        SelectedItemID = selectedBookReader.ID;
                        //доделать для полного вывода информации
                        SelectedLabel.Content = "Выбранный элемент (запись в библиотеке): " + selectedBookReader.ID.ToString() + " ";
                        //доделать для WPF элементов изменения
                    }
                }
                else
                {
                    SelectedLabel.Content = "Выбранный элемент: отсутствует";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //для книг
        /// <summary>
        /// Просмотр всех книг
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeeAllBooks(object sender, RoutedEventArgs e)
        {
            if (ElemTable.IsLoaded)
            {
                typeObject = new BooksDTO();
                RefreshElemTable();
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
                RefreshElemTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        /// <summary>
        /// Изменить книгу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBook(object sender, RoutedEventArgs e)
        {
            if (UpBookname.Text != "" && UpAuthor.Text != "" && UpPages.Text != "")
            {
                int argUpdatedPages;
                if (Int32.TryParse(UpPages.Text, out argUpdatedPages) && SelectedItemID >= 0)
                {
                    BooksDTO UpBook = new BooksDTO { ID = SelectedItemID, BookName = UpBookname.Text, Author = UpAuthor.Text, Pages = argUpdatedPages };
                    WPFfunctions.UpdateInfoBook(UpBook);
                    RefreshElemTable();
                    SelectedItemID = -1;
                }
            }
        }
        


        //для читателей
        /// <summary>
        /// Просмотр всех читателей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeeAllReaders(object sender, RoutedEventArgs e)
        {
            if (ElemTable.IsLoaded)
            {
                typeObject = new ReadersDTO();
                RefreshElemTable();
            }
        }
        //добавить
        //изменить


        //для общей таблицы
        //просмотр всех записей
        //добавить
        //изменить


        //вспомогательные методы
        /// <summary>
        /// Обновить таблицу (ГОТОВО)
        /// </summary>
        private void RefreshElemTable()
        {
            try
            {
                List<Object> objects = WPFfunctions.GetAll(typeObject);
                ElemTable.ItemsSource = objects;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        /// <summary>
        /// Проверка на ввод числа (ГОТОВО)
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
            RefreshElemTable();
        }
    }
}
