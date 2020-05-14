
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP_asp_Yicheng_Line.DB.Models;

namespace TP_asp_Yicheng_Line.Models
{
    public class CategoriesViewModel
    {
        public List<Category> Categories { get; set; }
    }
}
