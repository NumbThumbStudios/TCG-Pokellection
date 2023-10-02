using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class ExpansionsModel
    {
        public int tcg_sets_id { get; set; }
        public int tcg_sets_category_id { get; set; }
        public int tcg_sets_group_id { get; set; }
        public string tcg_sets_name { get; set; }
        public string tcg_sets_name_display { get; set; }
        public string tcg_sets_image_logo { get; set; } = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRtIeAplL_i4xnX0THrFfGtZMYzKMUnsaXWLA&usqp=CAU";
        public string tcg_sets_image_symbol { get; set; }
        public string tcg_sets_series { get; set; }
        public int tcg_sets_printed_total { get; set; }
        public int tcg_sets_overall_total { get; set; }
        public string tcg_sets_abbreviation { get; set; }
        public DateTime tcg_sets_published { get; set; }
        public DateTime tcg_sets_modified { get; set; }
        public int tcg_sets_isSupplemental { get; set; }
        public int tcg_sets_showInApp { get; set; }
    }
}
