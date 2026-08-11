using UnityEngine;

public class conesnail : MonoBehaviour
{
    public GameObject poison_puddle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    bool flag;
    // Update is called once per frame
    void Update()
    {
        if(GetComponent<general_health_manager>().Health <= 0 && !flag)
        {
            flag = true;
            Instantiate(poison_puddle,transform.position,Quaternion.identity);
        }
    }
}
