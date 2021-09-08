using GoogleSheetsAPI.Domain;
using UnityEngine;

namespace GoogleSheetsAPI.Database
{
    public abstract class AbstractDatabase<T> : ScriptableObject, IDatabase
    {
        [SerializeField] protected T data;

        public virtual string AssociatedSheet => "1ouhJ3FYE8pfKD2we2LTrRA908t9IbcELPwcn4-EKslI";
        public abstract string AssociatedWorksheet { get; }

        public T Data => data;

        public abstract void UpdateDatabase(GoogleSpreadSheet spreadSheet);
    }
}