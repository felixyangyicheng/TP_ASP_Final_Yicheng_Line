using System;
using System.Collections.Generic;
using System.Text;

namespace TP_asp_Yicheng_Line.DB.Models
{
    public class User
    {
        public int Identifiant { get; set; }
        public String Nom { get; set; }
        public String Prenom { get; set; }
        public int IdentifiantCivilite { get; set; }
        public int IdentifiantRole { get; set; }
    }
}
