public class FireFactory : AbstractFactory <IFireStrategy, FireFactory> {
    public FireFactory () {}

    public static void RegisterStrategy(string key, IFireStrategy strategy)
        => Instance.Register(key, strategy);

    public static IFireStrategy? GetStrategy(string key)
        => Instance.Get(key);
}