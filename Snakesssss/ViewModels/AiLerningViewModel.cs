using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm.UI;
using MaterialDesignExtensions.Controls;
using MaterialDesignThemes.Wpf;
using Microsoft.ML;
using Snakesssss.Model;

namespace Snakesssss.ViewModels
{
    public class AiLerningViewModel : IAiViewModel
    {
        private MLContext mlContext;
        private Snake snake;
        public AiLerningViewModel(Snake snake)
        {
            mlContext = new MLContext();
            this.snake = snake;
           
        }

        private string SetFolder()
        {
            IFolderBrowserDialogService dialogService = new FolderBrowserDialogService();
           
            if (dialogService.ShowDialog())
            {
                return dialogService.ResultPath;
            }
            else
            {
                return String.Empty;
            }

        }

        private async Task LearnModel(string path)
        {
            var data =  MLModelSnakes.LoadImageFromFolder(mlContext, path, snake.Name);
            MLModelSnakes.RetrainModel(mlContext, data);
        }
        public ICommand OpenImage
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    var folder = SetFolder();
                    await LearnModel(folder);

                });
            }
        }
        public Action Close { get; set; }
        public bool CanClose()
        {
            return true;
        }
    }
}
