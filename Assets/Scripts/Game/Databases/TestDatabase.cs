using System.Collections.Generic;
using Core.Utils;
using Game.Models.Databases;
using GoogleSheetsAPI.Database;
using GoogleSheetsAPI.Domain;
using UnityEngine;

namespace Game.Databases
{
    [CreateAssetMenu(fileName = "GridLevelDatabase", menuName = "Database/GridLevel")]
    public class TestDatabase : AbstractDatabase<List<GridLevel>>
    {
        [HideInInspector] public HashMap<string, HashMap<int, GridLevel>> index = new HashMap<string, HashMap<int, GridLevel>>();

        public override string AssociatedWorksheet => "GridLevel";
        
        public override void UpdateDatabase(GoogleSpreadSheet spreadSheet)
        {
            data = ParseGridLevelTable(spreadSheet);
            PopulateIndex();
        }

        private void PopulateIndex()
        {
            foreach (var row in data)
            {
                if (!index.ContainsKey(row.gridId)) index[row.gridId] = new HashMap<int, GridLevel>();
                index[row.gridId].Add(row.level, row);
            }
        }

        private List<GridLevel> ParseGridLevelTable(GoogleSpreadSheet spreadSheet)
        {
            var context = new DatabaseHelpers.ParseContext<List<GridLevel>>
            {
                SpreadSheet = spreadSheet,
                Init = c => c.Value = new List<GridLevel>(),
                ParseRow = ParseTestRow
            };

            return DatabaseHelpers.ParseTable(context);
        }

        private static void ParseTestRow(DatabaseHelpers.ParseContext<List<GridLevel>> context)
        {
           var result = new GridLevel { id = int.Parse(context.IdCell.Value) };
            
            foreach (var rowCell in context.Row)
            {
                switch (rowCell.Column.Name)
                {
                    case "GridId":
                        result.gridId = rowCell.Value;
                        break;
                    case "Level":
                        result.level = int.Parse(rowCell.Value);
                        break;
                    case "CellCoord":
                        result.cellCoord = DatabaseHelpers.ParseVector2Int(rowCell.Value);
                        break;
                }
            }

            context.Value.Add(result);
        }
    }
}