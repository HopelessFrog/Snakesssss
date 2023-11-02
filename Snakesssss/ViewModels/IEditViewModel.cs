using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snakesssss.Model;

namespace Snakesssss.ViewModels
{
    public  interface IEditViewModel : ICloseWindow
    {
        public Snake Snake { get; set; }
    }
}
