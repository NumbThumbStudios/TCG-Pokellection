using System;
using System.Collections.Generic;
using System.ComponentModel;
using CommunityToolkit.Mvvm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DataLibrary.Models
{
    public partial class CardsModel : ObservableObject
    {
        public int tcg_cards_id { get; set; }
        public int tcg_cards_product_id { get; set; }
        public int tcg_cards_is_product { get; set; }
        public string tcg_cards_cardNumber { get; set; }
        public string tcg_cards_name { get; set; }
        public string tcg_cards_clean_name { get; set; }
        public string tcg_cards_rarity { get; set; }
        public int tcg_cards_group_id { get; set; }
        public string tcg_cards_image_url { get; set; }
        public string tcg_cards_buy_url { get; set; }
        public DateTime tcg_cards_modified { get; set; }
        public string tcg_cards_subType { get; set; }
        public float tcg_cards_price_low { get; set; }
        public float tcg_cards_price_mid { get; set; }
        public float tcg_cards_price_high { get; set; }
        public float tcg_cards_price_market { get; set; }
        public float tcg_cards_price_directLow { get; set; }
        public DateTime tcg_cards_last_sync { get; set; }




        public string CardExpansion { get; set; }
        public string CardSet { get; set; }
        public string CardCollectionData
        {
            get => $"{CardSet}: {CardExpansion} ({tcg_cards_cardNumber})";
        }




        public string SubTypeAcronym
        {
            get
            {
                string[] sub_type_array = tcg_cards_subType.Split(' ');
                string sub_type = "";
                foreach (var sub in sub_type_array)
                {
                    sub_type += sub[0].ToString().ToUpper();
                }

                return $"{sub_type}";
            }
        }
        [ObservableProperty]
        public bool showSubType;
        public string tcg_CardsPage_DisplayName
        {
            get => $"{tcg_cards_clean_name}";
        }

        public int CardNumber { get; set; }
        public string PriceLow
        {
            get
            {
                if(tcg_cards_price_low == -1)
                {
                    return "No Data";
                }
                else
                {
                    return $"{tcg_cards_price_low:C}";
                }
            }
        }
        public string PriceMid
        {
            get
            {
                if(tcg_cards_price_mid == -1)
                {
                    return "No Data";
                }
                else
                {
                    return $"{tcg_cards_price_mid:C}";
                }
            }
        }
        public string PriceHigh
        {
            get
            {
                if(tcg_cards_price_high == -1)
                {
                    return "No Data";
                }
                else
                {
                    return $"{tcg_cards_price_high:C}";
                }
            }
        }
        public string PriceMarket
        {
            get
            {
                if(tcg_cards_price_market == -1)
                {
                    return "No Data";
                }
                else
                {
                    return $"{tcg_cards_price_market:C}";
                }
            }
        }
        public string PriceDirectLow
        {
            get
            {
                if(tcg_cards_price_directLow == -1)
                {
                    return "No Data";
                }
                else
                {
                    return $"{tcg_cards_price_directLow:C}";
                }
            }
        }

        public string? LastUpdated 
        {
            get
            {
                DateTime date = new DateTime(tcg_cards_last_sync.Year, tcg_cards_last_sync.Month, tcg_cards_last_sync.Day);
                return $"{date.ToString("MMMM dd, yyyy")}.";
            }
        }




        [ObservableProperty]
        public bool isInCollection;

        [ObservableProperty]
        public string collectionIconSource;

        [ObservableProperty]
        public string collectionCount;

        public bool ShowCardBack
        {
            get
            {
                if (tcg_cards_is_product == 1) { return false; } else { return true; }
            }
        }
    }
}
