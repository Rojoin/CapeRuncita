using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private Transform end;
    private Vector3 endpos;
    [SerializeField]
    private Transform start;
    private Vector3 startpos;

    [SerializeField]
    private float initialTimer;
    [SerializeField]
    private float speed;
    private float timer;

    private void Start()
    {
        timer = initialTimer;
        endpos = new Vector3(end.position.x, end.position.y, transform.position.z);
        startpos = new Vector3(start.position.x, start.position.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime * speed;
        transform.position = Vector3.LerpUnclamped(startpos, endpos, timer);
        var bounds = GetComponent<Renderer>().bounds;

        if (!GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera.main), bounds) &&
            transform.position.x < end.position.x)
        {
            transform.position = start.position;
            timer = 0.0f;
        }
    }
}