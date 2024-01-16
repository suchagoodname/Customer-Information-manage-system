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

namespace 客户信息管理系统1
{
    /// <summary>
    /// AddCommodity.xaml 的交互逻辑
    /// </summary>
    public partial class AddCommodity : Window
    {
        SqlConnection conn;
        SqlDataAdapter da;
        DataSet ds;
        DataView view;
        public AddCommodity()
        {
            InitializeComponent();
            string sc = @"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=D:\CustomerMis.mdf;
             Integrated Security=True;
             Connect Timeout=30";
            conn = new SqlConnection(sc);
            string s = "select * from Commodity";
            da = new SqlDataAdapter(s, conn);
            SqlCommandBuilder scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Commodity");
            view = new DataView();
            view.Table = ds.Tables["Commodity"];
            view.AllowDelete = true;
            view.AllowEdit = true;
            view.AllowNew = true;
            //view.RowFilter = "TeacherName='孔子'";
            listView1.ItemsSource = ds.Tables["Commodity"].DefaultView;
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.IsReadOnly = false;
        }

        private void TextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.IsReadOnly = true;
            if (ds.HasChanges())
                da.Update(ds, "Commodity");
        }
        private void ClearAddBox()
        {
            textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = ""; textBox4.Text = "";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)listView1.SelectedItem;
            drv.Delete();
            da.Update(ds, "Commodity");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string s1 = textBox1.Text, s2 = textBox2.Text, s3 = textBox3.Text;
            if (s1.Length * s2.Length * s3.Length > 0)
            {
                DataView dv = (DataView)listView1.ItemsSource;
                DataRowView drv = dv.AddNew();

                drv["CommodityNum"] = textBox1.Text;
                drv["CommodityName"] = textBox2.Text;
                drv["Price"] = textBox3.Text;
                drv["CustomerName"] = textBox4.Text;
                drv["CustomerNum"] = textBox5.Text;
                drv.EndEdit();

                da.Update(ds, "Commodity");
                ClearAddBox();
            }
            else MessageBox.Show("请输入全部信息！", "新增记录资料不齐", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
