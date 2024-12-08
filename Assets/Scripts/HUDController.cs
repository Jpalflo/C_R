using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    //Level timer
    private float timer = 0;
    private int minutes = 0;
    private int seconds = 0;


    //Components
    [Header("HUD text")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI pointsText;

    private void Start()
    {

    }
    private void Update()
    {
        LevelManager.Instance.InternalLevelTime -= Time.deltaTime;
        seconds = (int)LevelManager.Instance.InternalLevelTime % 60;
        minutes = (int)LevelManager.Instance.InternalLevelTime / 60;
    }

    private void OnGUI()
    {
        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00"); //Format numbers to be, for example, 01 instead of 1
        pointsText.text = GameManager.Instance.PlayerPoints.ToString("00");
    }

}
