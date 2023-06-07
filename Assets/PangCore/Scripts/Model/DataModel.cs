using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PangGame
{
    /// <summary>
    /// A class model that is used to retreive json data.
    /// </summary>
    [System.Serializable]
    public class DataModel
    {
        public Dictionary<int, Vector2> enemyVelocityData;
        public Dictionary<int, float> enemySizeData;
        public Dictionary<int, int> maxWeaponUsageInEffect;
        public List<AreaDataModel> levelData;
    }
}
