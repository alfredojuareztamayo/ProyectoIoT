using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class items : MonoBehaviour
{
    public string mensajeMQTT;
    private MQTTManager mqttManager;
    public bool goodBad = true;
    //public Color newColor;


    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Cambiar el color del material a uno aleatorio
            Color randomColor = GetRandomColor();
            renderer.material.color = randomColor;
            Debug.Log("Color changed to: " + randomColor);
        }
        else
        {
            Debug.LogError("Renderer not found on the GameObject");
        }

        mqttManager = FindObjectOfType<MQTTManager>();
        if (mqttManager == null)
        {
            Debug.LogError("MQTTManager not found in the scene");
        }

        if(goodBad )
        {
            mqttManager.advance++;
        }
    }

     private void OnTriggerEnter(Collider other)
     {
         if (other.gameObject.CompareTag("Player"))
         {
            if (mqttManager != null)
            {
                mqttManager.SendCommand(mensajeMQTT);
                Debug.Log("Item picked up and command sent");
                if(goodBad)
                {
                    mqttManager.levelAvance++;
                }
            }
            gameObject.SetActive(false);
        }
     }
    private Color GetRandomColor()
    {
        // Generar un color aleatorio
        return new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }
}
