using System.Collections.Generic;

namespace GoogleSheetsAPI.Domain
{
    public class GoogleSpreadSheet
    {
        public readonly Dictionary<string, Column> Columns = new Dictionary<string, Column>();
        public readonly List<List<Cell>> Rows = new List<List<Cell>>();
    }
}