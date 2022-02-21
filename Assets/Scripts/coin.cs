using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    Collider2D collider;
    public int puntos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if(CompareTag("Obstaculo") | CompareTag("tronco"))
        {
            this.transform.position = new Vector2(this.transform.position.x -30, this.transform.position.y);
        }
    }
    
}
