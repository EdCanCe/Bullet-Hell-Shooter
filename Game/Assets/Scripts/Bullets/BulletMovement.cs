using UnityEngine;

/// <summary>
/// The life cycle of a bullet after being generated:
///  - Movement
///  - Destruction
/// </summary>
public class BulletMovement : MonoBehaviour
{
    // The speed of the bullet
    public Vector3 speed;

    // The horizontal accelaration of the bullet
    public float acceleration;

    // The axis limits of the game area
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    /// <summary>
    /// Is called once per frame.
    /// </summary>
    void Update()
    {
        // Update the position
        transform.position += speed * Time.deltaTime;

        // If is outside the canvas gets destroyed
        float currentX = transform.position.x;
        float currentY = transform.position.y;
        if (currentX < minX || currentX > maxX || currentY < minY || currentY > maxY)
        {
            // Disables the bullet due to the use of an object pool
            BulletManager.DisableBullet(gameObject);
        }

        // Update the speed according to the acceleration
        speed = VectorManager.Rotate(speed, acceleration * Time.deltaTime);

        // Rotates the bullet
        transform.Rotate(0, 0, acceleration * Time.deltaTime);
    }
}
