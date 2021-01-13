
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
	public class RegistrantCsv
	{

		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string LastName;

		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string FirstName;

		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string Email;

		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string JobTitle;

		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string Company;

		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string Phone;

		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string Quantity;

		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string Type;

		[FieldConverter(ConverterKind.Date, "dd-MMM-yy")]
		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public DateTime OrderDate;

		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string SalesLastName;

		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string SalesFirstName;

	}
}
