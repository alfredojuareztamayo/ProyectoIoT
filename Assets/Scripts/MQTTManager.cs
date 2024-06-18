using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;
using System;

public class MQTTManager : MonoBehaviour
{
    private MqttClient client;
    public string brokerAddress = "broker.hivemq.com"; // Dirección del broker MQTT
    public int brokerPort = 1883; // Puerto del broker MQTT
    public string topic = "esp32sleek"; // Tema del MQTT
    AudioListener audioListener;
    bool playMusic;
    public int levelAvance = 0;
    //GameObject[] allItems;
    public int advance;

    void Start()
    {
        //GameObject[] allItems = GameObject.FindGameObjectsWithTag("advanceLevel");
         //advance = GameObject.FindGameObjectsWithTag("advanceLevel").Length - 3;
        // Inicializa el cliente MQTT
        client = new MqttClient(brokerAddress);

        // Eventos de conexión y recepción de mensajes
        client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
        client.ConnectionClosed += Client_ConnectionClosed;

        string clientId = Guid.NewGuid().ToString();
        try
        {
            // Conexión al broker
            client.Connect(clientId);
            Debug.Log("MQTT client connected successfully");

            // Suscribirse al tema
            client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

            // Enviar comandos
            SendCommand("play");  // Envía un comando para reproducir música
            SendCommand("light on");  // Envía un comando para encender la luz
        }
        catch (Exception e)
        {
            Debug.LogError("MQTT connection failed: " + e.Message);
        }
    }
    private void Update()
    {
        //CheckAdvance();
    }

    private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        var message = Encoding.UTF8.GetString(e.Message);
        Debug.Log("Received: " + message);
    }

    private void Client_ConnectionClosed(object sender, EventArgs e)
    {
        Debug.LogWarning("MQTT connection closed");
    }

    public void SendCommand(string cmd)
    {
        if (client.IsConnected)
        {
            client.Publish(topic, Encoding.UTF8.GetBytes(cmd), MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE, false);
            Debug.Log("Command sent: " + cmd);
        }
        else
        {
            Debug.LogWarning("MQTT client is not connected");
        }
    }

    void OnDestroy()
    {
        if (client.IsConnected)
        {
            client.Disconnect();
            Debug.Log("MQTT client disconnected");
        }
    }


    void CheckAdvance()
    {
        
        
        //Debug.Log(advance);
        if (levelAvance / advance == 1)
        {
            SendCommand("lvl2");
        }
        if (levelAvance/advance > 0.5)
        {
            SendCommand("lvl1");
        }


    }
}
