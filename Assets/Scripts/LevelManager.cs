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

    public PlayerMovement _playerMovement;

    [SerializeField] private List<GameObject> enemiesList = new List<GameObject>();

    public float cuentaAtras;

    private void Awake()
    {
        internalLevelTime = (float)levelTime;

        Instance = this;

        //Disable the panel
        endPanel.SetActive(false);

        //restart timeScale
        Time.timeScale = 1;
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemiesList.Add(item);
        }
    }

    private void Start()
    {
        //totalLevelPowerUps = GameObject.FindGameObjectsWithTag("PowerUp").Length;
        //Debug.Log(totalLevelPowerUps);

        //RemainingPlayerPowerUps = totalLevelPowerUps;
        StartCoroutine(ActivarPlayer());
    }

    private void Update()
    {
        cuentaAtras -= Time.deltaTime;
        if (internalLevelTime <= 0f)
        {
            GameOver();
        }
        if (_playerMovement.puntos >1200)
        {
            GameWin();
        }

       
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
        Cursor.lockState = CursorLockMode.None;

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

    IEnumerator ActivarPlayer()
    {
        yield return new WaitForSeconds(cuentaAtras);
        _playerMovement.enabled = true;
    }
}
