using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MissionResult : MonoBehaviour
{
    public GameObject[] titles;

    public GameObject rewardButton;

    public GoogleAdMobManager GAdMobManager;
    
    public void Fail()
    {
        titles[0].SetActive(true);

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(3))
        {
            Debug.Log("ThisSceneISNo.3");

            if(GAdMobManager.rewardedCheck == 1)
            {
                rewardButton.GetComponent<Image>().color = new Color(77/255f, 77/255f, 77/255f);
                Debug.Log("rewardButtonColorChange");
            }
        }
        else
        {
            Debug.Log("ThisSceneIsNotNo.3");
            return;
        }
    }

    public void Success()
    {
        titles[1].SetActive(true);        
    }
}
