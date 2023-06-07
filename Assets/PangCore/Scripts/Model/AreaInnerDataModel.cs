using System.Collections.Generic;
using UnityEngine;

namespace PangGame
{
    /// <summary>
    /// A class model that is used to retreive json data.
    /// </summary>
    [System.Serializable]
    public class AreaInnerDataModel
    {
        public Color32 backgroundColor;
        public int time;
        public Vector2 playerStartingPosition;
        public List<EnemyDataModel> enemyData;
    }
}
