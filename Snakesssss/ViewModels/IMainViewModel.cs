using Snakesssss.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snakesssss.ViewModels
{
    public interface IMainViewModel
    {
        Snake SnakeForCreate { get; set; }
        Snake SnakeForSearch { get; set; }
        event EventHandler SetDefaultValue;
    }
}
