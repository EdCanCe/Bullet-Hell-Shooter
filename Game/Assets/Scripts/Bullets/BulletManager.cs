using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// The creator of bullets and rounds of bullets.
/// </summary>
class BulletManager : MonoBehaviour
{
    // The base speed of the bullets
    public float baseSpeed = 140;

    // The baseAngle of the bullets for the round generation
    public float baseAngle = -90;

    // The game objects of the different bullets
    public GameObject heroBullet;
    public GameObject enemyBullet01;
    public GameObject enemyBullet02;
    public GameObject enemyBullet03;

    // The game area the bullets are held in
    public GameObject parent;

    private static float[] edges;

    // The pool of bullets. I used ChatGPT to know how arrays worked in C#
    // The logic came from a tutorial I saw on YouTube
    private static List<GameObject>[] BulletPool = new List<GameObject>[4];

    private static BulletManager instance;

    /// <summary>
    /// Assures only one Bullet Manager exists at the same time,
    /// initializes the array of pools and establishes the edges.
    /// </summary>
    private void Awake()
    {
        // Used to use singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Initializes the pool of bullets
        for (int i = 0; i < BulletPool.Length; i++)
        {
            BulletPool[i] = new List<GameObject>();
        }

        // Establishes the edges
        edges = LimitManager.GetLimits(parent, -10);
    }

    /// <summary>
    /// Adds a bullet to its corresponding pool and returns
    /// that bullet.
    /// </summary>
    /// <param name="type">The type of bullet to add to the pool.</param>
    /// <returns>The new bullet.</returns>
    private static GameObject AddToPool(int type)
    {
        GameObject bulletPrefab;

        // Depending on theh type of bullet, the prefab is chosen
        switch (type)
        {
            case 0:
                bulletPrefab = instance.heroBullet;
                break;
            case 1:
                bulletPrefab = instance.enemyBullet01;
                break;
            case 2:
                bulletPrefab = instance.enemyBullet02;
                break;
            default:
                bulletPrefab = instance.enemyBullet03;
                break;
        }

        // Creates a new bullet and adds it to the pool
        GameObject newBullet = Instantiate(bulletPrefab);
        BulletPool[type].Add(newBullet);

        // Adds the bullet to the game area
        newBullet.transform.parent = instance.parent.transform;

        return newBullet;
    }

    /// <summary>
    /// Obtains a bullet of its corresponding pool.
    /// </summary>
    /// <param name="type">The type of bullet requested.</param>
    /// <returns>The bullet requested.</returns>
    private static GameObject RequestToPool(int type)
    {
        // Searches for the first inactive bullet
        for (int i = 0; i < BulletPool[type].Count; i++)
        {
            if (!BulletPool[type][i].activeSelf)
            {
                // Activates and returns the bullet
                BulletPool[type][i].SetActive(true);
                return BulletPool[type][i];
            }
        }

        // If no bullet was found, adds another one to the pool
        return AddToPool(type);
    }

    /// <summary>
    /// Places a bullet in a certain part of the map.
    /// </summary>
    /// <param name="bullet">The type of the bullet.</param>
    /// <param name="position">The position the bullet will spawn on.</param>
    /// <param name="rotation">The initial rotation of the bullet.</param>
    /// <param name="extraSpeed">Any extra speed for the bullet.</param>
    /// <param name="acceleration">Horizontal acceleration for the bullet.</param>
    public static void Place(int bullet, Vector3 position, float rotation, float extraSpeed, float acceleration)
    {
        // Asks the pool for the bullet
        GameObject bulletObject = RequestToPool(bullet);

        // Positions and rotates the bullet
        bulletObject.transform.position = new Vector3(position.x, position.y, 0);
        bulletObject.transform.rotation = Quaternion.Euler(0, 0, rotation);

        // I looked in ChatGPT how to access the BulletMovement component from another class
        BulletMovement movement = bulletObject.GetComponent<BulletMovement>();

        // Gets the speed the bullet will have
        Vector3 angleSpeed = VectorManager.Create(instance.baseSpeed + extraSpeed, rotation);

        // The speed, acceleration and bullet area limits are established
        movement.speed = angleSpeed;
        movement.acceleration = acceleration;
        movement.minX = edges[0];
        movement.minY = edges[1];
        movement.maxX = edges[2];
        movement.maxY = edges[3];

        // Adds a bullet to the counter in the UI
        EnableBullet(bulletObject);
    }

    /// <summary>
    /// Creates a round of a specified amount of bullets in order
    /// to create many streams of them.
    /// </summary>
    /// <param name="bullet">The type of the bullet.</param>
    /// <param name="position">The position the round will spawn on.</param>
    /// <param name="amount">The amount of streams.</param>
    /// <param name="extraAngle">An extra angle for the rotation of the bullets.</param>
    /// <param name="acceleration">Horizontal acceleration of the bullets.</param>
    /// <param name="extraSpeed">A math formula for any extra speed in the bullets.</param>
    public static void PlaceRound(int bullet, Vector3 position, int amount, float extraAngle, float acceleration, string extraSpeed)
    {
        // I get the degrees each bullet will be away from each other
        float separation = 360f / amount;

        // All the bullets are created
        for (int i = 0; i < amount; i++)
        {
            Place(bullet, position, instance.baseAngle + extraAngle + separation * i, 0, acceleration);
        }
    }

    /// <summary>
    /// Disables a bullet and substracts 1 to the counter of bullets
    /// in the UI.
    /// </summary>
    /// <param name="bullet">The game object of the bullet.</param>
    public static void DisableBullet(GameObject bullet)
    {
        // Disables the bullet due to the use a pool
        bullet.SetActive(false);

        // Depending on the type of bullet, modifies its counter
        if (bullet.gameObject.tag == "HeroSide")
        {
            GameManager.ModifyCurrentPlayerBullets(-1);
        }
        else
        {
            GameManager.ModifyCurrentEnemyBullets(-1);
        }
    }

    /// <summary>
    /// Adds 1 to the counter of bullets in the UI.
    /// </summary>
    /// <param name="bullet">The game object of the bullet.</param>
    public static void EnableBullet(GameObject bullet)
    {
        // Depending on the type of bullet, modifies its counter
        if (bullet.gameObject.tag == "HeroSide")
        {
            GameManager.ModifyCurrentPlayerBullets(1);
        }
        else
        {
            GameManager.ModifyCurrentEnemyBullets(1);
        }
    }
}