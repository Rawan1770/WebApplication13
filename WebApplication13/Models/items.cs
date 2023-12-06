using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication13.Models
{
   
    public class items
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string descr { get; set; }
        public int price { get; set; }
        public string discount { get; set; }

        [BindProperty, DataType(DataType.Date)]
        public int category { get; set; }
        public int quantity { get; set; }
        public string imgfilename { get; set; }
    }
}
