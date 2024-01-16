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

namespace 客户信息管理系统1
{
    /// <summary>
    /// ToSaler.xaml 的交互逻辑
    /// </summary>
    public partial class ToSaler : Window
    {
        public ToSaler(DataTable table1)
        {
            InitializeComponent();
            textBlock3.DataContext = table1;
            textBlock4.DataContext = table1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InputCustomer ic = new InputCustomer();
            ic.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddCommodity ic = new AddCommodity();
            ic.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ReplyPropose ic = new ReplyPropose();
            ic.Show();
        }
    }
}
