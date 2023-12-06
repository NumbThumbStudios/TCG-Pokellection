using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using DataLibrary;
using Plugin.MauiMTAdmob;
using Plugin.InAppBilling;
using System.Diagnostics;
using System.ComponentModel;
using Plugin.Maui.AppRating;

namespace TCGPokellection.Models
{
    public class AppController
    {
        #region FIELDS / PROPERTIES
        public static bool collection_loaded = false;
        public static string RemoveAdsProductId { get; } = "remove_ads_1";

        // COLLECTION DATA //
        public static List<DeviceCollectionModel> DeviceCollection { get; set; } = new List<DeviceCollectionModel>();

        // SET DATA //
        public static SetsModel SetSelected { get; set; }

        // EXPANSION DATA //
        public static ExpansionsModel ExpansionSelected { get; set; }

        // CARD DATA //
        public static List<CardsModel> Cards { get; set; }
        public static CardsModel CardSelected { get; set; }
        public static string TCGPlayerAffiliateLink { get; set; }

        // VISUAL DATA //
        public static bool ShowVariants { get; set; } = false;

        // AD DATA //
        private static int TotalActionsClicked = 0;
        private static int TotalActionsClicked_TriggerAd = 12;

        // IAP DATA // 
        public static bool IAP_AdsRemoved { get; set; }
        #endregion




        #region METHODS
        // INITIALIZE
        public static void Init()
        {
            LoadCollection_Device();
            Get_ShowVariants();
            RestoreIAP();
            SetAdTrigger();
        }




        // INTERNET CONNECTION CHECK
        public static bool IsConnectedToNetwork()
        {
            NetworkAccess access_type = Connectivity.Current.NetworkAccess;

            if(access_type == NetworkAccess.None || access_type == NetworkAccess.Unknown)
            {
                return false;
            }

            return true;
        }




        // ADVERTISEMENTS
        public static bool AdActionClicked(int actions = 1)
        {
            TotalActionsClicked += actions;

            if (TotalActionsClicked >= TotalActionsClicked_TriggerAd)
            {
                // Verify Ads are a thing...
                RestoreIAP();

                if (IAP_AdsRemoved) { return false; }

                // Trigger ad here...
                CrossMauiMTAdmob.Current.ShowInterstitial();
                TotalActionsClicked = 0;
                return true;
            }

            if(IAP_AdsRemoved) { return false; }
            if (CrossMauiMTAdmob.Current.IsInterstitialLoaded() == false)
            {
                CrossMauiMTAdmob.Current.LoadInterstitial("ca-app-pub-6937310993044586/9283166520");
            }

            SetAdTrigger();

            return false;
        }

        public static async void SetAdTrigger()
        {
            DataAccess _data = new DataAccess();

            string sql = "SELECT tcg_api_ad_trigger_count FROM tcg_api";
            TotalActionsClicked_TriggerAd = int.Parse(await Task.Run(() => _data.LoadData_Simple(sql)));
        }




        // RESTORE IAP FROM STORE
        public async static Task RestoreIAP()
        {
            IAP_AdsRemoved = await Task.Run(() => WasItemPurchased(RemoveAdsProductId).Result);
            //IAP_AdsRemoved = false;
        }

        public static async Task<bool> WasItemPurchased(string productId)
        {
            var billing = CrossInAppBilling.Current;
            try
            {
                var connected = await billing.ConnectAsync();

                if (!connected)
                {
                    //Couldn't connect
                    return false;
                }

                //check purchases
                var idsToNotFinish = new List<string>(new[] { "myconsumable" });

                var purchases = await billing.GetPurchasesAsync(ItemType.InAppPurchase);

                //check for null just in case
                if (purchases?.Any(p => p.ProductId == productId) ?? false)
                {
                    //Purchase restored
                    // if on Android may be good to check if these purchases need to be acknowledge
                    return true;
                }
                else
                {
                    //no purchases found
                    return false;
                }
            }
            catch (InAppBillingPurchaseException purchaseEx)
            {
                //Billing Exception handle this based on the type
                Debug.WriteLine("Error: " + purchaseEx);
            }
            catch (Exception ex)
            {
                //Something has gone wrong
            }
            finally
            {
                await billing.DisconnectAsync();
            }

            return false;
        }




        // AFFILIATE LINKS
        public async static Task<string> Get_TCGPlayerAffiliateLink()
        {
            if(string.IsNullOrEmpty(TCGPlayerAffiliateLink))
            {
                DataAccess data_access = new();

                string sql = "SELECT tcg_api_affliate_link FROM tcg_api";
                TCGPlayerAffiliateLink = await Task.Run(() => data_access.LoadData_Simple(sql));
            }

            return TCGPlayerAffiliateLink;
        }




        // DEVICE COLLECTION METHODS
        public static async void LoadCollection_Device()
        {
            await Task.Delay(1000);

            string json = await SecureStorage.GetAsync("DeviceCollection");
            if (!string.IsNullOrEmpty(json))
            {
                DeviceCollection = JsonConvert.DeserializeObject<List<DeviceCollectionModel>>(json);
            }

            collection_loaded = true;
        }

        public static void AddCardToCollection_Device(CardsModel card)
        {
            AddCardToCollection_Device(card, 1);
        }

        public static void AddCardToCollection_Device(CardsModel card, int quantity)
        {
            if(quantity <= 0)
            {
                RemoveCardFromCollection_Device(card);
                return;
            }

            // CREATE NEW CARD
            DeviceCollectionModel new_card = new DeviceCollectionModel { card = card, Quantity = quantity };

            // CHECK IF CARD IS ALREADY IN DEVICE STORAGE
            if (DeviceCollection.Where(x => x.card.tcg_cards_id == new_card.card.tcg_cards_id).ToList().Count <= 0)
            {
                // ADD CARD TO DEVICE COLLECTION
                DeviceCollection.Add(new_card);
            }
            else
            {
                // UPDATE CARD QUANTITY
                DeviceCollection.Where(x => x.card.tcg_cards_id == new_card.card.tcg_cards_id).ToList()[0].Quantity = quantity;
            }

            // SAVE DEVICE COLLECTION TO DEVICE
            SecureStorage.SetAsync("DeviceCollection", JsonConvert.SerializeObject(DeviceCollection));
        }

        public static void RemoveCardFromCollection_Device(CardsModel card)
        {
            if(DeviceCollection.Where(x => x.card.tcg_cards_id == card.tcg_cards_id).ToList().Count > 0)
            {
                DeviceCollection.Remove(DeviceCollection.Where(x => x.card.tcg_cards_id == card.tcg_cards_id).First());

                // SAVE DEVICE COLLECTION TO DEVICE
                SecureStorage.SetAsync("DeviceCollection", JsonConvert.SerializeObject(DeviceCollection));
            }
        }




        // GET/SET VARIANT INFO
        public static async void Set_ShowVariants()
        {
            await SecureStorage.SetAsync("ShowVariants", ShowVariants.ToString());
        }
        public static async void Get_ShowVariants()
        {
            string show_variants = await SecureStorage.GetAsync("ShowVariants");

            if(string.IsNullOrEmpty(show_variants))
            {
                ShowVariants = false;
                Set_ShowVariants();
                return;
            }
            
            switch(show_variants)
            {
                case "True":
                case "true":
                    ShowVariants = true;
                    break;

                case "False":
                case "false":
                    ShowVariants = false;
                    break;
            }
        }
        #endregion
    }
}
