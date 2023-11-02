using System.Drawing;
using System.Windows.Media;
using DevExpress.Mvvm;

namespace Snakesssss.Model;

public class Snake : ViewModelBase
{
    public int Id { get; set; }
    public string ImagePath { get; set; }
    public string Name { get; set; }

    public List<SnakeColor> Colors
    {
        get; 
        set;
    }

    public int MinLenght { get; set; }
    public int MaxLenght { get; set; }

    public double MinAmountOfPoison { get; set; }
    public double MaxAmountOfPoison { get; set; }

    public List<Area> Areas
    {
        get;
        set;
    }
    public Family Family { get; set; }
    public PoisonType PoisonType { get; set; }
    public Design Design { get; set; }

    public double DangerausScore
    {
        get;
        set;
    }

    public string AiFolderPath { get; set; }
}