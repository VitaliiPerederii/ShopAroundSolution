using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.ComponentModel.DataAnnotations;

namespace ShopAround.Models
{
    public class UiShopItem
    {
        public UiShopItem()
        {
        }
        
        [HiddenInput]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool Available { get; set; }
        public byte[] Image { get; set; }
        public UiCategory Category { get; set; }
    }
}