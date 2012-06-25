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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btlogin_Click(object sender, RoutedEventArgs e)
        {
            lberror.Content = "";
            ServiceStoreClient ssc = new ServiceStoreClient();
            if (ssc.login(tbname.Text, tbpw.Password) != null)
            {
                ClientController.loginperson = ssc.login(tbname.Text, tbpw.Password);
                NavigationWindow nw = new NavigationWindow();
                nw.ShowsNavigationUI = false;
                nw.Navigate(new StorePage());
                nw.Show();

                ClientController.GetWindow(this).Close();
            }
            else
            {
                lberror.Content = "username or password are not valid";
            }
        }

        private void bsignup_Click(object sender, RoutedEventArgs e)
        {
            ClientController.GetWindow(this).Navigate(new SignupPage());
        }
    }
}
