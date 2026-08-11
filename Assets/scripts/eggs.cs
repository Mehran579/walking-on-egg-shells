using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class eggs : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject youlose;
    public TMP_Text yourhighscorewas;
    public TMP_Text timesurvived;
    public GameObject secondarycamera;
    public void gameover() 
    {
        Time.timeScale = 0;
        youlose.SetActive(true);
        timesurvived.text = "time survived is: " + Time.time.ToString("F2");
        secondarycamera.SetActive(false);
        //StartCoroutine(restert());
    }
    IEnumerator restert()
    {
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(0);
    }
}
