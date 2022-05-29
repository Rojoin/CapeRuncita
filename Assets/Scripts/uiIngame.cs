using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiIngame : MonoBehaviour
{
    [SerializeField] GameObject UIinGame;
    [SerializeField] PlayerController jugador;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(jugador.isDead)
        {
                UIinGame.SetActive(false);
        }
    }
    
}
