using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class gunscript : MonoBehaviour
{
    public Transform player;
    public float recoil;
    public bool canshoot = true;
    public float gun_knockback;
    public float knockbacktime;
    public float guncooldown;
    public LayerMask enemylayer;
    public ParticleSystem trail;
    public ParticleSystem muzzle;
    public Transform spanwpos;
    public ParticleSystem expo;
    public ParticleSystem[] expotest;
    public AudioClip gunshot;
    public AudioClip bullethurt;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player_manager.isbuilding) return;
        transform.position = player.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2((Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position).y, (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position).x) * Mathf.Rad2Deg);
        if (Mouse.current.leftButton.wasPressedThisFrame && canshoot)
        {
            StartCoroutine(shootcooldown());
            Debug.DrawLine(transform.position, Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Color.white, 3f);
            Vector2 dir = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir,100f,enemylayer);
            //Debug.Log("the sht that got his is" + hit.collider.name);  
            //player.gameObject.GetComponent<player_manager>().StartCoroutine(player.gameObject.GetComponent<player_manager>().knockback(0.04f));
            //player.gameObject.GetComponent<Rigidbody2D>().AddForce(-dir.normalized * recoil, ForceMode2D.Impulse);
            soundmanager.Instance.playsfx(gunshot);
            ParticleSystem _muzzle =  Instantiate(muzzle, spanwpos.position, Quaternion.identity);
            _muzzle.transform.SetParent(spanwpos, true);
            Instantiate(trail, spanwpos.position, Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f));
            if (hit.collider != null && hit.collider.CompareTag("human"))
            {
                hit.collider.gameObject.GetComponent<enemy_manager>().StartCoroutine(hit.collider.gameObject.GetComponent<enemy_manager>().knockback(knockbacktime));
                hit.collider.gameObject.GetComponent<enemy_manager>().health -= 1;
                hit.collider.gameObject.GetComponent<enemy_manager>().changecolor();
                hit.rigidbody.AddForce(dir.normalized * gun_knockback, ForceMode2D.Impulse);
                ParticleSystem _expo = Instantiate(expo, hit.point, Quaternion.identity);
                ParticleSystem _expotest = Instantiate(expotest[Random.Range(0,expotest.Length)], hit.point, Quaternion.identity);
                _expo.transform.SetParent(hit.collider.transform, true);
                _expotest.transform.SetParent(hit.collider.transform, true);
                soundmanager.Instance.playsfx(bullethurt);
                //Debug.Log("hit point is " + hit.point);
                //Debug.Log("expo position is " + _expo.transform.position);
            }
        }
    }
    public IEnumerator shootcooldown()
    {
        canshoot = false;
        yield return new WaitForSeconds(guncooldown);
        canshoot = true;
    }
}
