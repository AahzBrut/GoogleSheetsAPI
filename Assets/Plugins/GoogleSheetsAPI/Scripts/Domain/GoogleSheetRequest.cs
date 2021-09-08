namespace GoogleSheetsAPI.Domain
{
    public class GoogleSheetRequest
    {
        public readonly string SheetId = string.Empty;
        public readonly string WorkSheetName = string.Empty;
        public readonly string CellsRange = "A1:Z1000";

        public GoogleSheetRequest()
        {
            
        }

        public GoogleSheetRequest(string sheetId, string workSheetName)
        {
            SheetId = sheetId;
            WorkSheetName = workSheetName;
        }

        public GoogleSheetRequest(string sheetId, string workSheetName, string cellsRange)
        {
            SheetId = sheetId;
            WorkSheetName = workSheetName;
            CellsRange = cellsRange;
        }
    }
}