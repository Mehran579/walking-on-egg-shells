using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class general_health_manager : MonoBehaviour
{
    public int Health;
    public Slider slider;
    public GameObject canvas;
    public AudioClip impactsound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float maxhealth = Health;
        if(slider != null)
            slider.maxValue = maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (slider != null)
            slider.value = Health;
        if(Health <= 0)
        {
            Destroy(gameObject, 0.01f);
        }
    }
    public void takedamage(int damage)
    {
        Health -= damage;
        if (canvas != null)
            StartCoroutine(showslider());
        if(impactsound!= null)
        {
            soundmanager.Instance.playsfx(impactsound);
        }
    }
    IEnumerator showslider()
    {
        canvas.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        canvas.SetActive(false);
    }
}
