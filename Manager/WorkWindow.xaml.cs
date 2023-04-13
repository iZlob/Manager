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
using System.Collections.ObjectModel;
using System.IO;

namespace Manager
{
    /// <summary>
    /// Логика взаимодействия для WorkWindow.xaml
    /// </summary>
    public class RowContacts
    {
        public CheckBox check { get; set; }
        public TextBlock num { get; set; }
        public TextBox name { get; set; }
        public TextBox fone_number { get; set; }
        public RowContacts(CheckBox ch, TextBlock tblock, TextBox tbox, TextBox fone)
        {
            check = ch;
            num = tblock;
            name = tbox;
            fone_number = fone;
        }
    }
   
    public partial class WorkWindow : Window
    {
        public ObservableCollection<RowContacts> contacts { get; set; }
        public WorkWindow()
        {
            InitializeComponent();
            
            contacts = new ObservableCollection<RowContacts>();

            joblist.Items.Add("Написать письмо ...");
            joblist.Items.Add("Назначить встречу/рабочее совещание ...(место/время)");
            joblist.Items.Add("Встреча в ...(место/время)");
            joblist.Items.Add("Составить отчет ...");
            joblist.Items.Add("Составить заявку ...");
            joblist.Items.Add("Согласовать решения ...");

            ChangeLocalization();
        }

        private void ChangeLocalization()
        {
            WorkPlaceWindow.Title = Resource1.WorkPlaceWindow;
            btn_contactList.Content = Resource1.btn_contactList;
            btn_clear.Content = Resource1.btn_clear;
        }

        private void joblist_GotFocus(object sender, RoutedEventArgs e)
        {
            joblist.IsDropDownOpen = true;
        }

        private void joblist_LostFocus(object sender, RoutedEventArgs e)
        {
            joblist.IsDropDownOpen = false;
        }
        public void CreateRowInGridList(string str)
        {
            RowDefinition row = new RowDefinition();
            row.Height = GridLength.Auto;
            gridList.RowDefinitions.Add(row);
            ////////////////////
            TextBox txt = new TextBox();
            txt.Text = str;
            txt.TextWrapping = TextWrapping.Wrap;
            txt.AcceptsReturn = true;
            txt.BorderThickness = new Thickness(0);
            txt.Margin = new Thickness(2);
            txt.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(txt, 2);
            Grid.SetRow(txt, gridList.RowDefinitions.Count - 1);
            gridList.Children.Add(txt);

            ///////////////////////////////
            CheckBox box = new CheckBox();
            box.Checked += Box_Checked;
            box.Unchecked += Box_Unchecked;
            box.VerticalAlignment = VerticalAlignment.Center;
            box.Margin = new Thickness(2);
            Grid.SetColumn(box, 1);
            Grid.SetRow(box, gridList.RowDefinitions.Count - 1);
            gridList.Children.Add(box);

            ///////////////////////////////////
            Button btn = new Button();
            btn.Content = "✓";
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.Width = 20;
            btn.Margin = new Thickness(2);
            btn.Visibility = Visibility.Hidden;
            Grid.SetColumn(btn, 0);
            Grid.SetRow(btn, gridList.RowDefinitions.Count - 1);
            gridList.Children.Add(btn);
        }
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (joblist.SelectedValue != null)
            {
                CreateRowInGridList(joblist.SelectedValue.ToString());
            }
        }

        private void Box_Checked(object sender, RoutedEventArgs e)
        {
            int ind = 0;
            CheckBox cb = sender as CheckBox;
            foreach (object b in gridList.Children)
            {
                if (b.Equals(cb))
                {
                    Button btn = gridList.Children[ind + 1] as Button;
                    btn.Visibility = Visibility.Visible;
                    btn.Click += btn_Done_Click;
                }
                ind++;
            }
        }
        private void Box_Unchecked(object sender, RoutedEventArgs e)
        {
            int ind = 0;
            CheckBox cb = sender as CheckBox;
            foreach (object b in gridList.Children)
            {
                if (b.Equals(cb))
                {
                    Button btn = gridList.Children[ind + 1] as Button;
                    btn.Visibility = Visibility.Hidden;
                    btn.Click -= btn_Done_Click;
                }
                ind++;
            }
        }
        private void btn_Done_Click(object sender, RoutedEventArgs e)
        {
            int ind = 0;
            Button btn = sender as Button;
            foreach (object b in gridList.Children)
            {
                if (b.Equals(btn))
                {
                    TextBox tb = gridList.Children[ind - 2] as TextBox;
                    TextBlock txt = new TextBlock();
                    txt.Text = tb.Text;
                    txt.TextDecorations = TextDecorations.Strikethrough;
                    txt.TextWrapping = TextWrapping.Wrap;
                    txt.Width = doneList.ActualWidth - 30;
                    doneList.Items.Add(txt);

                    gridList.Children.RemoveAt(ind - 2);
                    gridList.Children.RemoveAt(ind - 2);
                    gridList.Children.RemoveAt(ind - 2);
                    break;
                }
                ind++;
            }
        }

        private void btn_contactList_Click(object sender, RoutedEventArgs e)
        {
            ContactListWindow contact = new ContactListWindow();
            contact.MaxWidth = 485;
            contact.contacts = this.contacts;
            contact.Owner = this;
            contact.ShowDialog();
        }

        private void WorkPlaceWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MyMessageBox box = new MyMessageBox();
            box.ShowDialog();

            switch (box.res)
            {
                case 1:
                    {
                        MainWindow mw = (MainWindow)this.Owner;

                        string current_path = Directory.GetCurrentDirectory() + "\\" + $"{mw.login}.txt";
                        StreamWriter file = File.CreateText(current_path);
                        file.WriteLine(mw.pass);
                        file.WriteLine(mw.name);
                        file.WriteLine(mw.mail);

                        for (int i = 0; i < gridList.Children.Count; i += 3)
                        {
                            TextBox tb = (TextBox)gridList.Children[i];
                            file.Write(tb.Text + "¿");
                        }
                        file.WriteLine();
                        foreach (TextBlock done in doneList.Items)
                            file.Write(done.Text + "¿");
                        file.WriteLine();
                        foreach (var contact in contacts)
                            file.Write(contact.name.Text + "¿");
                        file.WriteLine();
                        foreach (var contact in contacts)
                            file.Write(contact.fone_number.Text + "¿");
                        file.Close();
                        this.Owner.Show();
                        break;
                    }
                case 2:
                    {
                        if (box.res == 2)
                            this.Owner.Show();
                        break;
                    }
                case 0:
                    {
                        e.Cancel = true;
                        break;
                    }
                default:
                    {
                        e.Cancel = true;
                        break;
                    }
            }
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            doneList.Items.Clear();
        }
    }
}
