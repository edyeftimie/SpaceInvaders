using System;

public class BulletStrategyFactory : AbstractFactory <Func<IBulletStrategy>, BulletStrategyFactory> {
    public BulletStrategyFactory () {}

    public static void RegisterStrategy(string key, Func<IBulletStrategy> strategy)
        => Instance.Register(key, strategy);

    public static IBulletStrategy? GetStrategy(string key)
        => Instance.Get(key)?.Invoke();
}