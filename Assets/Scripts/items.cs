using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class items : MonoBehaviour
{
   public string mensajeMQTT; // MQTT message
private MQTTManager mqttManager; // Reference to MQTTManager
public bool goodBad = true; // Flag to determine good or bad item
private ManagerWin ManagerWin; // Reference to ManagerWin

private void Start()
{
    // Get the Renderer component and change the material color to a random color
    Renderer renderer = GetComponent<Renderer>();
    if (renderer != null)
    {
        Color randomColor = GetRandomColor();
        renderer.material.color = randomColor;
        Debug.Log("Color changed to: " + randomColor);
    }
    else
    {
        Debug.LogError("Renderer not found on the GameObject");
    }

    // Find MQTTManager in the scene
    mqttManager = FindObjectOfType<MQTTManager>();
    if (mqttManager == null)
    {
        Debug.LogError("MQTTManager not found in the scene");
    }

    // Find ManagerWin in the scene
    ManagerWin = FindObjectOfType<ManagerWin>();
    if (ManagerWin == null)
    {
        Debug.LogError("ManagerWin not found in the scene");
    }
}

private void OnTriggerEnter(Collider other)
{
    // Check if the colliding object has the "Player" tag
    if (other.gameObject.CompareTag("Player"))
    {
        if (mqttManager != null)
        {
            // Send MQTT command
            mqttManager.SendCommand(mensajeMQTT);
            Debug.Log("Item picked up and command sent");
        }

        if (ManagerWin != null && goodBad)
        {
            // Reduce the value of items if it's a good item
            ManagerWin.RestValueOfItems();
        }

        if (!goodBad)
        {
            // Reduce player's life if it's a bad item
            ManagerWin.RestLifePlayer();
        }

        // Deactivate the game object
        gameObject.SetActive(false);
    }
}

private Color GetRandomColor()
{
    // Generate a random color
    return new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
}
}
