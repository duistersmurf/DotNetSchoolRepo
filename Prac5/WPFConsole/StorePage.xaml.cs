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
    /// Interaction logic for StorePage.xaml
    /// </summary>
    public partial class StorePage : Page
    {

        public List<Product> listproductstore;
        public List<Product> listproductperson;
        ServiceStoreClient ssc = new ServiceStoreClient();

        public StorePage()
        {
            InitializeComponent();
            refreshStoreListBox();
            refreshPersonListBox();
            refreshSaldo();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedValue == null)
            {
                MessageBox.Show("select the item you want to buy");
            }
            else
            {
                string item = listBox1.SelectedValue.ToString();
                Product pdt = new Product();

                foreach (Product pt in listproductstore)
                {
                    pdt = pt;

                    if ((pdt.ProductName + " [" + pt.AvalibleProducts + "]" + " ("+pt.ProductPrice + ")").Equals(item))
                    {
                        if (ssc.buyProduct(pdt, int.Parse(textBox1.Text), ClientController.loginperson))
                        {                           
                            refreshStoreListBox();
                            refreshPersonListBox();
                            refreshSaldo();
                            textBox1.Text = "";
                            MessageBox.Show("buy succesfull");
                        }
                        else
                        {
                            if (ssc.getSaldoPerson(ClientController.loginperson.Id) < pdt.ProductPrice)
                            {
                                MessageBox.Show("not enough saldo");
                            }
                            if (pdt.AvalibleProducts < int.Parse(textBox1.Text))
                            {
                                MessageBox.Show("the store does not have that amount of the product or max is:" + pdt.AvalibleProducts);
                                textBox1.Text = "";
                            }
                            
                        }
                    }
                }
            }
        }

        private void refreshStoreListBox()
        {
            listproductstore = new List<Product>();
            listBox1.Items.Clear();
                    var arraylps = ssc.getProductListStore();
                    foreach (Product pt in arraylps)
                    {
                        listproductstore.Add(pt);
                        listBox1.Items.Add(pt.ProductName + " [" + pt.AvalibleProducts + "]" + " ("+pt.ProductPrice + ")");

                    }
        }

        private void refreshPersonListBox()
        {
            listproductperson = new List<Product>();
            listBox2.Items.Clear();
            var arraylpp = ssc.getProductListPerson(ClientController.loginperson.Id);
            foreach (Product pt in arraylpp)
            {
                listproductstore.Add(pt);
                listBox2.Items.Add(pt.ProductName + " [" + pt.AvalibleProducts + "]");

            }
        }

        private void refreshSaldo()
        {
            label3.Content = ssc.getSaldoPerson(ClientController.loginperson.Id);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            label3.Content = ssc.getSaldoPerson(ClientController.loginperson.Id);
            refreshPersonListBox();
            refreshStoreListBox();
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        
    }
}
