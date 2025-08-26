using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Vector3 speed;
    public float acceleration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update the position
        transform.position += speed * Time.deltaTime;

        // Update the speed according to the acceleration
        speed = EnemyManager.RotateVector(speed, acceleration * Time.deltaTime);

        // Rotate the bullet - With bugs
        transform.localRotation = Quaternion.Euler(0, 0, acceleration * Time.deltaTime);
    }
}
