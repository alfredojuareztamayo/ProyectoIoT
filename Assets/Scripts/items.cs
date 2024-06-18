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
    private ManagerWin ManagerWin;
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
        ManagerWin = FindObjectOfType<ManagerWin>();
        if (ManagerWin == null)
        {
            Debug.LogError("ManagerWin not found in the scene");
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
                
            }
            if(ManagerWin != null && goodBad)
            {
                ManagerWin.RestValueOfItems();
            }
            if(!goodBad)
            {
                ManagerWin.RestLifePlayer();
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
