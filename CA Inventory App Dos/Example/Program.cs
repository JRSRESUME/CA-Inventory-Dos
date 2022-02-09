using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Web.Services;
using Example.com.channeladvisor.api;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create your credentials
            
            start:
            Console.Clear();

            APICredentials cred = new APICredentials();
            cred.DeveloperKey = "2476c33f-a06b-41f7-8af3-3f337bbfdca6";
            cred.Password = "kennyb15";

            // Create the Web Service and attach the credentials
            InventoryService svc = new InventoryService();
            svc.APICredentialsValue = cred;

           
            
            Console.WriteLine("WELCOME TO THE NEW INVENTORY APP.");

            Console.WriteLine("Enter Sku");

            string[]skuList = new string [1];
            
             skuList[0] = Console.ReadLine();

            // Now just call the method

            // Now just call the method

             

             APIResultOfBoolean result0 = svc.DoesSkuExist("afe6a7ba-dd0c-4292-827d-4385fba66ae2", skuList[0]);
            if (result0.ResultData == false)
            {
                Console.WriteLine("Sku Does Not Exist");
                Console.ReadLine();
                goto start;
            }



            APIResultOfArrayOfInventoryItemResponse result1 = svc.GetInventoryItemList("afe6a7ba-dd0c-4292-827d-4385fba66ae2", skuList);
            
            Console.WriteLine(result1.ResultData[0].Title);

            APIResultOfArrayOfInventoryItemResponse result2 = svc.GetInventoryItemList("afe6a7ba-dd0c-4292-827d-4385fba66ae2", skuList);

            Console.WriteLine("Location: {0}", result1.ResultData[0].WarehouseLocation);

            APIResultOfQuantityInfoResponse result8 = svc.GetInventoryItemQuantityInfo("afe6a7ba-dd0c-4292-827d-4385fba66ae2", skuList[0]);
            Console.WriteLine("Pending Ship: {0}", result8.ResultData.PendingShipmentPooled);

            APIResultOfQuantityInfoResponse result = svc.GetInventoryItemQuantityInfo("afe6a7ba-dd0c-4292-827d-4385fba66ae2", skuList[0]);
            Console.WriteLine("Total: {0}", result.ResultData.TotalPooled);
            

            smear:

            Console.WriteLine("Enter Qty or s to restart or n for No Change");


            DateTime CurDate = DateTime.Today;
            string todaydate = CurDate.ToShortDateString();

            String Qty = Console.ReadLine();

            if (Qty == "s")
            {
                goto start;
            }

            if (Qty == "n")
            {
                goto here;

            }

            if (Qty == "")
            {
                Console.WriteLine("Cannot Have a Null Value");
                Console.ReadLine();
                goto smear;
            }

            int qty;
            int.TryParse(Qty, out qty);




            


            InventoryItemSubmit douche = new InventoryItemSubmit();
            douche.QuantityInfo = new QuantityInfoSubmit();
            douche.Sku = skuList[0];
            douche.QuantityInfo.UpdateType = "Absolute";
            douche.QuantityInfo.Total = qty;

            InventoryItemSubmit nipple = new InventoryItemSubmit();
            nipple.ReceivedInInventory = CurDate;
            nipple.Sku = skuList[0];

            APIResultOfBoolean result4 = svc.SynchInventoryItem("afe6a7ba-dd0c-4292-827d-4385fba66ae2", douche);

            if (qty > 0 && result.ResultData.Total < 1)
            {
                svc.SynchInventoryItem("afe6a7ba-dd0c-4292-827d-4385fba66ae2", nipple);
                svc.SynchInventoryItem("4d779e61-a9d6-4e63-815d-fd7094b230a3", nipple);
                svc.SynchInventoryItem("c90abf9f-feae-40e7-b560-3eb97e104250", nipple);
            }

            
            
            if (result4.ResultData == true)
            {
                Console.WriteLine("Success");
            }

            else
            {
                Console.WriteLine("Failed");
            }


        here:

            InventoryItemSubmit crap = new InventoryItemSubmit();
            crap.Sku = skuList[0];

            pooon:

            Console.WriteLine("Enter New Location or n for No Change:");
            String Location = Console.ReadLine();
            crap.WarehouseLocation = Location;

            if (Location == "n")
            {
                goto start;
            }

            if (Location == "")
            {
                Console.WriteLine("Cannot Have a Null Value");
                Console.ReadLine();
                goto pooon;
            }


            svc.SynchInventoryItem("4d779e61-a9d6-4e63-815d-fd7094b230a3", crap);

            svc.SynchInventoryItem("c90abf9f-feae-40e7-b560-3eb97e104250", crap);

            APIResultOfBoolean result3 = svc.SynchInventoryItem("afe6a7ba-dd0c-4292-827d-4385fba66ae2", crap);
            if (result3.ResultData == true)
            {
                Console.WriteLine("Success");
            }

            else
            {
                Console.WriteLine("Failed");
            }

            Console.ReadLine();

            goto start;
        }
    }
}
