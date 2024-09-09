﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MpDeportesMVC.Entidades
{
    public class ShoeSize
    {
        public int ShoeSizeId { get; set; }
        public int ShoeId { get; set; }
        public Shoe Shoe { get; set; } = null!;
        public int SizeId { get; set; }
        public Size Size { get; set; } = null!;
        public int QuantityInStock { get; set; }
    }
}
