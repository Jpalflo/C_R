using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private Coroutine coroutine;

    public void LoadScene(int sceneIndex) //Change the scene game
    {

        if (coroutine == null)
        {
            coroutine = StartCoroutine(LoadSceneWithTime(sceneIndex));
        }
        //AudioManager.Instance?.PlayButtonSound();
        SceneManager.LoadScene(sceneIndex);

    }

    IEnumerator LoadSceneWithTime(int sceneIndex) //Change scene after 0.25 seconds :)
    {

        yield return new WaitForSeconds(0.25f);
        coroutine = null;
        SceneManager.LoadScene(sceneIndex);
    }

    public void ExitScene() //Quit the game
    {
        //AudioManager.Instance?.PlayButtonSound();

        #if UNITY_EDITOR
            // Si estamos en el editor, detén el juego
            UnityEditor.EditorApplication.isPlaying = false;
        #else
        // If is a compilation, close the app
        Application.Quit();
        #endif
    }

    public void Restart() //Restart the scene
    {
        //AudioManager.Instance?.PlayButtonSound();

        SceneManager.LoadScene("OutdoorsScene");
    }

    public void OptionScene() //You can go to Option Scene, where could you change the volume with sliders
    {
        //AudioManager.Instance?.PlayButtonSound();

        SceneManager.LoadScene("OptionScene");
    }

    public void MenuScene()
    {
        //AudioManager.Instance?.PlayButtonSound();

        //GameManager.Instance.PlayerPoints = 0;
        //go to menu level
        SceneManager.LoadScene("MainMenu");
    }

    
}