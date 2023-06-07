using UnityEngine;

namespace PangGame
{
    /// <summary>
    /// An interface to pass input from the controller to the Model.
    /// </summary>
    public interface IGameService
    {
        public bool stopped { get; set; }
        public GameResultType CheckConditions();
        public void SetAreaData();
        void SetArea(int area);
        void SetInnerArea(int innerArea);
        public AreaDataModel GetAreaData();
        public AreaInnerDataModel GetAreaInnerData();
        public void SetLoseCondition();
    }
}
