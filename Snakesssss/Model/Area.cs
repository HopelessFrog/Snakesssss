using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakesssss.Model
{
    public class Area : ViewModelBase
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Snake> Snakes { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
