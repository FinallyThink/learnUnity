using UnityEngine;

// curiously recurring template pattern, CRTP
public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> {
    protected static T _instance;
    public static T Instance {
        get {
            if (_instance == null) {
                if (_instance = FindObjectOfType(typeof(T)) as T) {
                    DontDestroyOnLoad(_instance.gameObject);
                }
            }
            return _instance;
        }
    }
}
