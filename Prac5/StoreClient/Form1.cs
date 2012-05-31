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
    public partial class Form1 : Form
    {
        ServiceStoreClient StoreProxy = new ServiceStoreClient();
        public Form1()
        {
            InitializeComponent();
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (StoreProxy.login(textBox1.Text, textBox2.Text))
            {
                System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc3));
                t.Start();
                //Form3.setStoreProxy(StoreProxy);
                this.Close();
            }
            else
            {
                label3.Text = "wrong password or username";
                
            }
        }
        public static void ThreadProc2()
        {

            Application.Run(new Form2());

        }
        public static void ThreadProc3()
        {

            Application.Run(new Form3());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc2));
            t.Start();
            this.Close();
        }
    }
}
