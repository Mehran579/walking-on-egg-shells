using JetBrains.Annotations;
using UnityEngine;

public class spawning : MonoBehaviour
{
    public bool _flag = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 toReachlocation;
    public Vector2Int size;
    public Vector3Int startcell;
    Rigidbody2D rb;
    Collider2D Collider2D;
    Vector3 spawnpos;
    public gridmanager gridmanager;
    public float time;
    public float speedinx;
    public float speediny;
    //public SpriteRenderer[] sprteis;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Collider2D = GetComponent<Collider2D>();
        Collider2D.enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        foreach (Transform child in transform) 
        {
            if(child.GetComponent<SpriteRenderer>()!= null)
            {
                child.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        Invoke(nameof(enablesprites), 0.1f);
        speedinx = Mathf.Abs(transform.position.x - toReachlocation.x) / time;
        speediny = Mathf.Abs(transform.position.y - toReachlocation.y) / time;
    }
    void enablesprites()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        foreach (Transform child in transform)
        {
            if (child.GetComponent<SpriteRenderer>() != null)
            {
                child.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float speed = Random.Range(0.2f, 0.5f);
        if(Mathf.Abs(rb.position.x - toReachlocation.x) > 0.1f)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, new Vector2(toReachlocation.x, transform.position.y), speedinx * Time.fixedDeltaTime));
        }
        else
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, toReachlocation, speediny * Time.fixedDeltaTime));
        }
        if(Vector3.Distance(rb.position, toReachlocation) < 0.1f && !_flag)
        {
            Collider2D.enabled = true;
            rb.bodyType = RigidbodyType2D.Static;
            _flag = true;
        }
    }
    private void Update()
    {
        if(GetComponent<general_health_manager>().Health <= 0)
        {
            gridmanager.clearcell(startcell, size);
        }
    }
}
