using VContainer;
using VContainer.Unity;

namespace PangGame
{
    /// <summary>
    /// Dependency injection throught the title scene
    /// </summary>
    public class MenuLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameModel>(Lifetime.Scoped);
            builder.Register<DataModel>(Lifetime.Scoped);
            builder.Register<IUIService, UIService>(Lifetime.Scoped);

            builder.Register<ISceneLoaderService, SceneLoaderService>(Lifetime.Scoped);
            builder.Register<IScreenToWorldService, ScreenToWorldService>(Lifetime.Scoped);
            builder.Register<IGameService, GameService>(Lifetime.Scoped);
        }
    }
}
