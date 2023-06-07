using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PangGame
{
    /// <summary>
    /// An interface to pass input from the controller to the Model.
    /// </summary>
    public interface ISceneLoaderService
    {
        public void TitleScreen();
        public void GameScreen();
    }
}
