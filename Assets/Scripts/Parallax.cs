using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Parallax : MonoBehaviour
{
    public Transform end;
    public Transform start;
    private Vector3 endpos;
    private Vector3 startpos;

    public float initialTimer;
    float timer;
    bool hasBeenReset;

    public float speed;
    void Start()
    {
        timer = initialTimer;
        hasBeenReset = true;
        endpos = new Vector3(end.position.x, end.position.y, transform.position.z);
        startpos = new Vector3(start.position.x, start.position.y, transform.position.z);
    }

    void FixedUpdate()
    {

        timer += Time.deltaTime * speed;
        transform.position = Vector3.LerpUnclamped(startpos, endpos, timer);
        Bounds bounds = GetComponent<Renderer>().bounds;

        if (!GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), bounds) && transform.position.x < end.position.x)
        {
            transform.position = start.position;
            timer = 0.0f;
        }
    }
}
