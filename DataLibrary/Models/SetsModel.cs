using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public partial class SetsModel : ObservableObject
    {
        public int ID { get; set; }
        public string? set_name { get; set; } = "";
        public string? set_image { get; set; }
        public DateTime? release_date { get; set; }
        public string? total_expansions { get; set; }
        public string? set_number_of_products { get; set; }

        public string? SetsPage_TitleDisplay
        {
            get
            {
                DateTime date = new DateTime(release_date.Value.Year, release_date.Value.Month, release_date.Value.Day);
                return $"Released on {date.ToString("MMMM dd, yyyy")}.";
            }
        }

        public string? Display_TotalExpansions
        {
            get => $"{total_expansions} expansions";
        }

        public string? Display_NumberOfProducts { get => $"{set_number_of_products} products found"; }

        [ObservableProperty]
        public string display_TotalCollectionValue;
    }
}
