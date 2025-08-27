using UnityEngine;

/// <summary>
/// Manages the start of the song and the change between the
/// normal and the heroic version.
/// </summary>
public class StartSong : MonoBehaviour
{
    // The source of the song
    public AudioSource audioData;

    /// <summary>
    /// Is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    void Start()
    {
        // Plays the audio
        audioData.Play(0);
    }
}
