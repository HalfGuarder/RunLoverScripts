using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeySnorkelManager : MonoBehaviour
{
    public static int keySnorkel = 1;

    public Image[] keySnorkels;
    public Sprite getSnorkel;
    public Sprite emptySnorkel;

    void Awake()
    {
        keySnorkel = 0;
    }
    
    void Update()
    {
        foreach (Image img in keySnorkels)
        {
            img.sprite = emptySnorkel;
        }
        for (int i = 0; i < keySnorkel; i++)
        {
            keySnorkels[i].sprite = getSnorkel;
        }
    }
}
