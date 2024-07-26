using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        // To ensure that buttons can play their sound effects when AudioListener.pause = true;
        this.GetComponent<AudioSource>().ignoreListenerPause = true;
    }
}
