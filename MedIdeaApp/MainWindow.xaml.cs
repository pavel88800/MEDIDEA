using System.Linq;
using System.Windows;
using MedIdeaApp.DB.Models;
using MedIdeaApp.Model;

namespace MedIdeaApp
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            #region Генерация начальных данных (Если надо - раскоментить)

            /* 
              using (var db = new MedIdeaContext())
              {

                  for (int i = 0; i < 100; i++)
                  {
                      db.Users.Add(
                          new User {Name = "Граф Монтекристо"+i, Phone = "8 888 888 88 88", Address = "Krasnodar"+i, Birthday = DateTime.Today, Sex = "Мужской", Treatments = new List<Treatment>{new Treatment{DateVisit = DateTime.Now, Diagnosis = "test"+i, TypeVisit = "Первичный"}}}
                          );
                  }

                  db.SaveChanges();
              }
              */

            #endregion

            UpdateGrid();
        }

        /// <summary>
        ///     Обновить грид
        /// </summary>
        private void UpdateGrid()
        {
            using var db = new MedIdeaContext();
            db.Users.ToList();
            usersGrid.ItemsSource = db.Users.Local.ToBindingList();
        }

        /// <summary>
        ///     Обновить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateGrid();
        }

        /// <summary>
        ///     Удалить данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MedIdeaContext())
            {
                if (usersGrid.SelectedItems.Count > 0)
                    for (var i = 0; i < usersGrid.SelectedItems.Count; i++)
                    {
                        var user = usersGrid.SelectedItems[i] as User;
                        if (user != null) db.Users.Remove(user);
                    }

                db.SaveChanges();
            }

            UpdateGrid();
        }

        /// <summary>
        ///     Создать пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditWindow();
            editWindow.Show();
        }

        /// <summary>
        ///     Редактировать пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var user = (User) usersGrid.SelectedItem;
            var editWindow = new EditWindow(user);
            editWindow.Show();
        }
    }
}