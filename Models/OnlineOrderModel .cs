using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace IPNpaypal.Models
{
    public class OnlineOrderModel
    {
        public OnlineOrderModel() { }

        public int OnlineOrderModelId { set; get; }

        [Display(Name = "Name")]
        [Required]
        public string ClientName { set; get; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Telephone { set; get; }

        [Display(Name = "Is delivered?")]
        [DefaultValue(false)]
        public bool IsDelivered { get; set; }

        [Display(Name = "Quantity")]
        public double QuantityPurchased { set; get; }

        [Display(Name = "Confirmation Number")]
        public string ConfirmationNumber { set; get; }

        public Decimal Total { get; set; }


        [Required]
        [NotMapped]
        [Display(Name = "Email")]
        public string ClientEmail { get; set; }
    }

   
        
}