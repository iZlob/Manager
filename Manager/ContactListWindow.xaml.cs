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
using System.Data;
using System.Collections.ObjectModel;

namespace Manager
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class ContactListWindow : Window
    {
        public ObservableCollection<RowContacts> contacts;
        public ContactListWindow()
        {
            InitializeComponent();
            ChangeLocalization();
        }

        private void ChangeLocalization()
        {
            ContactListWin.Title = Resource1.ContactListWin;
            col3.Header = Resource1.col3;
            col4.Header = Resource1.col4;
        }
        private void ContactListWin_Loaded(object sender, RoutedEventArgs e)
        {
            Table.ItemsSource = contacts;
        }

        private void Add_btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border b = sender as Border;

            if (b.Equals(Add_btn))
            {
                string count = (contacts.Count + 1).ToString();
                contacts.Add(new RowContacts
                    (
                    new CheckBox(),
                    new TextBlock() { Text = count },
                    new TextBox() { Text = "", TextWrapping = TextWrapping.Wrap, Width = 100 },
                    new TextBox() { Text = "", TextWrapping = TextWrapping.Wrap, Width = 100 }
                    )
                    );
                //Table.Items.Add(new RowContacts(new CheckBox(), count, "", ""));
                Table.ItemsSource = contacts;
            }
        }

        private void Add_btn_MouseEnter(object sender, MouseEventArgs e)
        {
            Add_btn.Opacity = 0.5;
        }

        private void Add_btn_MouseLeave(object sender, MouseEventArgs e)
        {
            Add_btn.Opacity = 1;
        }

        private void Del_btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;
            if (border.Equals(Del_btn) &&
                contacts[contacts.Count - 1].name.Text == "" &&
                contacts[contacts.Count - 1].fone_number.Text == "")
                contacts.RemoveAt(contacts.Count - 1);
        }

        private void Del_btn_MouseEnter(object sender, MouseEventArgs e)
        {
            Del_btn.Opacity = 0.5;
        }

        private void Del_btn_MouseLeave(object sender, MouseEventArgs e)
        {
            Del_btn.Opacity = 1;
        }

        private void Done_btn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border border = sender as Border;

            if (border.Equals(Done_btn))
            {
                for (int i = 0; i < contacts.Count; i++)
                {
                    if (contacts[i].check.IsChecked == true)
                    {
                        WorkWindow ww = (WorkWindow)this.Owner;
                        TextBlock txt = new TextBlock();
                        txt.TextDecorations = TextDecorations.Strikethrough;
                        txt.Text = "Called: " + contacts[i].name.Text;
                        ww.doneList.Items.Add(txt);
                        
                        contacts[i].check.IsChecked = false;
                        contacts[i].fone_number.Text = "";
                        contacts[i].name.Text = "";
                        contacts.RemoveAt(i);
                        i--;
                    }
                }
                for (int i = 0; i < contacts.Count; i++)
                {
                    contacts[i].num.Text = (i + 1).ToString();
                }

            }
        }

        private void Done_btn_MouseEnter(object sender, MouseEventArgs e)
        {
            Done_btn.Opacity = 0.5;
        }

        private void Done_btn_MouseLeave(object sender, MouseEventArgs e)
        {
            Done_btn.Opacity = 1;
        }
    }
}
