using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFConsole.StoreService;

namespace WPFConsole
{
    /// <summary>
    /// Interaction logic for SignupPage.xaml
    /// </summary>
    public partial class SignupPage : Page
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ServiceStoreClient ssc = new ServiceStoreClient();

            label3.Content = ssc.signup(textBox1.Text);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            ClientController.GetWindow(this).Navigate(new LoginPage());
        }
    }
}
