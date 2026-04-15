using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAY.Models.ViewModels
{
    public sealed class AnalyticsVM
    {
        // De totale waarde van je inkoop (voorraad x inkoop)
        public double TotalInventoryValue { get; set; }

        // De totale winst die je zou maken als alles verkocht wordt
        public double TotalPotentialProfit { get; set; }

        // Gemiddeld winstpercentage
        public double AverageMarkup { get; set; }

        // Product met de meeste winst in euro's
        public string MostProfitableProduct { get; set; } = string.Empty;

        // Totaal aantal unieke producten
        public int TotalProducts { get; set; }
    }
}
