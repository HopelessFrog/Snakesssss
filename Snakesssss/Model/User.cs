using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakesssss.Model
{
    public class User : ViewModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public bool CanEdit { get; set; }

        public bool CanPermission { get; set; }
    }
}
