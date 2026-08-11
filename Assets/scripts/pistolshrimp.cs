using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class pistolshrimp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float ConeArc;
    public float range;
    public float coneDirection = 45f;
    public float detectionRadius;
    public bool isattacking;
    public float attackcooldown;
    public LayerMask enemylayer;
    public float knockbackduration;
    public float knockbackforce;
    public ParticleSystem impact;
    public ParticleSystem punctrail;
    public Transform impactspawnpos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<spawning>()._flag) return;
        if (isattacking) return;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius,enemylayer);

        Transform closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy.transform;
            }
        }
        if (closest == null)
            return;
        Vector2 direction = closest.position - transform.position;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Horizontal
            if (direction.x > 0)
                transform.up = Vector2.right;
            else
                transform.up = Vector2.left;
        }
        else
        {
            // Vertical
            if (direction.y > 0)
                transform.up = Vector2.up;
            else
                transform.up = Vector2.down;
        }
        StartCoroutine(attack());
    }
    public IEnumerator attack()
    {
        isattacking = true;
        Vector2 forward = transform.up;

        Collider2D[] hits =
            Physics2D.OverlapCircleAll(transform.position, range, enemylayer);

        Instantiate(impact, impactspawnpos.position, Quaternion.identity);
        punctrail.Play();
        GetComponent<general_health_manager>().Health--;
        foreach (Collider2D hit in hits)
        {
            Vector2 direction =
                (Vector2)hit.transform.position - (Vector2)transform.position;

            float angle = Vector2.Angle(forward, direction);

            if (angle <= ConeArc / 2f)
            {
                if(hit.GetComponent<Rigidbody2D>() != null)
                {
                    hit.GetComponent<enemy_manager>().StartCoroutine(hit.GetComponent<enemy_manager>().knockback(knockbackduration));
                    //hit.GetComponent<enemy_manager>().knockback(knockbackduration);
                    hit.GetComponent<Rigidbody2D>().AddForce((hit.transform.position - transform.position).normalized * knockbackforce, ForceMode2D.Impulse);
                    hit.GetComponent<enemy_manager>().health = 0;
                    hit.GetComponent<enemy_manager>().converttored();
                    //yield return new WaitForSeconds(0.1f);
                }
            }
        }
        yield return new WaitForSeconds(attackcooldown);
        isattacking = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector2 origin = transform.position;
        Vector2 forward = transform.up;

        float halfAngle = ConeArc / 2f;

        Vector2 left = Quaternion.Euler(0, 0, halfAngle) * forward;
        Vector2 right = Quaternion.Euler(0, 0, -halfAngle) * forward;

        Gizmos.DrawLine(origin, origin + left * range);
        Gizmos.DrawLine(origin, origin + right * range);

        int segments = 30;
        Vector2 previous = origin + right * range;

        for (int i = 1; i <= segments; i++)
        {
            float angle = -halfAngle + ConeArc * i / segments;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * forward;
            Vector2 current = origin + dir * range;

            Gizmos.DrawLine(previous, current);
            previous = current;
        }
        Gizmos.color = Color.pink;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
