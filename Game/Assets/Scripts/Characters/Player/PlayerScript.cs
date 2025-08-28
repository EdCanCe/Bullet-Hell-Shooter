using UnityEngine;
using System;

/// <summary>
/// Handles the input and actions the player will do.
/// </summary>
public class PlayerScript : MonoBehaviour
{
    // The sprites of the player
    public GameObject spriteRight;
    public GameObject spriteLeft;

    // The base speed of the player
    public float baseSpeed;

    // The movement inputs the player has
    private float horizontalInput;
    private float verticalInput;

    // The axis limits of the game area
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    /// <summary>
    /// Is called once before the first execution of Update 
    /// after the MonoBehaviour is created.
    /// </summary>
    void Start()
    {
        // Gets the coordinates of the game area corners
        float[] edges = LimitManager.GetLimits(transform.parent.gameObject, 4);

        // Gets the axis limits of the game area
        minX = edges[0];
        minY = edges[1];
        maxX = edges[2];
        maxY = edges[3];
    }

    /// <summary>
    /// Is called once per frame.
    /// </summary>
    void Update()
    {
        // Gets the inputs of the player
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Depending on the inputs, creates a vector of the change in position in the frame
        Vector3 posChange = new Vector3(horizontalInput, verticalInput, 0).normalized * baseSpeed * Time.deltaTime * (Input.GetKey(KeyCode.L) ? 0.5f : 1f);

        // The current position and the change in position are added together
        Vector3 newPos = transform.position += posChange;

        // The new position is set whilst being limited to not go out of bounds
        transform.position = new Vector3(Math.Min(Math.Max(minX, newPos.x), maxX), Math.Min(Math.Max(minY, newPos.y), maxY));

        // The user shoots a bullet
        if (Input.GetKeyDown(KeyCode.J))
        {
            // The bullet is placed on the map
            BulletManager.Place(0, transform.position, 90, 200, 0);

            // A sound is played to give the player feedback
            SoundManager.PlayerShot();
        }

        // The sprites of the player are rotated
        spriteRight.transform.Rotate(0, 0, Time.deltaTime * baseSpeed);
        spriteLeft.transform.Rotate(0, 0, Time.deltaTime * - baseSpeed);
    }
}
