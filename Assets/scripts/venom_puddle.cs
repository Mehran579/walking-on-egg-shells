using Unity.VisualScripting;
using UnityEngine;

public class venom_puddle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public ParticleSystem poisoncloud;
    void Start()
    {
        Destroy(gameObject, 3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("human"))
        {
            Instantiate(poisoncloud,transform.position,Quaternion.identity);
            collision.GetComponent<enemy_manager>().inflictpoison();
        }
    }
}
