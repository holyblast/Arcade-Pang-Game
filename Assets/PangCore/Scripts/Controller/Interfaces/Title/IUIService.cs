namespace PangGame
{
    /// <summary>
    /// An interface to pass input from the controller to the Model.
    /// </summary>
    public interface IUIService
    {
        public int GetPlayerStageProgress();
        public void Quit();
    }
}
