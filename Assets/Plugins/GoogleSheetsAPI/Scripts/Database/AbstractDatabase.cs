using GoogleSheetsAPI.Domain;
using UnityEngine;

namespace GoogleSheetsAPI.Database
{
    public abstract class AbstractDatabase<T> : ScriptableObject, IDatabase
    {
        [SerializeField] protected T data;

        public virtual string AssociatedSheet => "";
        public abstract string AssociatedWorksheet { get; }

        public T Data => data;

        public abstract void UpdateDatabase(GoogleSpreadSheet spreadSheet);
    }
}