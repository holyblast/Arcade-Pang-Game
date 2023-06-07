using VContainer;

namespace PangGame
{
    /// <summary>
    /// responsible for getting data and addressing game logic.
    /// </summary>
    public class GameService : IGameService
    {
        [Inject] private GameModel _gameModel;

        public bool stopped { get; set; }

        public GameService(GameModel gameModel)
        {
            _gameModel = gameModel;
        }

        public GameResultType CheckConditions()
        {
            return _gameModel.GetGameResult();
        }

        public void SetAreaData()
        {
            _gameModel.SetAreaData();
        }

        public void SetArea(int area)
        {
            _gameModel.Area = area;
        }

        public void SetInnerArea(int innerArea)
        {
            _gameModel.InnerArea = innerArea;
        }

        public AreaDataModel GetAreaData()
        {
            return _gameModel.GetAreaData();
        }

        public AreaInnerDataModel GetAreaInnerData()
        {
            return _gameModel.GetAreaInnerData();
        }

        public void SetLoseCondition()
        {
            _gameModel.gameResult = GameResultType.Lose;
        }
    }
}
