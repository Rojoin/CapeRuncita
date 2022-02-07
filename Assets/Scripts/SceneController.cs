using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{

    public GameObject jugador;
    public Camera mainCamera;
    public GameObject[] customPrefabs;
    public float pointer;
    public float safeArea = 12;

    // Start is called before the first frame update
    void Start()
    {
        pointer = -7;
    }

    // Update is called once per frame
    void Update()
    {
        while(jugador != null && pointer<jugador.transform.position.x + safeArea)
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
