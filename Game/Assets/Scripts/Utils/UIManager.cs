using UnityEngine;
using TMPro;
using System;

/// <summary>
/// Manages the changes of text in the UI.
/// </summary>
public class UIManager : MonoBehaviour
{
    // The text mesh of the elements to modify
    public TextMeshProUGUI currentStage;
    public TextMeshProUGUI currentEnemies;
    public TextMeshProUGUI currentEnemyBullets;
    public TextMeshProUGUI healthPoints;
    public TextMeshProUGUI currentPlayerBullets;
    public TextMeshProUGUI nextBulletTime;


    // To use singleton pattern
    private static UIManager instance;

    /// <summary>
    /// Assures only one UI Manager exists at the same time.
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
    }

    /// <summary>
    /// Updates the current stage value in the UI.
    /// </summary>
    /// <param name="value">The value it will be modified to.</param>
    public static void UpdateCurrentStage(int value)
    {
        instance.currentStage.text = Convert.ToString(value);
    }

    /// <summary>
    /// Updates the current enemies amount in the UI.
    /// </summary>
    /// <param name="value">The value it will be modified to.</param>
    public static void UpdateCurrentEnemies(int value)
    {
        instance.currentEnemies.text = Convert.ToString(value);
    }

    /// <summary>
    /// Updates the current enemy bullets amount in the UI.
    /// </summary>
    /// <param name="value">The value it will be modified to.</param>
    public static void UpdateCurrentEnemyBullets(int value)
    {
        instance.currentEnemyBullets.text = Convert.ToString(value);
    }

    /// <summary>
    /// Updates the health points of the player in the UI.
    /// </summary>
    /// <param name="value">The value it will be modified to.</param>
    public static void UpdateHealthPoints(int value)
    {
        instance.healthPoints.text = Convert.ToString(value) + " / 3";
    }

    /// <summary>
    /// Updates the current player bullets amount in the UI.
    /// </summary>
    /// <param name="value">The value it will be modified to.</param>
    public static void UpdateCurrentPlayerBullets(int value)
    {
        instance.currentPlayerBullets.text = Convert.ToString(value);
    }

    /// <summary>
    /// Updates the time the user needs to wait to shoot again in the UI.
    /// </summary>
    /// <param name="value">The value it will be modified to.</param>
    public static void UpdateNextBulletTime(float value)
    {
        // If the user can shoot, instead of displaying time, displays "READY!"
        if (value == 0)
        {
            instance.nextBulletTime.text = "READY!";
        }
        else
        {
            instance.nextBulletTime.text = Convert.ToString(value * 1000) + "ms";
        }
    }
}
