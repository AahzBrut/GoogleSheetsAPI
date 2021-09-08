#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace GoogleSheetsAPI.Utils
{
    public static class EditorCoroutines
    {
        private class Coroutine
        {
            public IEnumerator Enumerator;
            public System.Action<bool> OnUpdate;
            public readonly List<IEnumerator> History = new List<IEnumerator>();
        }

        private static readonly List<Coroutine> Coroutines = new List<Coroutine>();

        public static void Execute(IEnumerator enumerator, System.Action<bool> onUpdate = null)
        {
            if (Coroutines.Count == 0)
            {
                EditorApplication.update += Update;
            }

            var coroutine = new Coroutine { Enumerator = enumerator, OnUpdate = onUpdate };
            Coroutines.Add(coroutine);
        }

        private static void Update()
        {
            for (var i = 0; i < Coroutines.Count; i++)
            {
                var coroutine = Coroutines[i];
                var done = !coroutine.Enumerator.MoveNext();
                if (done)
                {
                    if (coroutine.History.Count == 0)
                    {
                        Coroutines.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        done = false;
                        coroutine.Enumerator = coroutine.History[coroutine.History.Count - 1];
                        coroutine.History.RemoveAt(coroutine.History.Count - 1);
                    }
                }
                else
                {
                    if (coroutine.Enumerator.Current is IEnumerator)
                    {
                        coroutine.History.Add(coroutine.Enumerator);
                        coroutine.Enumerator = (IEnumerator)coroutine.Enumerator.Current;
                    }
                }

                coroutine.OnUpdate?.Invoke(done);
            }

            if (Coroutines.Count == 0) EditorApplication.update -= Update;
        }

        internal static void StopAll()
        {
            Coroutines.Clear();
            EditorApplication.update -= Update;
        }
    }
}
#endif
