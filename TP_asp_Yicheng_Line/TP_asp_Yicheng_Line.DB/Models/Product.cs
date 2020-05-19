using System;
using System.Collections.Generic;
using System.Text;

namespace TP_asp_Yicheng_Line.DB.Models
{
    public class Product
    {
        public int Identifiant { get; set; }
        public string Titre { get; set; }
        public double Prix { get; set; }

        public int IdentifiantCategory { get; set; }
    }
}
