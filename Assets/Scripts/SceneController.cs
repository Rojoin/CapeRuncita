using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{

    [SerializeField] Text tiempoTranscurrido;
    [SerializeField] PlayerController player;
    
    public GameObject jugador;
    public Camera mainCamera;
    
    public GameObject[] customPrefabs;
    public float pointer;
    public float safeArea = 12;
    private Time time;
    public int Timer;
    
    bool timerActive = false;
    public float currentTime;
    public int startSeconds =0;
    float nextUpdate =10;
    public float velocidadNueva = 2;

    // Start is called before the first frame update
    void Start()
    {
        pointer = -7;
        timerActive= true;
        currentTime=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerActive)
        {
            currentTime +=1 * Time.deltaTime;
            tiempoTranscurrido.text = currentTime.ToString("000");

            if(currentTime > nextUpdate)
            {
                
                player.modifyVelocity(velocidadNueva);
                velocidadNueva+=2; 
                nextUpdate+= 10;
            }
        }
        
        
        while(!player.isDead && pointer<jugador.transform.position.x + safeArea)
        {
            int prefabIndex = Random.Range(0,customPrefabs.Length-1);
            if(pointer < 0)
            {
                prefabIndex =3;
            }
            GameObject objetoBloque = Instantiate(customPrefabs[prefabIndex]);
            objetoBloque.transform.SetParent(this.transform);
            Bloque bloque = objetoBloque.GetComponent<Bloque>();
            objetoBloque.transform.position = new Vector2(
                pointer+ bloque.size /2,
                0
            );
            pointer+=bloque.size;
        }
    }


}
