using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static bool isGameEndSnorkel;
    public static bool isGameEndTube;

    public int successOnce = 0;

    public GameManager gameManager;

    private void Awake()
    {
        isGameEndSnorkel = false;
        isGameEndTube = false;

        Debug.Log("KeyManagerSceneLoaded");
    }

    void Update()
    {
        MissionSuccessCheck();
    }

    void MissionSuccessCheck()
    {
        if(successOnce == 1)
        {
            return;
        }

        if(isGameEndSnorkel)
        {
            if(isGameEndTube)
            {
                successOnce += 1;

                if(successOnce == 1)
                {
                    gameManager.MissionSuccess();
                }
            }
        }  
    }
}
