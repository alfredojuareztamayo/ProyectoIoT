using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;
using System;

public class MQTTManager : MonoBehaviour
{
    private MqttClient client; // MQTT client
 public string brokerAddress = "broker.hivemq.com"; // MQTT broker address
 public int brokerPort = 1883; // MQTT broker port
 public string topic = "esp32sleek"; // MQTT topic
 AudioListener audioListener; // Audio listener
 bool playMusic; // Flag to play music
 public int levelAvance = 0; // Level progress
 //GameObject[] allItems; // All game objects
 public int advance; // Advance

 void Start()
 {
     // Initialize all objects with the tag "advanceLevel"
     // GameObject[] allItems = GameObject.FindGameObjectsWithTag("advanceLevel");
     // advance = GameObject.FindGameObjectsWithTag("advanceLevel").Length - 3;

     // Initialize the MQTT client
     client = new MqttClient(brokerAddress);

     // Connection and message reception events
     client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
     client.ConnectionClosed += Client_ConnectionClosed;

     string clientId = Guid.NewGuid().ToString();
     try
     {
         // Connect to the broker
         client.Connect(clientId);
         Debug.Log("MQTT client connected successfully");

         // Subscribe to the topic
         client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });

         // Send commands
         SendCommand("play");  // Send a command to play music
         SendCommand("light on");  // Send a command to turn on the light
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

 // Event handler for receiving MQTT messages
 private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
 {
     var message = Encoding.UTF8.GetString(e.Message);
     Debug.Log("Received: " + message);
 }

 // Event handler for MQTT connection closed
 private void Client_ConnectionClosed(object sender, EventArgs e)
 {
     Debug.LogWarning("MQTT connection closed");
 }

 // Method to send a command via MQTT
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
     // Disconnect the MQTT client when the object is destroyed
     if (client.IsConnected)
     {
         client.Disconnect();
         Debug.Log("MQTT client disconnected");
     }
 }

 // Method to check the advance level and send appropriate commands
 void CheckAdvance()
 {
     // Debug.Log(advance);
     if (levelAvance / advance == 1)
     {
         SendCommand("lvl2");
     }
     if (levelAvance / advance > 0.5)
     {
         SendCommand("lvl1");
     }
 }
}
