using UnityEngine;

/// <summary>
/// The creator of bullets and rounds of bullets.
/// </summary>
class BulletManager : MonoBehaviour
{
    // The base speed of the bullets
    private static float baseSpeed = 140;

    // The baseAngle of the bullets for the round generation
    private static float baseAngle = -90;

    /// <summary>
    /// Places a bullet in a certain part of the map.
    /// </summary>
    /// <param name="parent">The game object of the game area</param>
    /// <param name="bullet">The game object of the bullet</param>
    /// <param name="position">The position the bullet will spawn on</param>
    /// <param name="rotation">The initial rotation of the bullet</param>
    /// <param name="extraSpeed">Any extra speed for the bullet</param>
    /// <param name="acceleration">Horizontal acceleration for the bullet</param>
    /// <param name="edges">The coords limits for the bullet to exist</param>
    public static void Place(GameObject parent, GameObject bullet, Vector3 position, float rotation, float extraSpeed, float acceleration, float[] edges)
    {
        // Creates the bullet
        GameObject bulletObject = Instantiate(bullet, position, Quaternion.Euler(0, 0, rotation), parent.transform);

        // I looked in ChatGPT how to access the BulletMovement component from another class
        BulletMovement movement = bulletObject.GetComponent<BulletMovement>();

        // Gets the speed the bullet will have
        Vector3 angleSpeed = VectorManager.Create(baseSpeed + extraSpeed, rotation);

        // The speed, acceleration and bullet area limits are established
        movement.speed = angleSpeed;
        movement.acceleration = acceleration;
        movement.minX = edges[0];
        movement.minY = edges[1];
        movement.maxX = edges[2];
        movement.maxY = edges[3];
    }

    /// <summary>
    /// Creates a round of a specified amount of bullets in order
    /// to create many streams of them.
    /// </summary>
    /// <param name="parent">The game object of the game area</param>
    /// <param name="bullet">The game object of the bullet</param>
    /// <param name="position">The position the round will spawn on</param>
    /// <param name="amount">The amount of streams</param>
    /// <param name="extraAngle">An extra angle for the rotation of the bullets</param>
    /// <param name="acceleration">Horizontal acceleration of the bullets</param>
    /// <param name="extraSpeed">A math formula for any extra speed in the bullets</param>
    /// <param name="edges">The coords limits for the bullet to exist</param>
    public static void PlaceRound(GameObject parent, GameObject bullet, Vector3 position, int amount, float extraAngle, float acceleration, string extraSpeed, float[] edges)
    {
        // I get the degrees each bullet will be away from each other
        float separation = 360f / amount;

        // All the bullets are created
        for (int i = 0; i < amount; i++)
        {
            Place(parent, bullet, position, baseAngle + extraAngle + separation * i, 0, acceleration, edges);
        }
    }

}