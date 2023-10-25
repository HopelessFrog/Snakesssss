using System.Configuration;
using System.Data;
using System.Windows;
using Snakesssss.Model;
using Snakesssss.ViewModels;

namespace Snakesssss
{
   
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DatabaseLocator.Context = new SnakeContext();
            var window = new MainWindow();
            window.Show();
           
        }

    }
}
