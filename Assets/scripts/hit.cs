using UnityEngine;

public class hit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == transform.root.gameObject) return;
        if(collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<general_health_manager>().takedamage(transform.root.gameObject.GetComponent<enemy_manager>().damage);
        }
        if (collision.CompareTag("eggs"))
        {
            collision.gameObject.GetComponent<eggs>().gameover();
        }
    }
}
