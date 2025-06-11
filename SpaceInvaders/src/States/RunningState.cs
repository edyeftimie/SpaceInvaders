using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders;

public class RunningState : IGameState {
    private readonly PlayerController _playerController;
    private readonly EnemyManager _enemyManager;
    private readonly BulletManager _bulletManager;
    private readonly CollisionManager _collisionManager;
    private readonly EntityRenderer _renderer;

    public RunningState() {
        _playerController = new PlayerController();
        _enemyManager = new EnemyManager();
        _bulletManager = new BulletManager();
        _collisionManager = new CollisionManager();
        _renderer = new EntityRenderer();
    }

    public void Update(GameTime gameTime, SpaceInvadersGame game)
    {
        _playerController.HandleInput(gameTime, game._player, game._listOfBullets);

        EntityManager.CleanupDestroyedObjects(game._destroyableObjects, game._listOfEnemies, game._listOfBullets);

        _enemyManager.ManageEnemies(gameTime, game._listOfEnemies, game._destroyableObjects, game._listOfBullets, game._constants);

        _bulletManager.UpdateBullets(game._listOfBullets, game._destroyableObjects);

        _collisionManager.DetectCollisions(game._player, game._listOfEnemies, game._listOfBullets, game._destroyableObjects);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch, SpaceInvadersGame game) {
        _renderer.DrawGame(spriteBatch, game._player, game._listOfEnemies, game._listOfBullets);
    }
}