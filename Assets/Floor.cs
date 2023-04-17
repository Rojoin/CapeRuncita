using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Floor : MonoBehaviour
{
    [SerializeField] GameObject[] floorTypes;
    private float timeUntilEnd;
    private float speed;
    private Vector3 initialPos;
    [SerializeField] private float initialTimer;
    private float timer = 0.0f;
    private bool activeState = false;
    private int floorIndex;

    void Start()
    {
        timer = initialTimer;
    }

    void Update()
    {
        if (activeState)
        {
            timer += Time.deltaTime * speed;
        }
    }

    public void SetInitialPosition(Vector3 pos)
    {
        initialPos = pos;
    }
    public void ResetPosition()
    {
        transform.position = initialPos;
    }
    public void HorizontalMovement(Vector3 to)
    {
        transform.position = Vector3.Lerp(initialPos, to, timer);
    }
    public void SetActiveState(bool state)
    {
        activeState = state;
    }
    public bool GetActiveState() => activeState;

    public void ChangeFloorType(bool firstFloor = false)
    {
        int aux;
        if (!firstFloor)
        {
            floorTypes[floorIndex].SetActive(false);
            aux = Random.Range(0, floorTypes.Length);
            floorIndex = aux;
            floorTypes[floorIndex].SetActive(true);
        }
        else
        {
            aux = 0;
            floorIndex = aux;
            floorTypes[floorIndex].SetActive(true);
        }

    }

    public void ResetTimer(float value  )
    {
        timer = value;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public void SetTimeUntilEnd(float time)
    {
        this.timeUntilEnd = time;
    }

}