﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public interface IDictionaryMVC
    {
        string Name { get; set; }
        string Text { get; set; }
        void Save();
    }
}
