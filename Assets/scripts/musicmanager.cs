using UnityEngine;

public class musicmanager : MonoBehaviour
{
    public static musicmanager Instance;

    public AudioSource musicsource;

    private void Awake()
    {
        // Don't create another music manager when changing scenes
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        musicsource.volume = 0.1f;
        musicsource.loop = true;
        musicsource.Play();
    }

    //public void SetVolume(float volume)
    //{
    //    musicsource.volume = volume;
    //    PlayerPrefs.SetFloat("MusicVolume", volume);
    //}
    public void ToggleMusic()
    {
        musicsource.mute = !musicsource.mute;
        if (soundmanager.Instance != null)
            soundmanager.Instance.SetMute(IsMuted);
    }
    public bool IsMuted => musicsource.mute;
    private void Start()
    {
        //musicsource.volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }
}