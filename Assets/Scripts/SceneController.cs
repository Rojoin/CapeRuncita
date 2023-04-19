using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    PlayerController player;
    [SerializeField]
    private Floor[] floor;
    [SerializeField]
    Transform spawnPosition;
    [SerializeField]
    Transform endPosition;
    [SerializeField]
    GameObject powerUp;

    private float currentTimer;
    private float currentVelocity;
    [SerializeField]
    private float initialVelocity;
    [SerializeField]
    private float timeUntilUpdate = 20;
    [SerializeField]
    private float gereationOffset = 0.365f;
    [SerializeField]
    private float velocityIncrement;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }


    void Update()
    {
        if (player.isDead) return;
        TimerLogic();
        MoveScenario();
    }
    private void Init()
    {
        currentTimer = 0;
        currentVelocity = initialVelocity;
        foreach (var item in floor)
        {
            ResetFloor(item, true);
            item.SetInitialPosition(spawnPosition.position);
            item.SetSpeed(currentVelocity);
            item.SetActiveState(true);
        }
    }

    private void TimerLogic()
    {
        currentTimer += Time.deltaTime;
        if (!(currentTimer >= timeUntilUpdate)) return;
        currentVelocity += velocityIncrement;
        currentTimer -= timeUntilUpdate;
    }

    private void MoveScenario()
    {
        foreach (var item in floor)
        {
            item.HorizontalMovement(endPosition.position);
            item.SetSpeed(currentVelocity);
            if (isFloorInTheEnd(endPosition, item))
            {
                ResetFloor(item);
            }

        }
    }

    private bool isFloorInTheEnd(Transform transform, Floor floor)
    {
        return transform.position == floor.transform.position;
    }
    private void ResetFloor(Floor floor, bool isFirtTime = false)
    {
        floor.ResetPosition();
        floor.ChangeFloorType(isFirtTime);
        floor.SetActiveState(true);
        if (!isFirtTime)
        {
            floor.ResetTimer(-gereationOffset);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(Vector3.Lerp(spawnPosition.position,endPosition.position,0.5f),0.5f );
    }
}


