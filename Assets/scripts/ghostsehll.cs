using UnityEngine;

public class ghostsehll : MonoBehaviour
{
    //public LayerMask shell;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(deelet), 0.75f);
    }
    void deelet()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
