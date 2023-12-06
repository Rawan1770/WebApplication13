﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication13.Models
{
    public class orders
    {
        public int Id { get; set; }
        public int itemid { get; set; }
        public int userid { get; set; }
        public int quantity { get; set; }
        [BindProperty, DataType(DataType.Date)]
        public DateTime buydate { get; set; }

    }
}
