using UnityEngine;

namespace PangGame
{
    public interface IEnemyViewFactory
    {
        public IEnemyView CreateIEnemyView();
        public void ReturnIEnemyViewToPool(IEnemyView view);
    }
}