using DevExpress.Mvvm;

namespace Snakesssss.Model;

public class PoisonType : ViewModelBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public override string ToString()
    {
        return Name;
    }
}