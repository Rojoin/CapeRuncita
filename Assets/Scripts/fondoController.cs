using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fondoController : MonoBehaviour
{
    [SerializeField] PlayerController player;
     public GameObject wallpaper;
    public GameObject jugador;
    public Camera mainCamera;
    public float pointer;
    [SerializeField] float safeArea;
    // Start is called before the first frame update
    void Start()
    {
        pointer = -7;
    }

    // Update is called once per frame
    void Update()
    {
        while(pointer<jugador.transform.position.x + safeArea)
        {
            if(pointer < 0)
            {
                generarFondo();
                //generarFondoInicial();
            }
            else
            {
                generarFondo();
                //generarFondoActual();
                

            }
        }
    
    }

public void generarFondo()
{
    GameObject fondo = Instantiate(wallpaper);
            fondo.transform.SetParent(this.transform);
            Fondo background = fondo.GetComponent<Fondo>();
            fondo.transform.position = new Vector3(pointer+ background.size /2 ,5 ,2.55f);
            
            pointer+=background.size;
}
    public void generarFondoInicial()
    {
        GameObject fondo = Instantiate(wallpaper);
            fondo.transform.SetParent(this.transform);
            Fondo background = fondo.GetComponent<Fondo>();
            fondo.transform.position = new Vector3(pointer+ background.size /2 ,0 ,2.55f);
            
            pointer+=background.size;
    }
    
    public void generarFondoActual()
    {
        GameObject fondo = Instantiate(wallpaper);
            fondo.transform.SetParent(this.transform);
            Fondo background = fondo.GetComponent<Fondo>();
            fondo.transform.position = new Vector3(pointer+ background.size /2,0 ,2.55f);
            fondo.transform.position = new Vector3(fondo.transform.position.x+ 10.035f, fondo.transform.position.y,fondo.transform.position.z);
            
            pointer+=background.size;
    }
    }

