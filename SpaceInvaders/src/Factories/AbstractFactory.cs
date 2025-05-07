using System.Collections.Generic;

public abstract class AbstractFactory<V, TDerived> where TDerived : class, new()
{
    private static TDerived? _instance;
    protected Dictionary<string, V?> _registry = new();

    public static TDerived Instance {
        get {
            if (_instance == null) {
                _instance = new TDerived();
            }
            return _instance;
        }
    }

    protected AbstractFactory() { }

    public void Register(string key, V value) {
        if (_registry.ContainsKey(key)) {
            // Handle duplicate key case
        } else {
            _registry[key] = value;
        }
    }

    public V? Get(string key) {
        if (_registry.TryGetValue(key, out var value)) {
            return value;
        }
        return default;
    }

    public bool Contains(string key) => _registry.ContainsKey(key);
}
