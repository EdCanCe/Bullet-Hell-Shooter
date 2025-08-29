using UnityEngine;
using System.Collections;

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

    // The GameObjects needed to spawn and control everything
    public GameObject gameArea;
    public GameObject mainMenu;
    public GameObject player;

    public GameObject stage01Phase01;
    public GameObject stage01Phase02;
    public GameObject stage01Phase03;
    public GameObject stage02Phase01;
    public GameObject stage02Phase02;
    public GameObject stage03Phase01;

    // The amount of enemies defeated by the player
    private static int enemiesDefeated;
    private static int currentPhase;

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
        ShowMenu();
    }

    /// <summary>
    /// The method called after pressing the start game button
    /// </summary>
    public void StartGame()
    {
        // Initializes used variables
        currentStage = 1;
        currentEnemies = 0;
        currentEnemyBullets = 0;
        healthPoints = 3;
        currentPlayerBullets = 0;
        nextBulletTime = 0;
        enemiesDefeated = 0;
        currentPhase = 0;

        // Initializes the UI
        UIManager.UpdateCurrentStage(currentStage);
        UIManager.UpdateCurrentEnemies(currentEnemies);
        UIManager.UpdateCurrentEnemyBullets(currentEnemyBullets);
        UIManager.UpdateHealthPoints(healthPoints);
        UIManager.UpdateCurrentPlayerBullets(currentPlayerBullets);
        UIManager.UpdateNextBulletTime(nextBulletTime);

        // Activates the game area
        gameArea.SetActive(true);

        // Activates the player
        Instantiate(player, new Vector3(-414, -368, 0), Quaternion.Euler(0, 0, 0), gameArea.transform);

        // Hides the menu
        mainMenu.SetActive(false);
    }

    /// <summary>
    /// Modifies the current stage value.
    /// </summary>
    /// <param name="modifier">The amount it's going to be modified.</param>
    public static void ModifyCurrentStage(int modifier)
    {
        currentStage = modifier;
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

        // If the player defeated an enemy, it adds up its counter
        if (modifier < 0)
        {
            enemiesDefeated += 1;
        }
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

    /// <summary>
    /// Is called once per frame
    /// </summary>
    void Update()
    {
        // Depending on the current phase and the enemies defeated, calls another stage
        if (healthPoints == 0)
        {
            // If there are no health points, you lose the game
            Lose();
        }
        else if (currentPhase == 0)
        {
            PlayStage01();
        }
        else if (currentPhase == 1 && enemiesDefeated == 1)
        {
            PlayStage02();
        }
        else if (currentPhase == 2 && enemiesDefeated == 4)
        {
            PlayStage03();
        }
        else if (currentPhase == 3 && enemiesDefeated == 7)
        {
            // Waits some time to go to the next stage
            StartCoroutine(StageChange(2));
        }
        else if (currentPhase == 5)
        {
            PlayStage04();
        }
        else if (currentPhase == 6 && enemiesDefeated == 9)
        {
            PlayStage05();
        }
        else if (currentPhase == 7 && enemiesDefeated == 11)
        {
            // Waits some time to go to the next stage
            StartCoroutine(StageChange(3));
        }
        else if (currentPhase == 9)
        {
            PlayStage06();
        }
    }

    /// <summary>
    /// Waits some time to go to the next stage.
    /// Adds a "defeated enemy" to use it in the ifs above.
    /// </summary>
    /// <param name="nextStage"></param>
    /// <returns></returns>
    private IEnumerator StageChange(int nextStage)
    {
        currentPhase += 1;

        // The final stage should have more prep time
        float timeToWait = nextStage == 2 ? 3 : 5;

        float waitCounter = 0;

        // Waits half the time
        while (waitCounter <= timeToWait)
        {
            waitCounter += Time.deltaTime;

            yield return null;
        }

        // Updates the UI
        ModifyCurrentStage(nextStage);

        // Waits the other half
        waitCounter = 0;
        while (waitCounter <= timeToWait)
        {
            waitCounter += Time.deltaTime;

            yield return null;
        }

        // Since it goes to the final stage, changes the music
        if (nextStage == 3)
        {

        }

        // The enemy of wating time without doing nothing ;)
        currentPhase += 1;
    }

    /// <summary>
    /// Show the main menu and disables the game area.
    /// </summary>
    private void ShowMenu()
    {
        BulletManager.Initialize();

        // Shows the menu
        gameArea.SetActive(false);
        mainMenu.SetActive(true);

        // Marks that the game hasn's started yet
        currentPhase = -1;

        healthPoints = -1;
    }

    /// <summary>
    /// When the user loses, it shows the main menu.
    /// </summary>
    private void Lose()
    {
        UIManager.UpdateMenuTitle("YOU LOSE");
        UIManager.UpdatePlayButton("PLAY AGAIN");

        ShowMenu();
    }

    /// <summary>
    /// Starts the first stage.
    /// This stage is the introduction.
    /// </summary>
    private void PlayStage01()
    {
        currentPhase += 1;
        Instantiate(instance.stage01Phase01, gameArea.transform);
    }

    /// <summary>
    /// Continues the first stage.
    /// Fight against minions.
    /// </summary>
    private void PlayStage02()
    {
        currentPhase += 1;
        Instantiate(instance.stage01Phase02, gameArea.transform);
    }

    /// <summary>
    /// Ends the first stage.
    /// Fight against minions.
    /// </summary>    
    private void PlayStage03()
    {
        currentPhase += 1;
        Instantiate(instance.stage01Phase03, gameArea.transform);
    }

    /// <summary>
    /// Starts the second stage.
    /// Fight against minions.
    /// </summary>   
    private void PlayStage04()
    {
        currentPhase += 1;
        Instantiate(instance.stage02Phase01, gameArea.transform);
    }

    /// <summary>
    /// Ends the second stage.
    /// Fight against minions.
    /// </summary>   
    private void PlayStage05()
    {
        currentPhase += 1;
        Instantiate(instance.stage02Phase02, gameArea.transform);
    }

    /// <summary>
    /// Starts the third stage.
    /// Fight against the boss.
    /// </summary>   
    private void PlayStage06()
    {
        currentPhase += 1;
        Instantiate(instance.stage03Phase01, gameArea.transform);
    }
}

