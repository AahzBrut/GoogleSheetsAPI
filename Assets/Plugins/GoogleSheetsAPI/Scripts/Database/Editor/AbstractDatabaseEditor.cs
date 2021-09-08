using GoogleSheetsAPI.Domain;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GoogleSheetsAPI.Database.Editor
{
    public abstract class AbstractDatabaseEditor : UnityEditor.Editor
    {
        private IDatabase _database;

        private void OnEnable()
        {
            _database = (IDatabase) target;
        }

        public override VisualElement CreateInspectorGUI()
        {
            var container = new VisualElement();
            
            var downloadButton = new Button
            {
                text = "Download",
                style =
                {
                    // Gives it some style.
                    width = container.contentRect.width * .5f,
                    height = 30,
                    alignSelf = new StyleEnum<Align>(Align.Center)
                }
            };

            downloadButton.clickable.clicked += UpdateItems;
            container.Add(downloadButton);
            
            var iterator = serializedObject.GetIterator();
            if (iterator.NextVisible(true))
            {
                do
                {
                    var propertyField = new PropertyField(iterator.Copy()) { name = "PropertyField:" + iterator.propertyPath };
 
                    if (iterator.propertyPath == "m_Script" && serializedObject.targetObject != null)
                        propertyField.SetEnabled(false);
 
                    container.Add(propertyField);
                }
                while (iterator.NextVisible(false));
            }
 
            return container;
        }

        private void UpdateItems() =>
            GoogleSheetsManager.ReadPublicSpreadSheet(
                new GoogleSheetRequest(_database.AssociatedSheet, _database.AssociatedWorksheet), UpdateData);

        private void UpdateData(GoogleSpreadSheet spreadSheet)
        {
            _database.UpdateDatabase(spreadSheet);
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
        }
    }
}