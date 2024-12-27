using UnityEngine;

// Need to make all other refs to old singleton ref this
public class SingletonScript : MonoBehaviour
{
    public static SingletonScript Instance { get; private set; }

    protected void Awake()
    {
        // If in instance already exists and it's not this one
        if (Instance != null && Instance != this)
        {
            Debug.Log(this);
            Debug.Log("There already exists a " + this.name);
            Debug.Log(this);
            // Only want one instance at any times (hence singleton)
            Destroy(gameObject);
        }
        else
        {
            Debug.Log(this.name + " doesn't exist so all good to go");
            Instance = this;
        }
    }
}
