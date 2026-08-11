using System.Collections;
using UnityEngine;

public class hermitcrab : MonoBehaviour
{
    public float _cooldown ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    bool candamage = true;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("human") && candamage)
        {
            collision.gameObject.GetComponent<enemy_manager>().health--;
            collision.gameObject.GetComponent<enemy_manager>().changecolor();
            StartCoroutine(cooldown());
        }
    }
    IEnumerator cooldown()
    {
        candamage = false;
        yield return new WaitForSeconds(_cooldown);
        candamage = true;
    }
}
