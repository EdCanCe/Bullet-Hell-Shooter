using UnityEngine;

/// <summary>
/// Manages the game and its stages.
/// </summary>
public class GameManager : MonoBehaviour
{
    // The current value of the info displayed on the UI
    public static int currentStage = 1;
    public static int currentEnemies = 0;
    public static int currentEnemyBullets = 0;
    public static int healthPoints = 3;
    public static int currentPlayerBullets = 0;
    public static float nextBulletTime = 0;

    // The GameObjects needed to spawn and control everything
    public GameObject gameArea;
    public GameObject mainMenu;
    public GameObject player;


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
    /// Is called once before the first execution of Update
    /// after the MonoBehaviour is created.
    /// </summary>
    private void Start()
    {
        // Shows the menu
        gameArea.SetActive(false);
        mainMenu.SetActive(true);
    }

    /// <summary>
    /// The method called after pressing the start game button
    /// </summary>
    public void StartGame()
    {
        // Activates the game area
        gameArea.SetActive(true);

        // Activates the player
        Instantiate(player, new Vector3(-414, -368, 0), Quaternion.Euler(0, 0, 0), gameArea.transform);

        // Starts the first stage

        // Hides the menu
        mainMenu.SetActive(false);

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
