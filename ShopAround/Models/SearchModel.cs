using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAround.Models
{
    public class SearchModel
    {
        public string Keyword { get; set; }
        public int CategoryId { get; set; }
        public double MinPrice { get; set; }
        public double MaxPrice { get; set; }
    }
}