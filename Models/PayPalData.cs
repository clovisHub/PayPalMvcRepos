using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace IPNpaypal.Models
{
    public class PayPalData
    {
        public string cmd { get; set; }
        //public string redirect_cmd { get; set; }
        public string business { get; set; }
        public string no_shipping { get; set; }
        public string @return { get; set; }
        public string cancel_return { get; set; }
        public string notify_url { get; set; }
        public string currency_code { get; set; }
        public string item_name { get; set; }
        public string quantity { get; set; }

        public int    txn_id { get; set; }

        public string amount { get; set; }
        public string actionURL { get; set; }

        public string address1{ get; set; }
        public string first_name { get; set; }
        public string payer_email{ get; set; }
        public string day_phone_a{ get; set; }
        public double total_cost { get; set; }
        

        public PayPalData(bool useSandbox)
        {
              //this.redirect_cmd = "_xclick";
              //this.cmd = "_ext-enter";

              this.cmd = "_xclick";
              

              this.business = ConfigurationManager.AppSettings["business"];
              this.cancel_return = ConfigurationManager.AppSettings["cancel_return"];
              this.@return = ConfigurationManager.AppSettings["return"];

              if (useSandbox)
              {
                  this.actionURL = ConfigurationManager.AppSettings["test_url"];
              }
              else
              {
                  this.actionURL = ConfigurationManager.AppSettings["test_url"];
              }

              // We can add parameters here, for example OrderId, CustomerId, etc….
              this.notify_url = ConfigurationManager.AppSettings["notify_url"];
              // We can add parameters here, for example OrderId, CustomerId, etc….
              this.currency_code = ConfigurationManager.AppSettings["currency_code"];

             
         }
    }
}