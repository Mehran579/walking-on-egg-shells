using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Multiplayer.Center.Common;
using UnityEngine;

public class enemy_manager : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public Transform egg;
    public bool knockedback;
    public float health;
    public int damage;
    public Collider2D righthand;
    public Collider2D lefthand;
    public SpriteRenderer[] sr;
    public Color selfcolor;
    public ParticleSystem death;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isdead) return;
        if (player_manager.isbuilding) return;
        if (knockedback) return;
        rb.MovePosition(Vector2.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("eggs").transform.position, 0.1f));
    }
    public IEnumerator knockback(float duration)
    {
        knockedback = true;
        yield return new WaitForSeconds(duration);
        knockedback = false;
    }
    public void Update()
    {
        if (isdead) return;
        if(health <= 0)
        {
            StartCoroutine(die());
        }
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("attakc"))
        {
            righthand.enabled = false;
            lefthand.enabled = false;
        }
        if(health == 5)
        {
            foreach (SpriteRenderer s in sr)
            {
                s.color = selfcolor;
            }
        }
    }
    bool isdead;
    public IEnumerator die()
    {
        gridmanager.points += 3;
        isdead = true;
        anim.SetBool("attack", false);
        GetComponent<Collider2D>().enabled = false;
        //GetComponent<Rigidbody2D>().simulated = false;
        yield return new WaitForSeconds(0.5f);
        Instantiate(death, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (player_manager.isbuilding) return;
        if (collision.gameObject.CompareTag("eggs") || collision.gameObject.layer == 7)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("attackc"))
            {
                anim.SetBool("attack", true);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("eggs") || collision.gameObject.layer == 7)
        {
            anim.SetBool("attack", false);
        }
    }
    public void enablecollider()
    { 
        righthand.enabled = true;
        lefthand.enabled = true;
    }
    public void disablecollider()
    { 
        righthand.enabled = false;
        lefthand.enabled = false;
    }
    public Color damagecolor;
    //public void changecolor()
    //{
    //    int choice = Random.Range(0, 5);
    //    if (sr[choice].color == damagecolor)
    //    {
    //        changecolor();
    //        return;
    //    }
    //    switch (choice)
    //    {
    //        case 0:
    //            sr[0].color = damagecolor;
    //            break;
    //        case 1:
    //            sr[1].color = damagecolor;
    //            break;
    //        case 2:
    //            sr[2].color = damagecolor;
    //            break;
    //        case 3:
    //            sr[3].color = damagecolor;
    //            break;
    //        case 4:
    //            sr[4].color = damagecolor;
    //            sr[5].color = damagecolor;
    //            break;
    //        default:
    //            break;
    //    }
    //}
    public void changecolor()
    {
        List<int> available = new List<int>();
        for (int i = 0; i < 5; i++)
            if (sr[i].color != damagecolor)
                available.Add(i);

        if (available.Count == 0) return; // all 5 already red, nothing left to color

        int choice = available[Random.Range(0, available.Count)];
        sr[choice].color = damagecolor;
        if (choice == 4) sr[5].color = damagecolor;
    }
    public bool inflictedwithpoison;
    public Color poisonedcolor;
    public float poisoncooldown;
    public void inflictpoison()
    {
        if (!inflictedwithpoison)
        {
            inflictedwithpoison = true;
            StartCoroutine(poisoned());
        }
    }
    public IEnumerator poisoned()
    {
        foreach(SpriteRenderer s in sr)
        {
            if(s.color != damagecolor)
            {
                s.color = poisonedcolor;
            }
        }
        while (!isdead)
        {
            health -= 1;
            changecolor();
            Instantiate(poison,transform);
            yield return new WaitForSeconds(poisoncooldown);
        }
    }
    public ParticleSystem poison;
    public void converttored()
    {
        foreach (SpriteRenderer s in sr)
        {
            s.color = damagecolor;
        }
    }
}
