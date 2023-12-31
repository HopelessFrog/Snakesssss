﻿using DevExpress.Mvvm;

namespace Snakesssss.Model;

public class Design : ViewModelBase
{
    public int Id { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}