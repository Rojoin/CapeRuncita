using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class boton : MonoBehaviour
{
    // Start is called before the first frame update
     [SerializeField] PlayerController jugador;
     [SerializeField] GameObject Boton;
    // Update is called once per frame
    void Update()
    {
        if(jugador.isDead == true)
        {
            Boton.SetActive(false);
        }
    }
}
