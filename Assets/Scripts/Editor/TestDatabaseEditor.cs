using Game.Databases;
using GoogleSheetsAPI.Database.Editor;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(TestDatabase))]
    public class TestDatabaseEditor : AbstractDatabaseEditor {}
}