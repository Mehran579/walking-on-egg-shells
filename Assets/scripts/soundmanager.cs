using UnityEngine;

public class soundmanager : MonoBehaviour
{
    public static soundmanager Instance;

    [SerializeField] private AudioSource sfxsource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void playsfx(AudioClip clip)
    {
        sfxsource.PlayOneShot(clip);
    }

    public void SetVolume(float volume)
    {
        sfxsource.volume = volume;
    }
    public void SetMute(bool mute)
    {
        sfxsource.mute = mute;
    }
}
