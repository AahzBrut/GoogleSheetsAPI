using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GoogleSheetsAPI.Domain;
using GoogleSheetsAPI.Utils;
using UnityEngine;

namespace GoogleSheetsAPI.Database
{
    public static class DatabaseHelpers
    {
        private static readonly Regex Whitespace = new Regex(@"\s+");
        
        public class ParseContext<T>
        {
            public Action<ParseContext<T>> ParseRow;
            public Action<ParseContext<T>> Init;
            public T Value;
            public GoogleSpreadSheet SpreadSheet;
            public List<Cell> Row;
            public Cell IdCell;
        }
        
        public static T ParseTable<T>(ParseContext<T> context, string column = "Id")
        {
            context.Init(context);
            
            foreach (var cells in context.SpreadSheet.Rows)
            {
                context.IdCell = cells[context.SpreadSheet.Columns[column].Index];
                context.Row = cells;
                context.ParseRow(context);
            }

            return context.Value;
        }

        public static List<string> ParseStringList(string data, char sep = '/')
        {
            var result = new List<string>();
            foreach (var element in data.Split(sep))
            {
                if (!element.Trim().IsEmpty())
                {
                    result.Add(element.Trim());
                }
            }

            return result;
        }

        public static Vector2Int ParseVector2Int(string data, char sep = ',')
        {
            var cleanSource = data
                .Replace("{", string.Empty)
                .Replace("}", string.Empty)
                .Split(sep);

            if (cleanSource.Length != 2)
            {
                throw new ArgumentException($"Wrong number of Vector2Int components: {cleanSource.Length}",
                    nameof(data));
            }

            return new Vector2Int(int.Parse(cleanSource[0]), int.Parse(cleanSource[1]));
        }

        private static string RemoveWhitespaces(string input) => 
            Whitespace.Replace(input, string.Empty);
    }
}