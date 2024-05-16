using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyTubeManager : MonoBehaviour
{
    public static int keyTube = 1;

    public Image[] keyTubes;
    public Sprite getTube;
    public Sprite emptyTube;

    void Awake()
    {
        keyTube = 0;
    }
    
    void Update()
    {
        foreach (Image img in keyTubes)
        {
            img.sprite = emptyTube;
        }
        for (int i = 0; i < keyTube; i++)
        {
            keyTubes[i].sprite = getTube;
        }
    }
}
