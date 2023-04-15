

using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] private Floor[] floor;
    [SerializeField] Transform spawnPosition;
    [SerializeField] Transform endPosition;
    [SerializeField] GameObject powerUp;

    bool buffSpawned;
    private float currentVelocity;
    [SerializeField] private float initialVelocity;
    private float currentTimer;
    private float currentBuffTimer;
    [SerializeField] private float timeUntilUpdate = 20;
    [SerializeField] private float timeUntilEnd = 3;
    [SerializeField] private float velocityIncrement;

    // Start is called before the first frame update
    void Start()
    {
        currentTimer = 0;
        currentVelocity = initialVelocity;
        foreach (var item in floor)
        {
            ResetFloor(item, true);
            item.SetInitialPosition(spawnPosition.position);
            item.SetSpeed(currentVelocity);
            item.SetActiveState(true);
            item.SetTimeUntilEnd(timeUntilEnd);
        }
    }


    void Update()
    {
        if (player.isDead) return;
        timerLogic();
        MoveScenario();
    }

    private void timerLogic()
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
    public void buffSpawner()
    {

        GameObject manzana = Instantiate(powerUp);
        manzana.transform.SetParent(this.transform);

        int posicionX = Random.Range(40, 80);
        manzana.transform.position = new Vector2(0, 0);
        buffSpawned = true;
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
            floor.ResetTimer();
        }
    }
}


