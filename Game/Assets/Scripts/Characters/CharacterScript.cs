using UnityEngine;
using System.Collections;

/// <summary>
/// The methods all characters (both player and enemies) can do.
/// </summary>
public class CharacterScript : MonoBehaviour
{
    // Activates after being hit, it's used as a cooldown
    protected bool immortal;

    // The sprites of the character
    protected SpriteRenderer[] sprites;

    // The amount of ticks the animation will play
    public int tickAmount;

    /// <summary>
    /// It's called in the start method of its child classes.
    /// </summary>
    protected void CharacterStart()
    {
        // Disables immortality
        immortal = false;

        // Gets the character sprites. It was my logic but I didn't 
        // know about GetComponentsInChildren.
        sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
    }

    /// <summary>
    /// The animation that plays after being hit.
    /// </summary>
    /// <returns></returns>
    protected IEnumerator HitAnimation()
    {
        // Enables immortality while the animation is played
        immortal = true;

        // Each tick should last a tenth a second
        float animationTick = 0.1f;
        float animationCounter = animationTick;

        // The current status of the sprite
        bool currentStatus = true;

        for (int i = 0; i < tickAmount; i++)
        {
            while (animationCounter <= animationTick)
            {
                animationCounter += Time.deltaTime;

                yield return null;
            }

            // If it can be seen, disables it
            if (currentStatus)
            {
                for (int j = 0; j < sprites.Length; j++)
                {
                    sprites[j].enabled = false;
                }
            }
            // If it cannot be seen, enables it
            else
            {
                for (int j = 0; j < sprites.Length; j++)
                {
                    sprites[j].enabled = true;
                }
            }

            // Resets the counter
            animationCounter = 0;

            // Updates the status
            currentStatus = !currentStatus;
        }

        // Ends the immortality
        immortal = false;
    }
}
