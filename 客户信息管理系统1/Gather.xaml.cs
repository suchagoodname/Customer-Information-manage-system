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
using Microsoft.Win32;
using System.IO;

namespace 客户信息管理系统1
{
    /// <summary>
    /// Gather.xaml 的交互逻辑
    /// </summary>
    public partial class Gather : Window
    {
        CustomerMisDataSet ds;
        CustomerMisDataSetTableAdapters.CustomerTableAdapter customerAdapter;
        CustomerMisDataSetTableAdapters.CommodityTableAdapter commodityAdapter;
        DataView view;
        public Gather()
        {
            InitializeComponent();
            ds = new CustomerMisDataSet();
            customerAdapter = new CustomerMisDataSetTableAdapters.CustomerTableAdapter();
            commodityAdapter = new CustomerMisDataSetTableAdapters.CommodityTableAdapter();
            customerAdapter.Fill(ds.Customer);
            commodityAdapter.Fill(ds.Commodity);
            view = new DataView();
            view.Table = ds.Tables["Commodity"];
            listView1.ItemsSource = ds.Tables["Customer"].DefaultView;
            listView1.SelectedIndex = 0;          
        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView drv = (DataRowView)listView1.SelectedItem;
            view.RowFilter = "CustomerNum = " +
            drv["CustomerNum"].ToString();
            listView2.ItemsSource = view;
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboBox2.Items.Clear();
            ListBoxItem lbi = comboBox1.SelectedItem as ListBoxItem;
            string s = lbi.Content.ToString();
            if (s == "客户号")
            {
                comboBox2.Items.Add(">");
                comboBox2.Items.Add(">=");
                comboBox2.Items.Add("<");
                comboBox2.Items.Add("<=");
                comboBox2.Items.Add("=");
                comboBox2.Items.Add("<>");
            }
            else if (s == "姓名")
            {
                comboBox2.Items.Add("=");
                comboBox2.Items.Add("like");
            }
            else comboBox2.Items.Add("=");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string filterString;
            if (comboBox1.Text == "" || comboBox2.Text == "" || textBox1.Text == "")
            {
                MessageBox.Show("请将过滤条件填写完整！");
                return;
            }
            if (comboBox1.Text == "客户号")
            { filterString = "CustomerNum " + comboBox2.Text + " " + textBox1.Text; }
            else if (comboBox1.Text == "姓名")
            {
                if (comboBox2.Text == "=")
                    filterString = "CustomerName " + comboBox2.Text + " '" +textBox1.Text + "'";
                else
                    filterString = "CustomerName " + comboBox2.Text + " '" + textBox1.Text + "%'";
            }
            else filterString = "studentsex " + comboBox2.Text + " '" + textBox1.Text + "'";
            string sc = @"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=D:\CustomerMis.mdf;
             Integrated Security=True;
             Connect Timeout=30";
            SqlConnection conn = new SqlConnection(sc);
            string s = "SELECT * FROM Customer WHERE " + filterString;
            SqlDataAdapter da = new SqlDataAdapter(s, conn);
            DataSet ds1 = new DataSet();
            da.Fill(ds1, "Customer");
            CustomerMisDataSetTableAdapters.CustomerNumTableTableAdapter snta = new
            CustomerMisDataSetTableAdapters.CustomerNumTableTableAdapter();
            snta.DeleteAll();
            snta.Fill(ds.CustomerNumTable);
            DataTable dt = ds1.Tables["Customer"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = ds.Tables["CustomerNumTable"].NewRow();
                row["CustomerNum"] = dt.Rows[i]["CustomerNum"];
                ds.Tables["CustomerNumTable"].Rows.Add(row);
            }
            snta.Update(ds.CustomerNumTable);
            customerAdapter.FillInCustomerNumTable(ds.Customer);
            commodityAdapter.FillInCustomerNumTable(ds.Commodity);
            listView1.DataContext = ds.Tables["Customer"];
            listView2.DataContext = ds.Tables["Customer"];
            listView1.SelectedIndex = 0;

        }
    }
}
