using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Manager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string name = null;
        public string mail = null;
        public string login = null;
        public string pass = null;
        public MainWindow()
        {
            InitializeComponent();
            ChangeLocalization();

        }
        private void ChangeLocalization()
        {
            Enter.Content = Resource1.Enter;
            Cancel.Content = Resource1.Cancel;
            titleAutorization.Content = Resource1.titleAutorization;
            Login.Content = Resource1.Login;
            Password.Content = Resource1.Password;
            RegHyperLink.Text = Resource1.RegHyperLink;
            infoReg.Text = Resource1.infoReg;
            MainTitle.Title = Resource1.MainTitle;
        }

        private void Click_btnRus(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("Ru");
            ChangeLocalization();
        }
        private void Click_btnEng(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("Eng");
            ChangeLocalization();
        }
        private void Click_btnEsp(object sender, EventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("Es");
            ChangeLocalization();
        }
        private void ClickEnter(object sender, EventArgs e)
        {
            if (File.Exists($"{login_name.Text}.txt"))
            {
                string current_path = Directory.GetCurrentDirectory() + "\\" + $"{login_name.Text}.txt";
                StreamReader file = File.OpenText(current_path);
                pass = file.ReadLine();

                if (password.Password == pass)
                {
                    if (login_name.Text == "Boss")
                    {
                        file.Close();
                        BossArea boss = new BossArea();
                        login_name.Text = "";
                        password.Password = "";
                        boss.ShowDialog();
                        return;
                    }


                    WorkWindow work = new WorkWindow();
                    
                    name = file.ReadLine();
                    mail = file.ReadLine();
                    login = login_name.Text;
                    string grid_place = file.ReadLine();
                    string done_place = file.ReadLine();
                    string contact_name = file.ReadLine();
                    string contact_fone = file.ReadLine();
                    file.Close();

                    string[] grid_words;
                    string[] done_words;
                    string[] contact_name_words;
                    string[] contact_fone_words;
                    if (grid_place != null)
                    {
                        char[] separ = { '¿' };
                        grid_words = grid_place.Split(separ, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string word in grid_words)
                        {
                            work.CreateRowInGridList(word);
                        }
                    }
                    if (done_place != null)
                    {
                        char[] separ = { '¿' };
                        done_words = done_place.Split(separ, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string word in done_words)
                        {
                            TextBlock txt = new TextBlock();
                            txt.Text = word;
                            txt.TextDecorations = TextDecorations.Strikethrough;
                            txt.TextWrapping = TextWrapping.Wrap;
                            work.doneList.Items.Add(txt);
                        }
                    }
                    if (contact_name != null)
                    {
                        char[] separ = { '¿' };
                        contact_name_words = contact_name.Split(separ, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string word in contact_name_words)
                        {
                            work.contacts.Add(new RowContacts(new CheckBox(), new TextBlock(), new TextBox() { Text = word }, new TextBox()));
                        }
                    }
                    if (contact_fone != null)
                    {
                        char[] separ = { '¿' };
                        contact_fone_words = contact_fone.Split(separ, StringSplitOptions.RemoveEmptyEntries);
                        int i = 0;
                        foreach (string word in contact_fone_words)
                        {
                            work.contacts[i].fone_number.Text = word;
                            work.contacts[i].num.Text = (i + 1).ToString();
                            i++;                          
                        }
                    }
                    login_name.Text = "";
                    password.Password = "";
                    work.Title = work.Title + ": " + name + ", E-mail: " + mail;
                    work.Owner = this;
                    work.Show();                    
                    this.Hide();
                    
                }
                else
                {
                    if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "ru-RU")
                        MessageBox.Show("Пароль не верный. Попробуйте снова.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "eng")
                        MessageBox.Show("The password is not correct. Try again.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "es")
                        MessageBox.Show("La contraseña no es correcta. Inténtalo de nuevo.", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    password.Password = "";
                }
            }
            else
            {
                if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "ru-RU")
                    MessageBox.Show("Пользователя с таким логином не существует. Пожалуйста зарегистрируйтесь!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "eng")
                    MessageBox.Show("There is no user with this username. Please register!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "es")
                    MessageBox.Show("El usuario con este nombre de usuario no existe. Por favor regístrese!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                login_name.Text = "";
                password.Password = "";
            }
        }
        private void ClickCancel(object sender, EventArgs e)
        {
            this.Close();
        }
        void HyperlinkClick(object sender, EventArgs e)
        {
            RegWindow reg = new RegWindow();
            reg.Owner = this;
            reg.ShowDialog();
            login = reg.new_log.Text;
            pass = reg.new_pass.Text;
            name = reg.new_Name.Text;
            mail = reg.new_Mail.Text;
        }
    }

}

