using IPNpaypal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace IPNpaypal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Receipt(string x)
        {
            ViewBag.x = x;
            return View();
        }

        public ActionResult RedirectedFromPayPal()
        {
            return View();
        }

        public ActionResult NotifiedFromPayPal()
        {

            

            string strSandbox = "https://www.sandbox.paypal.com/cgi-bin/webscr";
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                        | SecurityProtocolType.Tls11
                        | SecurityProtocolType.Tls12
                        | SecurityProtocolType.Ssl3;

                //change this for live
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(strSandbox);


                //set values for the request back
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.Credentials = CredentialCache.DefaultCredentials;
                byte[] param = Request.BinaryRead(Request.ContentLength);
                string strRequest = Encoding.ASCII.GetString(param);
                strRequest += "&cmd=_notify-validate";

                req.ContentLength = strRequest.Length;

              
                // for proxy

                // Send request
                
                StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
                streamOut.Write(strRequest);
                ViewBag.streamout = streamOut;
                streamOut.Close();

                StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
                string strResponse = streamIn.ReadToEnd();

               
                streamIn.Close();
                ViewBag.streamIn = strResponse;



                string response = strResponse;

                if (response == "VERIFIED")
                {
                    string transactionId = Request["txn_id"];
                    string sAmountPaid = Request["mc_gross"];
                    string orderId = Request["custom"];

                    ViewBag.tx = transactionId;
                    ViewBag.amount = sAmountPaid;
                    ViewBag.orderId = orderId;
                }
            }
            catch (Exception e)
            {
                ViewBag.error = "" + e;
            }
            return View();
        }


        public ActionResult ValidateCommand([Bind(Include = "OnlineOrderModelId,ClientName,Address,Telephone,ConfirmOrder,QuantityPurchased,ClientEmail")] OnlineOrderModel onlineOrder)
        {
            bool useSandbox = Convert.ToBoolean(ConfigurationManager.AppSettings["IsSandbox"]);
            var paypal = new PayPalData(useSandbox);

            paypal.item_name = "honey";
            paypal.amount = "12.5";

            paypal.address1 = onlineOrder.Address;
            paypal.day_phone_a = onlineOrder.Telephone;
            paypal.first_name = onlineOrder.ClientName;
            paypal.payer_email = onlineOrder.ClientEmail;
            paypal.@return = ConfigurationManager.AppSettings["return"];
            paypal.cancel_return = ConfigurationManager.AppSettings["cancel_return"];
            paypal.notify_url = ConfigurationManager.AppSettings["notify_url"];
            paypal.quantity = Convert.ToString(onlineOrder.QuantityPurchased);

            paypal.total_cost = onlineOrder.QuantityPurchased * 12.5;


            return View(paypal);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}