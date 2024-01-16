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
    /// ToManager.xaml 的交互逻辑
    /// </summary>
    public partial class ToManager : Window
    {
        public ToManager(DataTable table1)
        {
            InitializeComponent();
            textBlock3.DataContext = table1;
            textBlock4.DataContext = table1;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ManageSalerData md = new ManageSalerData();
            md.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Gather md = new Gather();
            md.Show();
        }
    }
}
