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

namespace Manager
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class MyMessageBox : Window
    {
        public int res = 0;
        public MyMessageBox()
        {
            InitializeComponent();
            ChangeLocalization();
        }

        private void ChangeLocalization()
        {
            message.Text = Resource1.message;
            yes.Content = Resource1.yes;
            no.Content = Resource1.no;
            cancel_.Content = Resource1.cancel_;
        }

        private void Button_Click_Yes(object sender, RoutedEventArgs e)
        {
            res = 1;
            this.Close();
        }

        private void Button_Click_No(object sender, RoutedEventArgs e)
        {
            res = 2;
            this.Close();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            res = 0;
            this.Close();
        }
              
    }
}
