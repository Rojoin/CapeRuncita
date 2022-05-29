using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    [SerializeField] SceneController scenes;
    [SerializeField] Text final;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        final.text = scenes.puntosActuales.ToString();
    }
}
