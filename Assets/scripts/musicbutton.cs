using UnityEngine;
using UnityEngine.UI;

public class musicbutton : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite musicOnIcon;
    [SerializeField] private Sprite musicOffIcon;

    private bool musicOn = true;

    //public void ToggleMusic()
    //{

    //}
    private void Start()
    {
        musicOn = !musicmanager.Instance.IsMuted;
        buttonImage.sprite = musicOn ? musicOnIcon : musicOffIcon;
    }
    public void ToggleMusic()
    {
        musicOn = !musicOn;
        buttonImage.sprite = musicOn ? musicOnIcon : musicOffIcon;
        musicmanager.Instance.ToggleMusic();
    }
}