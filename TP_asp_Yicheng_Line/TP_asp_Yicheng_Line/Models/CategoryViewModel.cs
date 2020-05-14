using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TP_asp_Yicheng_Line.Models
{
    public class CategoryViewModel
    {
        [HiddenInput]
        public int? Identifiant { get; set; }

        [Required(ErrorMessage ="Il lui faut un titre de catégorie!")]

        public  string Libelle { get; set; }

        [DataType(DataType.Date)]

        public DateTime? Date { get; set; }
    }
}
