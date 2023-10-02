using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class CardsModel
    {
        public int tcg_cards_id { get; set; }
        public int tcg_cards_product_id { get; set; }
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




        public string tcg_CardsPage_DisplayName
        {
            get
            {
                return $"{tcg_cards_cardNumber} - {tcg_cards_clean_name}";
            }
        }
        public bool IsInCollection { get; set; }
        public string InCollectionImage { get; set; }
        public int CardNumber { get; set; }
        public string PriceLow { get => $"{tcg_cards_price_low:C}"; }
        public string PriceMid { get => $"{tcg_cards_price_mid:C}"; }
        public string PriceHigh { get => $"{tcg_cards_price_high:C}"; }
        public string PriceMarket { get => $"{tcg_cards_price_market:C}"; }
        public string PriceDirectLow { get => $"{tcg_cards_price_directLow:C}"; }
    }
}
