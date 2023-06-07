using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PangGame
{
    /// <summary>
    /// A factory to generate enemies and store them in a memory pool for future usage
    /// </summary>
    public class EnemyViewFactory : IEnemyViewFactory
    {
        private readonly GameObject _enemyPrefab;
        private readonly Transform _enemyParent;
        private Stack<IEnemyView> _enemyViewMemoryPool;

        private readonly IObjectResolver _container;

        public EnemyViewFactory(IObjectResolver container, GameObject enemyPrefab, Transform enemyParent)
        {
            _container = container;
            _enemyPrefab = enemyPrefab;
            _enemyParent = enemyParent;
            _enemyViewMemoryPool = new Stack<IEnemyView>();
        }

        public IEnemyView CreateIEnemyView()
        {
            if (_enemyViewMemoryPool.Count > 0)
            {
                BubbleView enemy = (BubbleView)_enemyViewMemoryPool.Pop();
                enemy.gameObject.SetActive(true);
                return enemy;
            }

            GameObject newEnemy = _container.Instantiate(_enemyPrefab, _enemyParent);
            BubbleView bubbleView = newEnemy.GetComponent<BubbleView>();

            return bubbleView;
        }

        public void ReturnIEnemyViewToPool(IEnemyView enemy)
        {
            ((EnemyView)enemy).gameObject.SetActive(false);
            _enemyViewMemoryPool.Push(enemy);
        }
    }
}
