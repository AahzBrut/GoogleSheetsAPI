using System.Collections.Generic;
using GoogleSheetsAPI.Domain;
using ModestTree;

namespace GoogleSheetsAPI.Utils
{
    public static class Extensions
    {
        public static GoogleSpreadSheet ToSpreadSheet(this GoogleSheetsResponse response)
        {
            var result = new GoogleSpreadSheet();
            if (response.values.IsEmpty()) return result;

            var columnsRow = response.values[0];
            var columnIndex = new Dictionary<int, Column>();
            for (var i = 0; i < columnsRow.Count; i++)
            {
                result.Columns[columnsRow[i]] = new Column { Index = i, Name = columnsRow[i] };
                columnIndex[i] = result.Columns[columnsRow[i]];
            }

            for (var i = 1; i < response.values.Count; i++)
            {
                result.Rows.Add(new List<Cell>());
                for (var j = 0; j < response.values[0].Count; j++)
                {
                    result.Rows[i - 1].Add(
                        new Cell { Column = columnIndex[j], Value = response.values[i][j] }
                        );
                }
            }

            return result;
        }
    }
}