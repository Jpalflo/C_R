using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //Singleton: to use methods from this class I don't need to get a component
    public static LevelManager Instance; //Capitalize cause this is a static reference to my object

    //Panels
    [Header("Panels")]
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject winPanel;

    [Tooltip("Level time in seconds")]
    [SerializeField] private float levelTime = 60;
    private float internalLevelTime;
    public float InternalLevelTime { get => internalLevelTime; set { if (value > 0) { internalLevelTime = value; } } } //Public setter and getter

    ////PowerUps in the level
    //private int totalLevelPowerUps = 0;

    ////PowerUps a player has at some point
    //private int currentPlayerPowerUps = 0;
    //public int CurrentPlayerPowerUps { get => currentPlayerPowerUps; set => currentPlayerPowerUps = value; }

    ////Remaining PowerUps in the level
    //private int remainingPowerUps = 0;
    //public int RemainingPlayerPowerUps { get => remainingPowerUps; set => remainingPowerUps = value; }


    private void Awake()
    {
        internalLevelTime = (float)levelTime;

        Instance = this;

        //Disable the panel
        endPanel.SetActive(false);

        //restart timeScale
        Time.timeScale = 1;
    }

    private void Start()
    {
        //totalLevelPowerUps = GameObject.FindGameObjectsWithTag("PowerUp").Length;
        //Debug.Log(totalLevelPowerUps);

        //RemainingPlayerPowerUps = totalLevelPowerUps;
    }

    private void Update()
    {
        if (internalLevelTime <= 0.2f)
        {
            GameOver();
        }

        //if (currentPlayerPowerUps >= totalLevelPowerUps)
        //{
        //    GameWin();
        //}

        //remainingPowerUps = totalLevelPowerUps - currentPlayerPowerUps;
    }

    /// <summary>
    /// This method activates game over panel and pauses the game
    /// </summary>
    public void GameOver()
    {
        //Activate the panel
        endPanel.SetActive(true);

        //Set timeScale to 0 (PAUSE GAME)
        Time.timeScale = 0;
    }

    /// <summary>
    /// This method activates game win panel and pauses the game
    /// </summary>
    public void GameWin()
    {
        winPanel.SetActive(true);

        Time.timeScale = 0;
    }

    /// <summary>
    /// This method changes the scene to a scene that has a given name
    /// </summary>
    /// <param name="sceneName"></param>
    public void ChangeScene(string sceneName)
    {
        //AudioManager.Instance?.PlayButtonSound();

        SceneManager.LoadScene(sceneName);
    }

    public void GoToNextScene(int levelBeaten)
    {
        //AudioManager.Instance?.PlayButtonSound();

        if (levelBeaten >= PlayerPrefs.GetInt("LevelBeaten")) //Don´t lose levels
        {

            PlayerPrefs.SetInt("LevelBeaten", levelBeaten);

        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Restart() //Restart the scene
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Go to Main Menu Level
    /// </summary>
    public void GoToMainMenu(int levelBeaten)
    {
        //AudioManager.Instance?.PlayButtonSound();

        if (levelBeaten >= PlayerPrefs.GetInt("LevelBeaten"))
        {

            PlayerPrefs.SetInt("LevelBeaten", levelBeaten);

        }


        //CheckHighScore();

        //Initialize playerpoint to 0 from new game
        GameManager.Instance.PlayerPoints = 0;
        //go to menu level
        //SceneManager.LoadScene(GameConstants.MAINMENU_LEVEL);
    }

}
