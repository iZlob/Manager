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
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Manager
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>

    public partial class RegWindow : Window
    {
        public RegWindow()
        {
            InitializeComponent();
            ChangeLocalization();
        }
        private void ChangeLocalization()
        {
            RegTitle.Title = Resource1.RegTitle;
            enterLogin.Content = Resource1.enterLogin;
            enterPassword.Content = Resource1.enterPassword;
            repeatPassword.Content = Resource1.repeatPassword;
            nameUser.Content = Resource1.nameUser;
            eMail.Content = Resource1.eMail;
            btn_Confirm.Content = Resource1.btn_Confirm;
            btn_Cancel.Content = Resource1.btn_Cancel;
        }

        private void Click_btnConfirm(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory());
            string current_path = Directory.GetCurrentDirectory() + "\\" + $"{new_log.Text}.txt";
            bool isOk = true;
            foreach (string file in files)
                if (file == current_path)
                    isOk = false;
            if (isOk)
            {
                if (new_log.Text == "")
                {
                    if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "ru-RU")
                        MessageBox.Show("Введите логин!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                        if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "eng")
                        MessageBox.Show("Enter your username!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                        if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "es")
                        MessageBox.Show("Introduzca su nombre de usuario!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                if (new_pass.Text == "")
                {
                    if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "ru-RU")
                        MessageBox.Show("Введите пароль!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                                            if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "eng")
                        MessageBox.Show("Enter the password!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                                            if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "es")
                        MessageBox.Show("Introduzca su contraseña", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                if (new_pass.Text == new_passRepeat.Text)
                {
                    StreamWriter file = File.CreateText(current_path);
                    file.WriteLine(new_pass.Text);
                    file.WriteLine(new_Name.Text);
                    file.WriteLine(new_Mail.Text);
                    file.Close();

                    WorkWindow work = new WorkWindow();
                    work.Title = work.Title + ": " + new_Name.Text + ", E-mail: " + new_Mail.Text;
                    work.Owner = this.Owner;
                    work.Owner.Hide();
                    this.Close();
                    work.Show();
                }
                else
                {
                    if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "ru-RU")
                        MessageBox.Show("Пароль не подтвержден!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                        if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "eng")
                        MessageBox.Show("The password is not confirmed!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                        if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "es")
                        MessageBox.Show("Contraseña no confirmada!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "ru-RU")
                    MessageBox.Show("Пользователь с таким логином уже существует.\nВыберите другой!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                        if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "eng")
                    MessageBox.Show("A user with this username already exists.\nChoose another one!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                        if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "es")
                    MessageBox.Show("El usuario con este nombre de usuario ya existe.\nElige otro!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void Click_btnCancel(object sender, EventArgs e)
        {            
            this.Close();
        }
        private void RepeatPassword(object sender, TextChangedEventArgs args)
        {
            int ind = 0;
            if (new_pass.Text.Length == new_passRepeat.Text.Length)
            {
                foreach (var ch in new_passRepeat.Text)
                {
                    if (new_pass.Text[ind] == ch)
                    {
                        new_passBorder.BorderBrush = new SolidColorBrush(Colors.Green);
                        new_passRepeatBorder.BorderBrush = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        new_passBorder.BorderBrush = new SolidColorBrush(Colors.Red);
                        new_passRepeatBorder.BorderBrush = new SolidColorBrush(Colors.Red);
                        break;
                    }
                    ind++;
                }
            }
            else
            {
                new_passBorder.BorderBrush = new SolidColorBrush(Colors.Red);
                new_passRepeatBorder.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            if (new_passRepeat.Text == "")
            {
                new_passBorder.BorderBrush = new SolidColorBrush(Colors.Black);
                new_passRepeatBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            }
        }
       
    }

}
