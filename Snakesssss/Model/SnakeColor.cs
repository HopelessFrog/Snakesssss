using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Brush = System.Windows.Media.Brush;
using Color = System.Windows.Media.Color;

namespace Snakesssss.Model
{
    public class SnakeColor : ViewModelBase
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public List<Snake> Snakes { get; set; }


        [NotMapped]
        public Brush Color
        {
            get
            {
                var color = (Color)System.Windows.Media.ColorConverter.ConvertFromString(Name);
                return new SolidColorBrush(color);
            }
        }
        public override string ToString()
        {
            return Name;
            }
    }
}
