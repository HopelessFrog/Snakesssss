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
using Snakesssss.Service;

namespace Snakesssss.ViewModels
{
    public class AiTrainViewModel : ViewModelBase, IAiViewModel
    {
        private bool buzy = false;
        public Visibility Loaning { get; set; }
        private MLContext mlContext;
        public Snake Snake { get; set; }
        public bool IsSelectedFolder { get; set; }

        private string selectedFolderPath;
        public AiTrainViewModel(Snake snake)
        {
            mlContext = new MLContext();
            this.Snake = snake;
            IsSelectedFolder = false;
            Loaning = Visibility.Collapsed;
            

        }

        public ICommand SetFolderCommand
        {
            get
            {
                return new DelegateCommand( () =>
                {
                    if(buzy)
                        return;
                    selectedFolderPath = SetFolder();
                    

                });
            }
        }

        private string SetFolder()
        {
           
            IFolderBrowserDialogService dialogService = new FolderBrowserDialogService();
           
            if (dialogService.ShowDialog())
            {
                IsSelectedFolder = true;
                return dialogService.ResultPath;
            }
            else
            {
                return String.Empty;
            }

        }

        public ICommand LearnModel
        {
            get
            {
                return new DelegateCommand( async () =>
                {
                    if(!IsSelectedFolder)
                        return; 
                    buzy = true;
                    Loaning = Visibility.Visible;
                     await AIService.LearnSnake(selectedFolderPath,Snake.Name);
                     Loaning = Visibility.Collapsed;

                    Close.Invoke();

                });
            }
        }

        public Action Close { get; set; }
        public bool CanClose()
        {
            if (buzy)
                return false;
            else
            {
                return true;
            }
        }
    }
}
