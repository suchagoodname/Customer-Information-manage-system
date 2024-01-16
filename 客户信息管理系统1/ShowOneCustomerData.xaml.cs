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
    /// ShowOneCustomerData.xaml 的交互逻辑
    /// </summary>
    public partial class ShowOneCustomerData : Window
    {
        SqlConnection conn;
        SqlDataAdapter da;
        DataSet ds;
        DataView view;
        public ShowOneCustomerData(DataTable table1, DataTable table2)
        {
            InitializeComponent();
            textBlock3.DataContext = table1;
            textBlock4.DataContext = table1;
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
            view.AllowEdit = true;
            view.AllowDelete = true;
            view.AllowNew = true;
            listview1.ItemsSource=table2.DefaultView;
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
    }
}
