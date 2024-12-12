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
    private int cuentaAtras;

    public PlayerMovement _playerMovement;

    //Components
    [Header("HUD text")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI pointsText;
    [SerializeField] private TextMeshProUGUI cuentaAtrasText;

    private void Start()
    {

    }
    private void Update()
    {
        seconds = (int)LevelManager.Instance.InternalLevelTime % 60;
        minutes = (int)LevelManager.Instance.InternalLevelTime / 60;

        if ((int)LevelManager.Instance.cuentaAtras % 60 > 0)
        {
            cuentaAtras = (int)LevelManager.Instance.cuentaAtras % 60;
        }
        else
        {
            cuentaAtrasText.enabled = false;
        }
    }

    private void OnGUI()
    {
        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00"); //Format numbers to be, for example, 01 instead of 1
        pointsText.text = _playerMovement.puntos.ToString();
        cuentaAtrasText.text = cuentaAtras.ToString("00");
    }

}
