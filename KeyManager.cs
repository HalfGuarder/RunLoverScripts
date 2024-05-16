using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static bool isGameEndSnorkel;
    public static bool isGameEndTube;

    public int SuccessOnce = 0;

    public GameManager gameManager;

    private void Awake()
    {
        isGameEndSnorkel = false;
        isGameEndTube = false;

        Debug.Log("KeyManagerSceneLoaded");
    }

    void Update()
    {
        if(isGameEndSnorkel)
        {
            if(isGameEndTube)
            {
                if(SuccessOnce > 1)
                {
                    return;
                }

                SuccessOnce += 1;

                if(SuccessOnce == 1)
                {
                    gameManager.MissionSuccess();
                }
            }
        }  
    }
}
