using System;
using UnityEngine;

namespace Game.Models.Databases
{
    [Serializable]
    public class GridLevel
    {
        public int id;
        public string gridId;
        public int level;
        public Vector2Int cellCoord;
    }
}