using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MedIdeaApp.DB.Models;
using MedIdeaApp.Model;

namespace MedIdeaApp
{
    /// <summary>
    ///     Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private readonly User _user;
        private readonly List<Treatment> list = new List<Treatment>();
        private List<Treatment> _treatment;

        /// <summary>
        ///     Конструктор
        /// </summary>
        public EditWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     Конструктор с параметрами.
        /// </summary>
        /// <param name="user"></param>
        public EditWindow(User user)
        {
            _user = user;

            InitializeComponent();

            UpdateTreatmentsGrid();

            InitField();
        }

        /// <summary>
        ///     Сохранить данные
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_user != null)
                    Save(_user);
                else
                    Save();

                MessageBox.Show("Данные успешно сохранены");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Данные не сохранены. Проверьте корректность введенных данных. Если все верно звоните фиксикам");
            }
        }


        /// <summary>
        ///     Отчистить поля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearFields_Click(object sender, RoutedEventArgs e)
        {
            txtAddress.Text = "";
            txtName.Text = "";
            txtBirthday.Text = "";
            cmbSex.Text = "";
            txtPhone.Text = "";
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
                if (treatmentsGrid.SelectedItems.Count > 0)
                    for (var i = 0; i < treatmentsGrid.SelectedItems.Count; i++)
                    {
                        var treatment = treatmentsGrid.SelectedItems[i] as Treatment;
                        if (treatment != null) db.Treatments.Remove(treatment);
                    }

                db.SaveChanges();
            }

            UpdateTreatmentsGrid();
        }

        /// <summary>
        ///     Обновить грид
        /// </summary>
        private void UpdateTreatmentsGrid()
        {
            using var db = new MedIdeaContext();
            _treatment = db.Treatments.Where(x => x.User == _user).ToList();
            treatmentsGrid.ItemsSource = db.Treatments.Local.ToBindingList();
        }


        /// <summary>
        ///     Задать значения полям
        /// </summary>
        private void InitField()
        {
            if (_user != null)
            {
                txtAddress.Text = _user.Address;
                txtBirthday.Text = _user.Birthday.ToString();
                txtName.Text = _user.Name;
                txtPhone.Text = _user.Phone;
                cmbSex.Text = _user.Sex;
            }
            else MessageBox.Show("Выберите пользователя");

        }

        /// <summary>
        ///     Создать строку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            if (_treatment != null)
                _treatment.Add(new Treatment());
            else
                _treatment = new List<Treatment>();

            treatmentsGrid.ItemsSource = _treatment;
            treatmentsGrid.Items.Refresh();
        }


        /// <summary>
        ///     Сохранить
        /// </summary>
        /// <param name="user">
        ///     <see cref="User" />
        /// </param>
        private void Save(User user = null)
        {
            User newUser;

            if (user != null)
                newUser = user;
            else
                newUser = new User();

            SaveChanges(newUser, user);
        }

        /// <summary>
        ///     Добавить в лист
        /// </summary>
        /// <param name="newUser">
        ///     <see cref="User" />
        /// </param>
        private void AddDataInList(User newUser)
        {
            foreach (var item in _treatment)
            {
                var it = item;
                it.User = newUser;
                it.UserId = newUser.Id;

                list.Add(it);
            }
        }

        /// <summary>
        ///     Сохранить изменения.
        /// </summary>
        /// <param name="newUser">
        ///     <see cref="User" />
        /// </param>
        /// <param name="user">
        ///     <see cref="User" />
        /// </param>
        private void SaveChanges(User newUser, User user)
        {
            using (var db = new MedIdeaContext())
            {
                AddNewDataFromFields(newUser);

                if (user != null)
                {
                    db.Update(newUser);
                    AddDataInList(newUser);
                    db.UpdateRange(list);
                }
                else
                {
                    db.Add(newUser);
                    AddDataInList(newUser);
                    db.UpdateRange(list);
                }

                db.SaveChanges();
            }
        }

        /// <summary>
        ///     Добавить новые поля в объект из полей ввода
        /// </summary>
        /// <param name="newUser">
        ///     <see cref="User" />
        /// </param>
        private void AddNewDataFromFields(User newUser)
        {
            newUser.Address = txtAddress.Text;
            newUser.Birthday = txtBirthday.DisplayDate;
            newUser.Phone = txtPhone.Text;
            newUser.Sex = cmbSex.Text;
            newUser.Name = txtName.Text;
        }
    }
}