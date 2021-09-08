using GoogleSheetsAPI.Domain;

namespace GoogleSheetsAPI.Database
{
    public interface IDatabase
    {
        public void UpdateDatabase(GoogleSpreadSheet spreadSheet);
        public string AssociatedSheet { get; }
        public string AssociatedWorksheet { get; }
    }
}