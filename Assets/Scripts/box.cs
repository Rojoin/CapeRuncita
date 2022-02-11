using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(other.transform.parent.gameObject);
        
    }    
    private void OnTriggerExit2D(Collider2D other) {
        
       Destroy(other.transform.parent.gameObject);
    }
}

