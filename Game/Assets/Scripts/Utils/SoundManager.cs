using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Manages the audio that plays in the game.
/// </summary>
public class SoundManager : MonoBehaviour
{
    // The audio sources of each of the sfx and songs.
    public AudioSource hitSFX;
    public AudioSource killSFX;
    public AudioSource playerShot;
    public AudioSource enemyShot01;
    public AudioSource enemyShot02;
    public AudioSource normalSong;
    public AudioSource bossSong;

    // To use singleton pattern
    public static SoundManager instance;

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

    /// <summary>
    /// Plays the enemy shot 1 sound effect.
    /// </summary>
    public static void EnemyShot01()
    {
        instance.enemyShot01.Play(0);
    }

    /// <summary>
    /// Plays the enemy shot 2 sound effect.
    /// </summary>
    public static void EnemyShot02()
    {
        instance.enemyShot01.Play(0);
    }

    /// <summary>
    /// Starts playing the normal and boss mode songs.
    /// </summary>
    public static void StartSong()
    {
        instance.normalSong.volume = 0.35f;
        instance.bossSong.volume = 0f;

        instance.normalSong.Play(0);
        instance.bossSong.Play(0);
    }

    /// <summary>
    /// Stops the songs from playing.
    /// </summary>
    public static void EndSong()
    {
        instance.normalSong.Stop();
        instance.bossSong.Stop();
    }

    /// <summary>
    /// Switches the songs.
    /// </summary>
    public void SwitchSong()
    {
        StartCoroutine(TransitionSongs());
    }

    /// <summary>
    /// In 6 seconds, makes a transition to switch
    /// the songs.
    /// </summary>
    /// <returns></returns>
    private IEnumerator TransitionSongs()
    {
        float transitionCounter = 0;

        while (transitionCounter <= 6)
        {
            transitionCounter += Time.deltaTime;

            instance.normalSong.volume = Math.Abs(3 - transitionCounter) / 6 * 0.35f;
            instance.bossSong.volume = transitionCounter / 6 * 0.40f;

            yield return null;
        }

        instance.normalSong.volume = 0f;
        instance.bossSong.volume = 0.40f;
    }
}