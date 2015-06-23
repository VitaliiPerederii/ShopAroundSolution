using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ShopAround.Models
{
    [Table("ShopItem")]
    [MetadataType(typeof(ShopItemMetadata))]
    public partial class ShopItem
    {
        internal sealed class ShopItemMetadata
        {
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            [HiddenInput]
            public int Id { get; set; }

            [Required]
            public string Name { get; set; }
        }
    }
}