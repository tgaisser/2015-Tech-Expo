using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace DataImporter
{
    [DelimitedRecord(",")]
    [IgnoreFirst(1)]
    public class LocationCsv
    {
        public int PointsValue;
         [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string FirstName;
         [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string LastName;
         [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Company;
         [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Email;
         [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string Phone;
         [FieldQuoted('"', QuoteMode.OptionalForBoth)]
        public string LocationName;
    }
}
