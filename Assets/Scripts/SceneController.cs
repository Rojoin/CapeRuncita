using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{

    [SerializeField] Text tiempoTranscurrido;
    [SerializeField] Text puntos;
    [SerializeField] PlayerController player;
    
    public GameObject jugador;
    public Camera mainCamera;
    
    public GameObject[] customPrefabs;
    public GameObject[] coin;
    public GameObject buff;
    bool buffSpawned;
    public float pointer;
    public float safeArea = 12;
    private Time time;
    public int Timer;
    
    public bool timerActive = false;
    public float currentTime;
    public int startSeconds =0;
    public float nextUpdate =10;
    int prefabIndex;
    bool casaGenerada;
    public int puntosActuales;

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
                timerActive = false;
                casaGenerada = true;
               
            }
        }
        puntos.text = puntosActuales.ToString();
        
        while(!player.isDead && pointer<jugador.transform.position.x + safeArea)
        {
            prefabIndex = Random.Range(0,customPrefabs.Length-2);
            if(pointer < 0)
            {
                prefabIndex =8;
            }
            if(!timerActive && casaGenerada)
            {
                prefabIndex =9;
                casaGenerada =false;
            }
            GameObject objetoBloque = Instantiate(customPrefabs[prefabIndex]);
            objetoBloque.transform.SetParent(this.transform);
            Bloque bloque = objetoBloque.GetComponent<Bloque>();
            objetoBloque.transform.position = new Vector2(
                pointer+ bloque.size /2,
                0
            );
            pointer+=bloque.size;

            coinSpawner();
            if(!player.powerUp && !buffSpawned)
            {
                buffSpawner();
            }
            if(player.powerUp)
            {
                StartCoroutine("tiempoSpawnBuffo");
            }
        }
    
    }
    public void coinSpawner()
    {
        int  coinGenerator = Random.Range(0,coin.Length);
        GameObject moneda = Instantiate(coin[coinGenerator]);
            moneda.transform.SetParent(this.transform);
            coin monedaclase = moneda.GetComponent<coin>();
            int posicionX = Random.Range(20,40);
            moneda.transform.position = new Vector2(jugador.transform.position.x + posicionX, jugador.transform.position.y);
            
    }
    public void buffSpawner()
    {
            
            GameObject manzana = Instantiate(buff);
            manzana.transform.SetParent(this.transform);
            
            int posicionX = Random.Range(40,80);
            manzana.transform.position = new Vector2(jugador.transform.position.x + posicionX, jugador.transform.position.y);
            buffSpawned =true;
    }
    IEnumerator tiempoSpawnBuffo()
        {
            yield return new WaitForSeconds(10);
            buffSpawned =false;
        }
}


