using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    [SerializeField]
    private AudioSource backgroundSource;
    [SerializeField]
    private AudioSource soundEffects;

    public AudioClip bgm;
    public AudioClip changeLevelSFX;


    public static AudioManagerScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
    private void Start()
    {
        backgroundSource.clip = bgm;
        backgroundSource.loop = true;
        backgroundSource.Play();
    }


    public void playStateChange()
    {
        soundEffects.clip = changeLevelSFX;
        soundEffects.loop = false;
        soundEffects.Play();
    }
}
