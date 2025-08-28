using UnityEngine;

/// <summary>
/// Manages the game and its stages.
/// </summary>
public class GameManager : MonoBehaviour
{
    // The current value of the info displayed on the UI
    public static int currentStage;
    public static int currentEnemies;
    public static int currentEnemyBullets;
    public static int healthPoints;
    public static int currentPlayerBullets;
    public static float nextBulletTime;


    // To use singleton pattern
    private static GameManager instance;

    /// <summary>
    /// Assures only one Game Manager exists at the same time.
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
    /// Modifies the current stage value.
    /// </summary>
    /// <param name="modifier">The amount it's going to be modified.</param>
    public static void ModifyCurrentStage(int modifier)
    {
        currentStage += modifier;
        UIManager.UpdateCurrentStage(currentStage);
    }

    /// <summary>
    /// Modifies the current enemies amount.
    /// </summary>
    /// <param name="modifier">The amount it's going to be modified.</param>
    public static void ModifyCurrentEnemies(int modifier)
    {
        currentEnemies += modifier;
        UIManager.UpdateCurrentEnemies(currentEnemies);
    }

    /// <summary>
    /// Modifies the current enemy bullets amount.
    /// </summary>
    /// <param name="modifier">The amount it's going to be modified.</param>
    public static void ModifyCurrentEnemyBullets(int modifier)
    {
        currentEnemyBullets += modifier;
        UIManager.UpdateCurrentEnemyBullets(currentEnemyBullets);
    }

    /// <summary>
    /// Modifies the health points of the player.
    /// </summary>
    /// <param name="modifier">The amount it's going to be modified.</param>
    public static void ModifyHealthPoints(int modifier)
    {
        healthPoints += modifier;
        UIManager.UpdateHealthPoints(healthPoints);
    }

    /// <summary>
    /// Modifies the current player bullets amount.
    /// </summary>
    /// <param name="modifier">The amount it's going to be modified.</param>
    public static void ModifyCurrentPlayerBullets(int modifier)
    {
        currentPlayerBullets += modifier;
        UIManager.UpdateCurrentPlayerBullets(currentPlayerBullets);
    }

    /// <summary>
    /// Modifies the time needed for the player to shoot again.
    /// </summary>
    /// <param name="modifier">The amount it's going to be set to.</param>
    public static void ModifyNextBulletTime(float modifier)
    {
        nextBulletTime = modifier;
        UIManager.UpdateNextBulletTime(nextBulletTime);
    }
}
