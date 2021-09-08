using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace GoogleSheetsAPI.Domain
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GoogleSheetsResponse
    {
        public string range;
        public string majorDimension;
        public List<List<string>> values;
    }
}