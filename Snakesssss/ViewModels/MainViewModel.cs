using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;
using Snakesssss.Model;
using System.Drawing;
using MaterialDesignExtensions.Controls;
using Microsoft.EntityFrameworkCore;
using static MaterialDesignThemes.Wpf.Theme;

namespace Snakesssss.ViewModels
{
    public class MainViewModel : ViewModelBase , IMainViewModel
    {

        public Area AreaForCreate { get; set; }
        public Design DesignForCreate { get; set; }

        public PoisonType PoisonTypeForCreate { get; set; }

        public Family FamilyForCreate { get; set; }

        private string defaultImage = @"C:\Users\leaha\source\repos\Snakesssss\Snakesssss\Images\images.jpg";
       
        public ObservableCollection<Snake> Snakes { get; set; }

        public List<Area> Areas
        {
            get
            {
                return DatabaseLocator.Context.Areas.ToList();
            }
        }

        public List<Family> Familyes
        {
            get
            {
                return DatabaseLocator.Context.Families.ToList();
            }
        }

        private Snake selectedSnake;

        public Snake SnakeForCreate
        {
            get; set;
        }
        public event EventHandler? SetDefaultValue;

        public List<SnakeColor> Colors
        {
            get
            {
                return DatabaseLocator.Context.SnakesColors.ToList();
            }
        }

        public List<PoisonType> PoisonTypes
        {
            get
            {
                return DatabaseLocator.Context.PoisonTypes.ToList();
            }
        }

        public List<Design> Designs
        {
            get
            {
                return DatabaseLocator.Context.Designs.ToList();
            }
        }




        public ICommand AddSnakeCommand
        {
            get { return new DelegateCommand(() =>
            {
                SnakeForCreate.AiFolderPath = "qweqwe";
                DatabaseLocator.Context.Snakes.Add(SnakeForCreate);
                DatabaseLocator.Context.SaveChanges();
               // Search.Execute(this);
            }); }
        }

        public ICommand AddPoisonCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                   
                    DatabaseLocator.Context.PoisonTypes.Add(PoisonTypeForCreate);
                    DatabaseLocator.Context.SaveChanges();
                    RaisePropertiesChanged(nameof(PoisonTypes));
                });
            }
        }

        public ICommand AddAreaCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {

                    DatabaseLocator.Context.Areas.Add(AreaForCreate);
                    DatabaseLocator.Context.SaveChanges();
                    RaisePropertiesChanged(nameof(Areas));
                });
            }
        }

        public ICommand AddFamilyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {

                    DatabaseLocator.Context.Families.Add(FamilyForCreate);
                    DatabaseLocator.Context.SaveChanges();
                    RaisePropertiesChanged(nameof(Familyes));
                });
            }
        }

        public ICommand AddDesignCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {

                    DatabaseLocator.Context.Designs.Add(DesignForCreate);
                    DatabaseLocator.Context.SaveChanges();
                    RaisePropertiesChanged(nameof(Designs));
                });
            }
        }
        public ICommand Search
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    DatabaseLocator.Context.SaveChanges();
                    Snakes = new ObservableCollection<Snake>(DatabaseLocator.Context.Snakes.Include(p => p.Colors)
                        .Include(p => p.Areas).Include(p=> p.Design).Include(p => p.Family).Include(p => p.PoisonType).Include(p => p.Family).ToList());
                });
            }
        }
        public string CreateImagePath { get; set; }

        public ICommand SetDefaultPropertiesSnake
        {
            
            get { return new DelegateCommand(() =>
            {
                SnakeForCreate = new Snake();
                SnakeForCreate.ImagePath = defaultImage;
                SetDefaultValue?.Invoke(this, EventArgs.Empty);

            }); }
        }

        public ICommand SetDefaultPropertiesPoison
        {

            get
            {
                return new DelegateCommand(() =>
                {
                    PoisonTypeForCreate = new PoisonType();

                });
            }
        }

        public ICommand SetDefaultPropertiesArea
        {

            get
            {
                return new DelegateCommand(() =>
                {
                    AreaForCreate = new Area();

                });
            }
        }
        public ICommand SetDefaultPropertiesFamily
        {

            get
            {
                return new DelegateCommand(() =>
                {
                    FamilyForCreate = new Family();

                });
            }
        }

        public ICommand SetDefaultPropertiesDesign
        {

            get
            {
                return new DelegateCommand(() =>
                {
                    DesignForCreate = new Design();

                });
            }
        }
        public ICommand OpenImage
        {
            get { return new DelegateCommand(() =>
            {
               
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                if (openFileDialog.ShowDialog() == true)
                {
                    SnakeForCreate.ImagePath = openFileDialog.FileName;
                }
                else
                {
                    return;
                }


            }); }
        }

        public Snake SelectedSnake
        {
            get
            {
                return selectedSnake;
            }
            set
            {
                if (value != selectedSnake)
                {
                    selectedSnake = value;
                    RaisePropertiesChanged(nameof(SelectedSnake));
                    selectedSnake = null!;
                    RaisePropertiesChanged(nameof(SelectedSnake));
                }
            }
        }
        private void ShowInfo(object item)
        {
            return;
        }
        public MainViewModel()
        {

            Snakes = new ObservableCollection<Snake>();

            //Snakes.Add(new Snake()
            //{
            //    Name = "qQWEWQEQWEQWEQWEq", ImagePath = @"C:\\Users\\leaha\\Documents\\New folder\\zmeuyt.jpg", Areas = new List<Area>() {new Area(){Name = "qwe"}, new Area() { Name = "qwe" } , new Area() { Name = "qwe" }  },
            //    PoisonType = new PoisonType() { Name = "Black Death"}, Colors = new List<SnakeColor>() {new SnakeColor(){ Name = "Aqua"},new SnakeColor() { Name = "Black" },new SnakeColor() { Name = "BlueViolet" } }, MinAmountOfPoison = 1,MaxAmountOfPoison = 3, Design = new Design() {Name = "Something"}
            //});

            //Colors = new List<SnakeColor>();
            if (DatabaseLocator.Context.SnakesColors.ToList().Count == 0)
            {
                foreach (PropertyInfo property in typeof(Brushes).GetProperties())
                {
                    Brush brush = (Brush)property.GetValue(null, null);
                    DatabaseLocator.Context.SnakesColors.Add(new SnakeColor() { Name = property.Name });
                }

                DatabaseLocator.Context.SaveChanges();
               RaisePropertiesChanged(nameof(Colors));

            }
            
            //DatabaseLocator.Context.SnakesColors.AddRange(Colors);
            //DatabaseLocator.Context.SaveChanges();

            //Familyes = new List<Family>()
            //    { new Family() { Name = "qwe" }, new Family() { Name = "eee" }, new Family() { Name = "sss" } };

            //DatabaseLocator.Context.Families.AddRange(Familyes);
            //DatabaseLocator.Context.SaveChanges();

            //PoisonTypes = new List<PoisonType>()
            //{
            //    new PoisonType() { Name = "eqwe" }, new PoisonType() { Name = "ttt" },
            //    new PoisonType() { Name = "eqzzzwe" }
            //};
            //DatabaseLocator.Context.PoisonTypes.AddRange(PoisonTypes);
            //DatabaseLocator.Context.SaveChanges();

            //Designs = new List<Design>()
            //    { new Design() { Name = "uuuu" }, new Design() { Name = "uuuu" }, new Design() { Name = "uuuu" } };
            //DatabaseLocator.Context.Designs.AddRange(Designs);
            //DatabaseLocator.Context.SaveChanges();




        }
    }
}
