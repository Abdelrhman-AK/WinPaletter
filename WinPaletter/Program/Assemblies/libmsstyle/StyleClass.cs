﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace libmsstyle
{
    public class StyleClass
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public Dictionary<int, StylePart> Parts { get; set; }

        public StyleClass()
        {
            Parts = new Dictionary<int, StylePart>();
        }
    }
}
