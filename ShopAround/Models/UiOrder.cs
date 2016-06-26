using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using ShopAround.Abstract;

namespace ShopAround.Models
{
    public class UiOrder
    {
        [HiddenInput]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [HiddenInput]
        [Required]
        public System.DateTime Date { get; set; }
        
        [Required]
        public string ShipAddress { get; set; }

        [Required]
        [HiddenInput]
        public bool Proceed { get; set; }
        
        [Required]
        public string CustomerName { get; set; }
        
        [Required]
        public string CustomerEmail { get; set; }
        public ICart Cart { get; set; }
    }
}