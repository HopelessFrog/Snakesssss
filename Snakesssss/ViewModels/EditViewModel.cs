using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DevExpress.Mvvm;
using Nelibur.ObjectMapper;
using Snakesssss.Model;

namespace Snakesssss.ViewModels
{
    public class EditViewModel : IEditViewModel
    {
        
        public Snake Snake { get; set; }
        private Snake defaultSnake;
        public Action Close { get; set; }

        public EditViewModel(Snake snake)
        {
            TinyMapper.Bind<Snake, Snake>((config =>
            {
                config.Ignore(x => x.Areas);
                config.Ignore(x => x.Colors);


            }));
            defaultSnake = TinyMapper.Map<Snake>(snake);
            this.Snake = snake;
            Snake.Areas = snake.Areas;
            Snake.Colors = snake.Colors;
            
           
        }

        public bool CanClose()
        {
            return !(string.IsNullOrEmpty(Snake.Name) || Snake.Family == null ||
                    Snake.Colors.Count == 0 || Snake.Areas.Count == 0 ||
                    Snake.PoisonType == null || Snake.Design == null ||
                    Snake.DangerausScore == null || string.IsNullOrEmpty(Snake.AiFolderPath)
                    || Snake.Family.Name == "none" || Snake.PoisonType.Name == "none" || Snake.Design.Name == "none");

        }

        public List<SnakeColor> Colors
        {
            get { return DatabaseLocator.Context.SnakesColors.ToList(); }
        }

        public List<PoisonType> PoisonTypes
        {
            get
            {
                var poisons = DatabaseLocator.Context.PoisonTypes.ToList();
                poisons.Insert(0, new PoisonType() { Name = "none" });
                return poisons;
            }
        }

        public List<Design> Designs
        {
            get
            {
                var designs = DatabaseLocator.Context.Designs.ToList();
                designs.Insert(0, new Design() { Name = "none" });
                return designs;
            }
        }

        public List<Area> Areas
        {
            get { return DatabaseLocator.Context.Areas.ToList(); }
        }

        public List<Family> Familyes
        {
            get
            {
                var families = DatabaseLocator.Context.Families.ToList();
                families.Insert(0, new Family() { Name = "none" });
                return families;
            }
        }
        public ICommand Edit
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if(CanClose())
                    {
                        Close?.Invoke();
                    }
                    else
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("Invorrect Data", "",
                            MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }

                });
            }
        }

        public ICommand Cancel
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (CanClose())
                    {
                        TinyMapper.Map<Snake, Snake>(defaultSnake, Snake);
                        Close?.Invoke();
                    }
                    else
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("Invorrect Data", "",
                            MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }

                });
            }
        }
        public ICommand OpenImage
        {
            get
            {
                return new DelegateCommand(() =>
                {

                    Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                    openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        Snake.ImagePath = openFileDialog.FileName;
                    }
                    else
                    {
                        return;
                    }


                });
            }
        }
    }
}