using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    // NOTE:
    // There is a way to implemnt without SingletonMonoBehaviour<T>.instanciated.
    // However, checking bool is faster than checking null.

    #region Field

    private static T instance;

    private static bool instanciated;

    #endregion Field

    #region Property

    public static T Instance
    {
        get
        {
            if (SingletonMonoBehaviour<T>.instanciated)
            {
                return instance;
            }

            if (SingletonMonoBehaviour<T>.instance == null)
            {
                SingletonMonoBehaviour<T>.instance = (T)FindObjectOfType(typeof(T));

                if (SingletonMonoBehaviour<T>.instance == null)
                {
                    GameObject gameObject = new GameObject(typeof(T).ToString());
                    SingletonMonoBehaviour<T>.instance = gameObject.AddComponent<T>();
                }
            }

            SingletonMonoBehaviour<T>.instanciated = true;

            return instance;
        }
    }

    public static bool Instanciated
    {
        get { return SingletonMonoBehaviour<T>.instanciated; }
    }

    #endregion Property

    #region Method

    protected virtual void Awake()
    {
        // NOTE:
        // Make instance in Awake to make reference performance uniformly.

        if (SingletonMonoBehaviour<T>.instance == null)
        {
            SingletonMonoBehaviour<T>.instance = (T)this;
            SingletonMonoBehaviour<T>.instanciated = true;
        }

        // NOTE:
        // If there is an instance already in the same scene, destroy this script.
        // But keep a GameObject(parent) instance is important
        // because some another scripts may attached to it.

        else if (SingletonMonoBehaviour<T>.instance != this)
        {
            Debug.LogWarning("Singleton " + typeof(T) + " is already exists.");
            GameObject.Destroy(this);
        }
    }

    protected virtual void OnDestroy()
    {
        SingletonMonoBehaviour<T>.instance = null;
        SingletonMonoBehaviour<T>.instanciated = false;
    }

    #endregion Method
}