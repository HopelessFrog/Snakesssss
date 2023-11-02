using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Snakesssss.Model;
using Snakesssss.ViewModels;
using Xceed.Wpf.Toolkit.Primitives;

namespace Snakesssss
{
   
    public partial class MainWindow : Window
    {
       
        IMainViewModel mainViewModel;
        public MainWindow(IMainViewModel viewModel)
        {
            InitializeComponent();
            mainViewModel = viewModel;
            mainViewModel.SetDefaultValue += MainViewModel_SetDefaultValue;
        }

        private void MainViewModel_SetDefaultValue(object? sender, EventArgs e)
        {
            AreasBox.SelectedItem = null;
            ColorsBox.SelectedItem = null;
            RatingBar.Value = 0;

        }

        private void AreasBox_OnItemSelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            mainViewModel.SnakeForCreate.Areas = AreasBox.SelectedItems.Cast<Area>().ToList();

        }
        private void ColorsBox_OnItemSelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            mainViewModel.SnakeForCreate.Colors = ColorsBox.SelectedItems.Cast<SnakeColor>().ToList();

        }
        private void AreasBox_OnItemSelectionChangedSearch(object sender, ItemSelectionChangedEventArgs e)
        {
            mainViewModel.SnakeForSearch.Areas = AreasBoxSearch.SelectedItems.Cast<Area>().ToList();

        }
        private void ColorsBox_OnItemSelectionChangedSearch(object sender, ItemSelectionChangedEventArgs e)
        {
            mainViewModel.SnakeForSearch.Colors = ColorsBoxSearch.SelectedItems.Cast<SnakeColor>().ToList();

        }

        private void RatingBar_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mainViewModel.SnakeForCreate.DangerausScore = e.NewValue;
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
    }
}