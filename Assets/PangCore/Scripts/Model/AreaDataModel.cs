using System.Collections.Generic;

namespace PangGame
{
    /// <summary>
    /// A class model that is used to retreive json data.
    /// </summary>
    [System.Serializable]
    public class AreaDataModel
    {
        public BackgroundType backgroundType;
        public List<AreaInnerDataModel> areaStages;
    }
}