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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Unziper.Views
{
    /// <summary>
    /// Логика взаимодействия для MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
        }

        private void Button_DragOver(object sender, DragEventArgs e)
        {
            var button = (Button)sender;
            button.BorderBrush = new SolidColorBrush(new Color() { R = 32, G = 160, B = 255, A = 255});
        }

        private void Button_DragLeave(object sender, DragEventArgs e)
        {
            var button = (Button)sender;
            button.BorderBrush = new SolidColorBrush(new Color() { A = 0 });
        }
    }
}
