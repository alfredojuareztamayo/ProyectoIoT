using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLedOff : MQTTManager
{
    bool onOffLed = true;
    bool musicOn = true;
    int lvl = 1;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (onOffLed)
            {
                SendCommand("Fon");
                onOffLed = false;
            }
            else
            {
                SendCommand("Foff");
                onOffLed = true;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (musicOn)
            {
                SendCommand("M1");
                musicOn = false;
            }
            else
            {
                SendCommand("M1n");
                musicOn = true;
            }

        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (musicOn)
            {
                SendCommand("M2");
                musicOn = false;
            }
            else
            {
                SendCommand("M1n");
                musicOn = true;
            }

        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (musicOn)
            {
                SendCommand("M3");
                musicOn = false;
            }
            else
            {
                SendCommand("M1n");
                musicOn = true;
            }

        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (musicOn)
            {
                SendCommand("M4");
                musicOn = false;
            }
            else
            {
                SendCommand("M1n");
                musicOn = true;
            }

        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SendCommand("damage");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            
            if (lvl == 2)
            {
                SendCommand("lvl2");
                lvl++;
            }
            if (lvl == 1)
            {
                SendCommand("lvl1");
                lvl++;
            }
        }

    }
}
