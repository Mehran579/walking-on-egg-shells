using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class player_manager : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 move;
    public float speed;
    public bool knockedback;
    public gridmanager gridmanager;
    public static bool isbuilding;
    public GameObject BuildPanel;
    public GameObject ShopPanel;
    public TMP_Text btobuild;
    public TMP_Text shoptext;
    //public TMP_Text scores;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        move = ctx.ReadValue<Vector2>();
        if (move.x > 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (move.x < 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    public void OnB(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (gridmanager.canspawn)
            {
                btobuild.text = "Press b to build";
                BuildPanel.SetActive(false);
                Time.timeScale = 1f;
                isbuilding = false;
                gridmanager.canspawn = false;
                //gridmanager.SetActive(false);
                //gridmanager.enabled = false;
                ShopPanel.SetActive(false);
            }
            else
            {
                btobuild.text = "Press b to cancel";
                BuildPanel.SetActive(true);
                Time.timeScale = 0f;
                isbuilding = true;
                gridmanager.canspawn = true;
                shoptext.text = "shop";
                //gridmanager.SetActive(true);
                //gridmanager.enabled = true;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isbuilding) return;
        if (knockedback) return;
        rb.linearVelocity = move * speed;   
        //rb.linearVelocity = new Vector2(move.x * speed, move.y * speed);
    }
    public IEnumerator knockback(float duration)
    {
        knockedback = true;
        yield return new WaitForSeconds(duration);
        knockedback = false;
    }

    //public void onclick()
    //{
    //    if (gridmanager.isActiveAndEnabled)
    //    {
    //        Time.timeScale = 1f;
    //        isbuilding = false;
    //        gridmanager.enabled = false;
    //        gridmanager.canspawn = false;
    //    }
    //    else
    //    {
    //        Time.timeScale = 0f;
    //        isbuilding = true;
    //        gridmanager.enabled = true;
    //        gridmanager.canspawn = true;
    //        //gridmanager.SetActive(true);
    //    }
    //}
    public void onclickrestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
