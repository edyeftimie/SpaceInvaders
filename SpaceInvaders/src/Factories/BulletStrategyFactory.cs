public class BulletStrategyFactory : AbstractFactory <IBulletStrategy, BulletStrategyFactory> {
    public BulletStrategyFactory () {}

    public static void RegisterStrategy(string key, IBulletStrategy strategy)
        => Instance.Register(key, strategy);

    public static IBulletStrategy? GetStrategy(string key)
        => Instance.Get(key);
}