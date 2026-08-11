using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public GameObject tutorialimage;
    public GameObject controlsimage;
    public GameObject waiting;
    public void OnClickPlay()
    {
        tutorialimage.SetActive(true);
    }
    public void OnClickContinue()
    {
        controlsimage.SetActive(true);
    }
    public void OnClickStart()
    {
        waiting.SetActive(true);
        SceneManager.LoadScene(1);
    }
}
