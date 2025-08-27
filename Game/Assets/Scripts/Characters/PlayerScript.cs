using UnityEngine;
using System;

/// <summary>
/// Handles the input and actions the player will do.
/// </summary>
public class PlayerScript : MonoBehaviour
{
    // The sprites the player will have
    public Sprite normalSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    // The sprite renderer that will render the previous spries
    public SpriteRenderer renderer;

    // The bullet the player can shoot
    public GameObject bullet;

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
    /// Is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    void Start()
    {
        // Gets the coordinates of the game area corners
        Vector3[] corners = new Vector3[4];
        transform.parent.GetComponent<RectTransform>().GetWorldCorners(corners);

        // Gets the axis limits of the game area
        minX = corners[0].x + 4;
        minY = corners[0].y + 4;
        maxX = corners[2].x - 4;
        maxY = corners[2].y - 4;
    }

    /// <summary>
    /// Is called once per frame
    /// </summary>
    void Update()
    {
        // Gets the inputs of the player
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Depending on the current input changes the sprite
        if (horizontalInput < 0)
        {
            renderer.sprite = leftSprite;
        }
        else if (horizontalInput > 0)
        {
            renderer.sprite = rightSprite;
        }
        else
        {
            renderer.sprite = normalSprite;
        }

        // Depending on the inputs, creates a vector of the change in position in the frame
        Vector3 posChange = new Vector3(horizontalInput, verticalInput, 0).normalized * baseSpeed * Time.deltaTime * (Input.GetKey(KeyCode.L) ? 0.5f : 1f);

        // The current position and the change in position are added together
        Vector3 newPos = transform.position += posChange;

        // The new position is set whilst being limited to not go out of bounds
        transform.position = new Vector3(Math.Min(Math.Max(minX, newPos.x), maxX), Math.Min(Math.Max(minY, newPos.y), maxY));

        // The user shoots a bullet
        if (Input.GetKeyDown(KeyCode.J))
        {
            BulletManager.Place(transform.parent.gameObject, bullet, transform.position, 90, 200, 0, new float[] { minX, minY, maxX, maxY });
        }
    }
}
