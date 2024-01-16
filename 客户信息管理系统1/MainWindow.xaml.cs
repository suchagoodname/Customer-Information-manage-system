using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Data;

namespace 客户信息管理系统1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conn;
        SqlCommand da;
        string SqlConnTxt = "";//数据库连接字符串
        public string RadioButtonState
        {
            get
            {
                string s = null;
                if (radioButton1.IsChecked == true)
                    s = radioButton1.Content.ToString();
                else if (radioButton2.IsChecked == true)
                    s = radioButton2.Content.ToString();
                else if (radioButton3.IsChecked == true)
                    s = radioButton3.Content.ToString();
                return s;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string s = RadioButtonState;
            DataTable table, CoreTable;
            switch (s)
            {
                case "客户":
                    CustomerMisDataSetTableAdapters.CustomerTableAdapter CuTa =
                        new CustomerMisDataSetTableAdapters.CustomerTableAdapter();
                    int n = int.Parse(textBox1.Text);
                    table = CuTa.GetDataByCustomerNum(n);
                    if (table.Rows.Count != 0)
                    {
                        if (table.Rows[0]["Customerpassword"].ToString() == passwordBox1.Password)
                        {
                            CustomerMisDataSetTableAdapters.CommodityTableAdapter comTa =
                                 new CustomerMisDataSetTableAdapters.CommodityTableAdapter();
                            CoreTable = comTa.GetDataByCustomerNum(n);
                            ShowOneCustomerData sd = new ShowOneCustomerData(table, CoreTable);
                            sd.Show();
                        }
                        else
                            MessageBox.Show("密码错误");

                    }
                    else
                        MessageBox.Show("查无此人");
                    break;
                case "销售员":
                    CustomerMisDataSetTableAdapters.SalerTableAdapter saTa =
                new CustomerMisDataSetTableAdapters.SalerTableAdapter();
                    int i = int.Parse(textBox1.Text);
                    table = saTa.GetDataBySalerNum(i);
                    if (table.Rows.Count != 0)
                    {
                        if (table.Rows[0]["Salerpassword"].ToString() == passwordBox1.Password)
                        {
                            ToSaler id = new ToSaler(table);
                            id.Show();
                        }
                        else
                            MessageBox.Show("密码错误");
                    }
                    else
                        MessageBox.Show("查无此人");
                    break;
                case "管理员":
                    CustomerMisDataSetTableAdapters.ManagerTableAdapter maTa =
                       new CustomerMisDataSetTableAdapters.ManagerTableAdapter();
                    int m = int.Parse(textBox1.Text);
                    table = maTa.GetDataByManagerNum(m);
                    if (table.Rows.Count != 0)
                    {
                        if (table.Rows[0]["Managerpassword"].ToString() == passwordBox1.Password)
                        {
                            ToManager md = new ToManager(table);
                            md.Show();
                        }
                        else
                            MessageBox.Show("密码错误");
                    }
                    else
                        MessageBox.Show("查无此人");
                    break;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || passwordBox1.Password == "")
            {
                MessageBox.Show("所有项都必须填写！");
                return;
            }
            conn = new SqlConnection(SqlConnTxt);

            string s = RadioButtonState;
            switch (s)
            {
                case "销售员":
                    string txt2 = "Insert Into Saler(SalerNum,SalerName,SalerPassword) Values(' ";
                    txt2 += textBox1.Text + "',N'";
                    txt2 += textBox2.Text + "','";
                    txt2 += passwordBox1.Password + "')";
                    conn.Open();
                    da = new SqlCommand(txt2, conn);
                    da.ExecuteNonQuery();
                    MessageBox.Show("注册成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    passwordBox1.Password = "";
                    conn.Close();
                    break;
                case "客户":
                    string txt1 = "Insert Into Customer(CustomerNum,CustomerName,CustomerPassword) Values(' ";
                    txt1 += textBox1.Text + "',N'";
                    txt1 += textBox2.Text + "','";
                    txt1 += passwordBox1.Password + "')";
                    conn.Open();
                    da = new SqlCommand(txt1, conn);
                    da.ExecuteNonQuery();
                    MessageBox.Show("注册成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    passwordBox1.Password = "";
                    conn.Close();
                    break;
                case "管理员":
                    string txt3 = "Insert Into Manager(ManagerNum,ManagerName,ManagerPassword) Values(' ";
                    txt3 += textBox1.Text + "',N'";
                    txt3 += textBox2.Text + "','";
                    txt3 += passwordBox1.Password + "')";
                    conn.Open();
                    da = new SqlCommand(txt3, conn);
                    da.ExecuteNonQuery();
                    MessageBox.Show("注册成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    passwordBox1.Password = "";
                    conn.Close();
                    break;
            }

        }
        private bool DBConn()
        {
            StreamReader sr = null;
            bool blnConnSuccess = true;
            try
            {
                sr = File.OpenText(@".\sqlconn.txt");//从设置文件中读出数据库连接串

                while (sr.Peek() != -1)
                {
                    SqlConnTxt += sr.ReadLine();

                }
                if (SqlConnTxt == "")
                    throw new Exception("数据库（文件）连接字串定义异常！");
            }
            catch (DirectoryNotFoundException e1)
            {
                MessageBox.Show(e1.Message + "配置文件出错", "出错！", MessageBoxButton.OK, MessageBoxImage.Error);
                blnConnSuccess = false;
            }
            catch (FileNotFoundException e1)
            {
                MessageBox.Show(e1.Message + "配置文件出错！", "出错！", MessageBoxButton.OK, MessageBoxImage.Error);
                blnConnSuccess = false;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message + "退出程序！", "出错！", MessageBoxButton.OK, MessageBoxImage.Error);
                blnConnSuccess = false;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
            if (blnConnSuccess)
            {
                conn = new SqlConnection(SqlConnTxt);
                try
                {
                    conn.Open();
                    conn.Close();
                }
                catch (SqlException e)
                {
                    MessageBox.Show(e.Message, "连接不成功，请检查环境配置再试。", MessageBoxButton.OK, MessageBoxImage.Error);
                    blnConnSuccess = false;
                }

            }

            return blnConnSuccess;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!DBConn())
            {
                MessageBox.Show("连接数据库失败,程序中止，请与开发者联系！", "出错", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
                this.Close();
            }
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
