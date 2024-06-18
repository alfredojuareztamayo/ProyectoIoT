using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject winPanel, gameOverPanel;
    // Start is called before the first frame update
    private MQTTManager mqttManager;
    void Start()
    {
        mqttManager = FindObjectOfType<MQTTManager>();
        if (mqttManager == null)
        {
            Debug.LogError("MQTTManager not found in the scene");
        }
    }

  
    public void WinPanelCanvas()
    {
        winPanel.SetActive(true);
        mqttManager.SendCommand("win");
        Cursor.visible = true;
    }
    public void GameOverCanvas()
    {
        gameOverPanel.SetActive(true);
        mqttManager.SendCommand("gameover");
        Cursor.visible = true;
    }
    public void BackAplication()
    {
        Application.Quit();
    }
}
