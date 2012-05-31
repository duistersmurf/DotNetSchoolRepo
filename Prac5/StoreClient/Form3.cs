using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StoreClient.StoreService;

namespace StoreClient
{
    public partial class Form3 : Form
    {
        ServiceStoreClient StoreProxy;

        public Form3()
        {
            InitializeComponent();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           // listBox2.Items.AddRange(myList);
        }

        public void setStoreProxy()
        {
            
        }

        internal void setStoreProxy(ServiceStoreClient st)
        {
            StoreProxy = st;
        }
    }
}
