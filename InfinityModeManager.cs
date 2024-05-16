using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using TMPro;

public class InfinityModeManager : MonoBehaviour
{
    public GameManager gameManager;

    void Awake()
    {
        StartCoroutine(InfinityTimeScale());
    }

    private IEnumerator InfinityTimeScale()
    {
        while (Time.timeScale < 5)
        {
            Time.timeScale += 0.1f;
            Debug.Log(Time.timeScale);
            yield return new WaitForSeconds(10f);
        }
    }

    public void StopTimeScale()
    {
        if (gameManager.health <= 0)
        {
            StopCoroutine(InfinityTimeScale());
        }
    }
}
