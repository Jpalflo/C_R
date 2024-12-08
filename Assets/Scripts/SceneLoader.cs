using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("OutdoorsScene");
    }

    public void ExitScene()
    {
        //AudioManager.Instance?.PlayButtonSound();

        #if UNITY_EDITOR
            // Si estamos en el editor, det�n el juego
            UnityEditor.EditorApplication.isPlaying = false;
        #else
        // Si es una compilaci�n, cierra la aplicaci�n
        Application.Quit();
        #endif
    }
}