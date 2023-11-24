using Snakesssss.ViewModels;
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

namespace Snakesssss
{
    /// <summary>
    /// Interaction logic for AITrainWindow.xaml
    /// </summary>
    public partial class AITrainWindow : Window
    {
        public AITrainWindow()
        {
            InitializeComponent();
            Loaded += AITrainWindow_Loaded;
        }

        private void AITrainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ICloseWindow viewModel)
            {
                viewModel.Close +=
                    () => { this.Close(); };

                Closing += (s, e) => { e.Cancel = !viewModel.CanClose(); };
            }
        }
       
    }
}
