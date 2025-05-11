using System;

public class FireFactory : AbstractFactory <Func<IFireStrategy>, FireFactory> {
    public FireFactory () {}

    public static void RegisterStrategy(string key, Func<IFireStrategy> strategy)
        => Instance.Register(key, strategy);

    public static IFireStrategy? GetStrategy(string key)
        => Instance.Get (key)?.Invoke ();
}