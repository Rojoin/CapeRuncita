using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit : MonoBehaviour
{
    public PlayerController playerController;
    public Rigidbody2D rb;
    public int Jump;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag("Enemy"))
        {
            GameObject.Destroy(other.transform.parent.gameObject);
            rb.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Jump));

            
        }
        
    }
}
