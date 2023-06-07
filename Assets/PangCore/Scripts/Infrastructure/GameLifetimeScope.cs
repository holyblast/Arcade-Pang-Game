using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace PangGame
{
    /// <summary>
    /// Dependency injection throught the game scene
    /// </summary>
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameObject _bubblePrefab, _hookPrefab;
        [SerializeField] private Transform _enemyParent, weaponsParent;

        protected override void Configure(IContainerBuilder builder)
        {
            // Models
            builder.Register<GameModel>(Lifetime.Scoped);
            builder.Register<PlayerModel>(Lifetime.Scoped);

            // Controller Interfaces
            //builder.Register<IUIGameService, UIGameService>(Lifetime.Scoped);
            builder.Register<IGameService, GameService>(Lifetime.Scoped);
            builder.Register<ISceneLoaderService, SceneLoaderService>(Lifetime.Scoped);
            builder.Register<IEnemyService, EnemyService>(Lifetime.Scoped);
            builder.Register<IWeaponService, WeaponService>(Lifetime.Scoped);
            builder.Register<IPlayerService, PlayerService>(Lifetime.Scoped);

            // View Interfaces
            builder.Register<IEnemyView, EnemyView>(Lifetime.Scoped);
            builder.Register<IWeaponView, WeaponView>(Lifetime.Scoped);

            // Factories
            builder.Register<IEnemyViewFactory>(container => new EnemyViewFactory(container, _bubblePrefab, _enemyParent), Lifetime.Scoped);
            builder.Register<IWeaponViewFactory>(container => new WeaponViewFactory(container, _hookPrefab, weaponsParent), Lifetime.Scoped);
            
            // Entry Points
            builder.RegisterEntryPoint<EnemyService>(Lifetime.Scoped);
        }
    }
}
