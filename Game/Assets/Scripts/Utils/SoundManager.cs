using UnityEngine;

/// <summary>
/// Manages the audio that plays in the game.
/// </summary>
public class SoundManager : MonoBehaviour
{
    // The audio sources of each of the sfx and songs.
    public AudioSource hitSFX;
    public AudioSource killSFX;
    public AudioSource playerShot;

    // To use singleton pattern
    private static SoundManager instance;

    /// <summary>
    /// Assures only one Sound Manager exists at the
    /// same time.
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Plays the hit sound effect.
    /// </summary>
    public static void HitSFX()
    {
        instance.hitSFX.Play(0);
    }

    /// <summary>
    /// Plays the kill sound effect.
    /// </summary>
    public static void KillSFX()
    {
        instance.killSFX.Play(0);
    }

    /// <summary>
    /// Plays the player shot sound effect.
    /// </summary>
    public static void PlayerShot()
    {
        instance.playerShot.Play(0);
    }
}
