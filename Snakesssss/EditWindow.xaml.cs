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
using Snakesssss.Model;
using Xceed.Wpf.Toolkit.Primitives;

namespace Snakesssss
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        private IEditViewModel _viewModel;
        public EditWindow(IEditViewModel viewModel)
        {
            InitializeComponent();
            Loaded += EditWindow_Loaded;

            _viewModel = viewModel;
            _viewModel.Close = Close;
            foreach (var item in _viewModel.Snake.Areas)
            {
                AreasBox.SelectedItems.Add(item);
            }
            foreach (var item in _viewModel.Snake.Colors)
            {
                ColorsBox.SelectedItems.Add(item);
            }

        }

       

        private void EditWindow_Loaded(object sender, RoutedEventArgs e)
        {
           
            if (DataContext is ICloseWindow viewModel)
            {
                viewModel.Close +=
                    () => { this.Close(); };

                Closing += (s, e) => { e.Cancel = !viewModel.CanClose(); };
            }
        }

        private void ColorsBox_OnItemSelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            //if(ColorsBox.SelectedItems.Count != 0)
            //{
                _viewModel.Snake.Colors = ColorsBox.SelectedItems.Cast<SnakeColor>().ToList();
            //}
        }

        private void AreasBox_OnItemSelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            //if(AreasBox.SelectedItems.Count != 0)
            //{
                _viewModel.Snake.Areas = AreasBox.SelectedItems.Cast<Area>().ToList();
            //}
        }
    }
}
