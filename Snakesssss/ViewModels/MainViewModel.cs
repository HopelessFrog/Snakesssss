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
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;
using System.Windows;

namespace Snakesssss.ViewModels
{
    public class MainViewModel : ViewModelBase , IMainViewModel
    {

       

        public User User { get; set; }

        public bool IsValidate { get; set; } = true;
        public bool IsValidateArea { get; set; } = true;
        public bool IsValidatePoison { get; set; } = true;
        public bool IsValidateFamily { get; set; } = true;
        public bool IsValidateDesign { get; set; } = true;
        public bool SelectedPoisonType { get; set; }
        private PoisonType poisonType;
        public PoisonType PoisonTypeSearch
        {
            get
            {
                return poisonType;
            }
            set
            {
                if (value != poisonType && value != null)
                {
                    poisonType = value;
                    SnakeForSearch.PoisonType = value;
                    RaisePropertiesChanged(nameof(PoisonTypeSearch));
                    if (poisonType.Name != "none" && poisonType != null)
                    {
                        SelectedPoisonType = true;
                    }
                    else
                    {
                        SelectedPoisonType = false;
                    }
                }
            }
        }
        public Snake SnakeForSearch { get; set; }
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
                var families =  DatabaseLocator.Context.Families.ToList();
                families.Insert(0,new Family() {Name = "none"});
                return families;
            }
        }

        public List<Family> FamilyesProperties
        {
            get
            {
                var families = DatabaseLocator.Context.Families.ToList();
                return families;
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
        public List<User> Users
        {
            get
            {
                return DatabaseLocator.Context.Users.ToList();
            }
        }
        public List<PoisonType> PoisonTypes
        {
            get
            {
                var poisons =  DatabaseLocator.Context.PoisonTypes.ToList();
                poisons.Insert(0,new PoisonType() {Name = "none"});
                return poisons; 
            }
        }
        public List<PoisonType> PoisonTypesProperties
        {
            get
            {
                var poisons = DatabaseLocator.Context.PoisonTypes.ToList();
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
        public List<Design> DesignsProperties
        {
            get
            {
                var designs = DatabaseLocator.Context.Designs.ToList();
                return designs;
            }
        }



        public ICommand AddSnakeCommand
        {
            get { return new DelegateCommand(() =>
            {
                SnakeForCreate.AiFolderPath = "qweqwe";
                if (string.IsNullOrEmpty(SnakeForCreate.Name) || SnakeForCreate.Family == null ||
                    SnakeForCreate.Colors.Count == 0 || SnakeForCreate.Areas.Count == 0 ||
                    SnakeForCreate.PoisonType == null || SnakeForCreate.Design == null ||
                    SnakeForCreate.DangerausScore == null || string.IsNullOrEmpty(SnakeForCreate.AiFolderPath))
                {
                    IsValidate = false;
                    Xceed.Wpf.Toolkit.MessageBox.Show("Invorrect Data", "",
                        MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {
                    DatabaseLocator.Context.Snakes.Add(SnakeForCreate);
                    DatabaseLocator.Context.SaveChanges();
                    IsValidate = true;
                }
               
               
               // Search.Execute(this);
            }); }
        }

        public ICommand AddPoisonCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if(string.IsNullOrEmpty(PoisonTypeForCreate.Name))
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("Invorrect Data", "",
                            MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        IsValidatePoison = false;
                    }
                    else
                    {
                        DatabaseLocator.Context.PoisonTypes.Add(PoisonTypeForCreate);
                        DatabaseLocator.Context.SaveChanges();
                        RaisePropertiesChanged(nameof(PoisonTypes));
                        RaisePropertiesChanged(nameof(PoisonTypesProperties));
                        IsValidatePoison = true;
                    }

                    
                });
            }
        }
        public ICommand SaveDataComand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    DatabaseLocator.Context.SaveChanges();

                });
            }
        }
        public ICommand AddUser
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    DatabaseLocator.Context.Users.Add(new User()
                        { Name = "user", Password = "user", CanEdit = false, CanPermission = false });
                    DatabaseLocator.Context.SaveChanges();
                    RaisePropertiesChanged(nameof(Users));

                });
            }
        }
        public ICommand DeletePoison
        {
            get
            {
                return new DelegateCommand<PoisonType>(deleteItemPoison);
            }
        }
        public ICommand DeleteArea
        {
            get
            {
                return new DelegateCommand<Area>(deleteItemArea);
            }
        }

        public ICommand DeleteFamily
        {
            get
            {
                return new DelegateCommand<Family>(deleteItemFamily);
            }
        }
        public ICommand DeleteDesign
        {
            get
            {
                return new DelegateCommand<Design>(deleteItemDesign);
            }
        }

        public ICommand AddAreaCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (string.IsNullOrEmpty(AreaForCreate.Name))
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("Invorrect Data", "",
                            MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        IsValidateArea = false;
                    }
                    else
                    {
                        DatabaseLocator.Context.Areas.Add(AreaForCreate);
                        DatabaseLocator.Context.SaveChanges();
                        RaisePropertiesChanged(nameof(Areas));
                        IsValidateArea = true;
                    }
                });
            }
        }

        public ICommand AddFamilyCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (string.IsNullOrEmpty(FamilyForCreate.Name))
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("Invorrect Data", "",
                            MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        IsValidateFamily = false;
                    }
                    else
                    {
                        DatabaseLocator.Context.Families.Add(FamilyForCreate);
                        DatabaseLocator.Context.SaveChanges();
                        RaisePropertiesChanged(nameof(Familyes));
                        RaisePropertiesChanged(nameof(FamilyesProperties));
                        IsValidateFamily = true;
                    }
                   
                });
            }
        }

        public ICommand AddDesignCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (string.IsNullOrEmpty(DesignForCreate.Name))
                    {
                        Xceed.Wpf.Toolkit.MessageBox.Show("Invorrect Data", "",
                            MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        IsValidateDesign = false;
                    }
                    else
                    {
                        DatabaseLocator.Context.Designs.Add(DesignForCreate);
                        DatabaseLocator.Context.SaveChanges();
                        RaisePropertiesChanged(nameof(Designs));
                        RaisePropertiesChanged(nameof(DesignsProperties));

                        IsValidateDesign = true;
                    }
                });
            }
        }

        public ICommand EditSnake
        {
            get
            {
                return new DelegateCommand<Snake>((snake) =>
                {
                    var viewnodel = new EditViewModel(snake);
                    var window = new EditWindow(viewnodel);
                    window.DataContext = viewnodel;
                    window.ShowDialog();
                });
            }
        }
        public ICommand Search
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    
                    var snakes =DatabaseLocator.Context.Snakes.Include(p => p.Colors)
                        .Include(p => p.Areas).Include(p=> p.Design)
                        .Include(p => p.Family).Include(p => p.PoisonType).Include(p => p.Family).AsQueryable();
                    List<Expression<Func<Snake, bool>>> conditionsSnakes = new List<Expression<Func<Snake, bool>>>();

                    if (!string.IsNullOrEmpty(SnakeForSearch.Name))
                        conditionsSnakes.Add(s => s.Name!.Contains(SnakeForSearch.Name));
                    if (SnakeForSearch.MinLenght != 0)
                        conditionsSnakes.Add(s => s.MinLenght >= SnakeForSearch.MinLenght);
                    if (SnakeForSearch.MaxLenght != 0)
                        conditionsSnakes.Add(s => s.MaxLenght <= SnakeForSearch.MaxLenght);
                    if (SnakeForSearch.Design != null && SnakeForSearch.Design.Name != "none")
                        conditionsSnakes.Add(s => s.Design.Name == SnakeForSearch.Design.Name);
                    if (SnakeForSearch.Family != null && SnakeForSearch.Family.Name != "none")
                        conditionsSnakes.Add(s => s.Family.Name == SnakeForSearch.Family.Name);
                    if (SnakeForSearch.PoisonType != null && SnakeForSearch.PoisonType.Name != "none")
                        conditionsSnakes.Add(s => s.Family.Name == SnakeForSearch.PoisonType.Name);
                    if (SnakeForSearch.Areas != null &&   SnakeForSearch.Areas.Count != 0)
                        conditionsSnakes.Add(s => s.Areas.Any(c => SnakeForSearch.Areas.Contains(c)));
                    if (SnakeForSearch.Colors!= null &&   SnakeForSearch.Colors.Count != 0)
                        conditionsSnakes.Add(s => s.Colors.Any(c => SnakeForSearch.Colors.Contains(c)));
                    if (SnakeForSearch.PoisonType != null && SnakeForSearch.PoisonType.Name != "none" )
                    {
                        if(SnakeForSearch.MaxAmountOfPoison!= 0)
                            conditionsSnakes.Add(s => s.MaxAmountOfPoison <= SnakeForSearch.MaxAmountOfPoison);
                        if(SnakeForSearch.MinAmountOfPoison != 0)
                            conditionsSnakes.Add(s => s.MinAmountOfPoison >= SnakeForSearch.MinAmountOfPoison);
                        
                    }

                    foreach (var item in conditionsSnakes)
                    {
                        snakes = snakes.Where(item);
                    }

                    Snakes = new ObservableCollection<Snake>(snakes.ToList());

                    Snakes.CollectionChanged += (s, e) =>
                    {
                        if (e.NewItems != null)
                            DatabaseLocator.Context.Snakes.AddRange(e.NewItems.Cast<Snake>());
                        else if (e.OldItems != null)
                            DatabaseLocator.Context.Snakes.RemoveRange(e.OldItems.Cast<Snake>());
                        else if (e.Action == NotifyCollectionChangedAction.Replace)
                        {
                            foreach (Snake item in e.NewItems!)
                            {
                                DatabaseLocator.Context.Snakes.Entry(item).State = EntityState.Modified;
                            }
                        }
                        DatabaseLocator.Context.SaveChanges();

                    };
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
        public ICommand DeleteSnake
        {
            get
            {
                return new DelegateCommand<Snake>(deleteItem);

            }
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

        private void deleteItem(Snake snake)
        {
            Snakes.Remove(snake);
        }
        private void deleteItemPoison(PoisonType poisonType)
        {
            DatabaseLocator.Context.PoisonTypes.Remove(poisonType);
            DatabaseLocator.Context.SaveChanges();
           RaisePropertiesChanged(nameof(PoisonTypesProperties));
           RaisePropertiesChanged(nameof(PoisonTypes));
        }
        private void deleteItemArea(Area area)
        {
            DatabaseLocator.Context.Areas.Remove(area);
            DatabaseLocator.Context.SaveChanges();
            RaisePropertiesChanged(nameof(Areas));
        }

        private void deleteItemFamily(Family family)
        {
            DatabaseLocator.Context.Families.Remove(family);
            DatabaseLocator.Context.SaveChanges();
            RaisePropertiesChanged(nameof(Familyes));
            RaisePropertiesChanged(nameof(FamilyesProperties));
        }

        private void deleteItemDesign(Design design)
        {
            DatabaseLocator.Context.Designs.Remove(design);
            DatabaseLocator.Context.SaveChanges();
            RaisePropertiesChanged(nameof(Designs));
            RaisePropertiesChanged(nameof(DesignsProperties));
        }
        private void ShowInfo(object item)
        {
            return;
        }
        public MainViewModel(User user)
        {
            User = user;
            SelectedPoisonType = false;
            Snakes = new ObservableCollection<Snake>();
            SnakeForSearch = new Snake();
           

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



            Search.Execute(this);
        }
    }
}
