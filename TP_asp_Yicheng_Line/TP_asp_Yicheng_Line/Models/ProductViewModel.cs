using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TP_asp_Yicheng_Line.Models
{
    public class ProductViewModel
    {
        [HiddenInput]
        public int? Identifiant { get; set; }

        [Required(ErrorMessage = "Le titre du produit est requis")]

        public string Titre { get; set; }

        public double Prix { get; set; }

        public int IdentifiantCategory { get; set; }

        public List<SelectListItem> Categories { get; set; }
    }
}
