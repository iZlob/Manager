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

namespace Manager
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class BossArea : Window
    {
        public BossArea()
        {
            InitializeComponent();
            ChangeLocalization();

            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());
            FileInfo[] files = dir.GetFiles("*.txt", SearchOption.TopDirectoryOnly);
            foreach (FileInfo file in files)
            {
                if (file.Name != "Boss.txt")
                {
                    RowDefinition row = new RowDefinition();
                    row.Height = GridLength.Auto;
                    staffList.RowDefinitions.Add(row);
                    ///////////////////////////////
                    CheckBox box = new CheckBox();
                    box.VerticalAlignment = VerticalAlignment.Center;
                    box.Margin = new Thickness(2);
                    Grid.SetColumn(box, 0);
                    Grid.SetRow(box, staffList.RowDefinitions.Count - 1);
                    staffList.Children.Add(box);
                    ///////////////////////////////
                    StreamReader f = File.OpenText(file.FullName);
                    string pass = f.ReadLine();
                    string name = f.ReadLine();
                    string mail = f.ReadLine();
                    f.Close();
                    string[] separ = { ".txt" };
                    string[] login = file.Name.Split(separ, StringSplitOptions.RemoveEmptyEntries);
                    string str = name + "  { E-Mail: " + mail + "   Login/Password: " + login[0] + "/" + pass + " }";
                    ///////////////////////////////
                    TextBlock txt = new TextBlock();
                    txt.Text = str;
                    txt.TextWrapping = TextWrapping.Wrap;
                    txt.Margin = new Thickness(2);
                    txt.VerticalAlignment = VerticalAlignment.Center;
                    Grid.SetColumn(txt, 1);
                    Grid.SetRow(txt, staffList.RowDefinitions.Count - 1);
                    staffList.Children.Add(txt);
                }
            }
        }

        private void ChangeLocalization()
        {
            staff.Title = Resource1.staff;
            change_btn.Content = Resource1.change_btn;
            del_btn.Text = Resource1.del_btn;
        }
        private void Delete_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < staffList.Children.Count; i += 2)
            {
                CheckBox check = staffList.Children[i] as CheckBox;
                if (check.IsChecked == true)
                {
                    char[] separ = { ' ', '/' };
                    string[] words = (staffList.Children[i + 1] as TextBlock).Text.Split(separ);
                    string file_name = "";
                    
                    for (int j = 0; j < words.Length; j++)
                        if (words[j] == "Password:")
                            file_name = words[j + 1];

                    FileInfo del_file = new FileInfo(Directory.GetCurrentDirectory() + "\\" + file_name + ".txt");
                    del_file.Delete();

                    staffList.Children.RemoveAt(i);
                    staffList.Children.RemoveAt(i);
                    i -= 2;
                }
            }    
        }

        private void Delete_MouseEnter(object sender, MouseEventArgs e)
        {
            del_btn_border.Opacity = 0.5;
        }

        private void Delete_MouseLeave(object sender, MouseEventArgs e)
        {
            del_btn_border.Opacity = 1;
        }

        private void change_btn_Click(object sender, RoutedEventArgs e)
        {
            pass_area.Visibility = Visibility.Visible;
            OK_btn.Visibility = Visibility.Visible;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter file = File.CreateText(Directory.GetCurrentDirectory() + "\\Boss.txt");
            file.WriteLine(password_text.Text);
            file.Close();
            pass_area.Visibility = Visibility.Hidden;
            OK_btn.Visibility = Visibility.Hidden;
        }

    }
}
