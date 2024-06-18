using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerWin : MonoBehaviour
{
    private MQTTManager mqttManager;
    private StackMaze stackMaze;
    public CanvasManager conditionWinGameOver;
    private int numberOfItems;
    private int numberFinalOfItems;
    private int lifePlayer = 3;
    bool didUWin;

    // Start is called before the first frame update
    void Start()
    {
        mqttManager = FindObjectOfType<MQTTManager>();
        if (mqttManager == null)
        {
            Debug.LogError("MQTTManager not found in the scene");
        }
        stackMaze = FindObjectOfType<StackMaze>();
        if(stackMaze == null)
        {
            Debug.LogError("StackMaze not found in the scene");
        }

        Invoke("GetNumberOfItems", 0.5f);
        InvokeRepeating("CheckWin", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void GetNumberOfItems()
    {
        numberOfItems = stackMaze.itemsPrefabs.Length;
        foreach(var item in stackMaze.itemsPrefabs)
        {
            if (item.GetComponent<items>().goodBad == false)
            {
                numberOfItems--;
            }
        }
        numberFinalOfItems = numberOfItems;
    }

    public void RestValueOfItems()
    {
        numberOfItems--;
    }

    public void RestLifePlayer()
    {
        lifePlayer--;
    }
    public void CheckWin()
    {
        //Debug.Log("numero de items: "+ numberOfItems+ " Numero de items para ganar: " +  numberFinalOfItems);
        if(numberOfItems == 0)
        {
            mqttManager.SendCommand("lvl2");
            didUWin = false;
            conditionWinGameOver.WinPanelCanvas();
            CancelInvoke();
        }
        if(didUWin && (numberOfItems / numberFinalOfItems) <0.6)
        {
            mqttManager.SendCommand("lvl1");
        }
        if (lifePlayer == 0)
        {
            conditionWinGameOver.GameOverCanvas();
        }
    }
}
