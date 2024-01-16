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

namespace 客户信息管理系统1
{
    /// <summary>
    /// ManageSalerData.xaml 的交互逻辑
    /// </summary>
    public partial class ManageSalerData : Window
    {
        SqlConnection conn;
        SqlDataAdapter da;
        DataSet ds;
        DataView view;
        public ManageSalerData()
        {
            InitializeComponent();
            string sc = @"Data Source=(LocalDB)\MSSQLLocalDB;
            AttachDbFilename=D:\CustomerMis.mdf;
             Integrated Security=True;
             Connect Timeout=30";
            conn = new SqlConnection(sc);
            string s = "select * from Saler";
            da = new SqlDataAdapter(s, conn);
            SqlCommandBuilder scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Saler");
            view = new DataView();
            view.Table = ds.Tables["Saler"];
            view.AllowDelete = true;
            view.AllowEdit = true;
            view.AllowNew = true;
            //view.RowFilter = "TeacherName='孔子'";
            view.Sort = "SalerName ASC"; //按TeacherName 升序排列
            listView1.ItemsSource = ds.Tables["Saler"].DefaultView;
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
                da.Update(ds, "Saler");
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)listView1.SelectedItem;
            drv.Delete();
            da.Update(ds, "Saler");
        }
        private void ClearAddBox()
        {
            textBox1.Text = ""; textBox2.Text = ""; textBox3.Text = "";
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string s1 = textBox1.Text, s2 = textBox2.Text, s3 = textBox3.Text;
            if (s1.Length * s2.Length * s3.Length > 0)
            {
                DataView dv = (DataView)listView1.ItemsSource;
                DataRowView drv = dv.AddNew();

                drv["SalerNum"] = textBox1.Text;
                drv["SalerName"] = textBox2.Text;
                drv["SalerPassword"] = textBox3.Text;
                drv.EndEdit(); //结束编辑状态

                da.Update(ds, "Saler");
                ClearAddBox();
            }
            else MessageBox.Show("请输入全部信息！", "新增记录资料不齐", MessageBoxButton.OK, MessageBoxImage.Error);
        }

       
    }
}
