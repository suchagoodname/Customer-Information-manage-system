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
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Win32;
using System.ComponentModel;


namespace 客户信息管理系统1
{
    /// <summary>
    /// InputCustomer.xaml 的交互逻辑
    /// </summary>
   
    public partial class InputCustomer : Window
    {
        SqlCommand da;
        SqlDataAdapter Da;
        DataSet ds;
        public InputCustomer( )
        {
            InitializeComponent();         
            string sc = @"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=D:\CustomerMis.mdf;
             Integrated Security=True;
             Connect Timeout=30";
            SqlConnection conn = new SqlConnection(sc);
            string s = "SELECT* FROM Customer";
            Da = new SqlDataAdapter(s, conn);
            SqlCommandBuilder scb = new SqlCommandBuilder(Da);
            ds = new DataSet(); //创建DataSet实例
            Da.Fill(ds, "Customer");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("所有项都必须填写！");
                return;
            }
            string sc = @"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=D:\CustomerMis.mdf;
             Integrated Security=True;
             Connect Timeout=30";
            SqlConnection conn = new SqlConnection(sc);
            string txt1 = "Insert Into Customer(CustomerNum,CustomerName,CustomerPassword) Values(' ";
            txt1 += textBox1.Text + "',N'";
            txt1 += textBox2.Text + "','";
            txt1 += textBox3.Text + "')";
            conn.Open();
            da = new SqlCommand(txt1, conn);
            da.ExecuteNonQuery();
            MessageBox.Show("录入成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            conn.Close();
            string txtCommand = "SELECT * FROM Customer";
            Da = new SqlDataAdapter(txtCommand, conn);
            ds = new DataSet();
            Da.Fill(ds, "Customer");        
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }

       
    }
}
