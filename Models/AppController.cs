using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGPokellection.Models
{
    public class AppController
    {
        // SET DATA //
        public static SetsModel SetSelected { get; set; }


        // EXPANSION DATA //
        public static ExpansionsModel ExpansionSelected { get; set; }


        // CARD DATA //
        public static List<CardsModel> Cards { get; set; }
        public static CardsModel CardSelected { get; set; }
    }
}
