using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public GameObject parent;
    public GameObject test;
    public float enemyTime;
    private float enemyTimer;

    private float circleTimer = 360;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        enemyTimer -= Time.deltaTime;
        circleTimer -= Time.deltaTime * 72;

        if (enemyTimer <= 0) {
            enemyTimer = enemyTime;

            EnemyManager.PlaceRound(parent, test, new Vector3(0,0,0), 5, 0, 0, 15);
            EnemyManager.PlaceRound(parent, test, new Vector3(0,0,0), 5, 0, 0, -15);
        }
    }
}
