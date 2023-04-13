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
 
   // private void OnCollisionEnter(Collider other) {
   //     if(CompareTag("Destructor"))
   //     {
   //         Debug.Log("a");
   //     }
   //     else
   //     Destroy(other.transform.parent.gameObject);
   //     
   // }    
    private void OnColissionExit(Collider other) {
        
       Destroy(other.transform.parent.gameObject);
    }
}

