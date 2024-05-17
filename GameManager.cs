using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if(null == _instance)
            {
                return null;
            }
            return _instance;
        }
    }

    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public PlayerCollision player;
    public PlayerMovement playerMove;

    public KeyManager keyManager;

    public GameObject menuPanel;
    public GameObject gamePanel;
    public GameObject gSpawner;
    public GameObject aSpawner;
    public GameObject oSpawner;

    public PlayableDirector timeline;
    public PlayableDirector openingTimeline;

    public TMP_Text UIPoint;
    public Image[] UIhealth;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public MissionResult missionResult;
    public GameObject storyOpening;
    public GameObject storyOpeningTimeline;

    public bool stopPoint = false;
    public bool isMissionSuccess = false;

    public GameObject thanksForSupportPanel;
    public GameObject alreadyPurchase;
    public GameObject supportPanel;

    public GameObject morningPhoto;
    public GameObject afternoonPhoto;
    public GameObject eveningPhoto;

    public GameObject purchaseCheckPanel;
    public GameObject[] purchaseCheckPanels;

    public GameObject quitCheckPanel;

    public GameObject afterAdWatchPanel;
    public GameObject adIsNotReadyImage;
    public GameObject gameOverImage;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }

        else if(_instance != null)
        {
            Destroy(this.gameObject);
        }

        Time.timeScale = 1;
        Application.targetFrameRate = 60;
    }

    public void OnSceneLoaded()
    {
        Debug.Log("PointCountStart");
        CountPointControl();
    }

    public void GameStart()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickButton);

        AudioManager.instance.StopBgm();

        keyManager.successOnce = 0;

        StartCoroutine("GameStartRoutine");
    }

    IEnumerator GameStartRoutine()
    {
        storyOpening.SetActive(true);

        storyOpeningTimeline.SetActive(true);

        storyOpeningTimeline.GetComponent<PlayableDirector>().enabled = true;

        yield return new WaitForSeconds(10f);

        SceneManager.LoadScene(1);
    }

    public void InfinityModeStart()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickButton);

        SceneManager.LoadScene(3);
    }

    public void MissionFail()
    {
        StartCoroutine("TimeStop");
        missionResult.gameObject.SetActive(true);
        gameOverImage.SetActive(true);
        missionResult.Fail();    
    }

    IEnumerator TimeStop()
    {
        while (Time.timeScale > 0.3)
        {
            Time.timeScale -= 0.1f;
            Debug.Log(Time.timeScale);
            yield return new WaitForSeconds(1f);
        }
    }

    public void CountPointControl()
    {
        if(stopPoint == false)
        {
            Debug.Log("PointCountStop");
            stopPoint = true;
        }
        else
        {
            stopPoint = false;
        }
    }

    public void MissionSuccess()
    {
        isMissionSuccess = true;

        player.StartCoroutine("ResetCoroutine");

        missionResult.gameObject.SetActive(true);
        missionResult.Success();
        timeline.Play();

        StartCoroutine("MissionSuccessRoutine");
    }

    IEnumerator MissionSuccessRoutine()
    {
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene(2);
    }


    public void GameRetry()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickButton);

        player.StartCoroutine("ResetCoroutine");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoMenu()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickButton);

        SceneManager.LoadScene(0);
    }

    public void QuitCheckPanelControl()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickButton);

        if(quitCheckPanel.activeSelf == false)
        {
            quitCheckPanel.SetActive(true);
        }
        else
        {
            quitCheckPanel.SetActive(false);
        }
    }

    public void GameQuit()
    {
        StartCoroutine("GameQuitCoroutine");
    }

    IEnumerator GameQuitCoroutine()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickButton);

        yield return new WaitForSeconds(0.5f);

        Application.Quit();
    }

    void Update()
    {
        UIPoint.text = string.Format("{0:n0}", stagePoint);

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            AudioManager.instance.PlayBgm(AudioManager.Bgm.StartMenu);
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            AudioManager.instance.PlayBgm(AudioManager.Bgm.StoryMode);
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            AudioManager.instance.PlayBgm(AudioManager.Bgm.StoryEnding);
        }

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            AudioManager.instance.PlayBgm(AudioManager.Bgm.InfinityMode);
        }
    }

    public void HealthDown()
    {
        if(health > 1)
        {
            health--;
            UIhealth[health].sprite = emptyHeart;
        }

        else
        {
            //All Health UI Off
            health--;
            UIhealth[0].sprite = emptyHeart;
        }
    }

    public void HealthUp()
    {
        if(health < 3)
        {
            health++;
            UIhealth[health-1].sprite = fullHeart;
        }
    }

    public void GetReward()
    {
        gameOverImage.SetActive(false);
        
        afterAdWatchPanel.SetActive(true);

        player.PlayerGetReward();
    }

    public void AfterWatchAd()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickButton);

        CountPointControl();

        player.PlayerWatchAd();

        HealthUp();
        HealthUp();
        HealthUp();

        player.StartCoroutine("GetHurt");

        StopCoroutine("TimeStop");

        Time.timeScale = 1;

        afterAdWatchPanel.SetActive(false);
        missionResult.gameObject.SetActive(false);
    }

    public void AdIsNotReadyYet()
    {
        if(adIsNotReadyImage.activeSelf == false)
        {
            adIsNotReadyImage.SetActive(true);
        }
        else
        {
            adIsNotReadyImage.SetActive(false);
        }
    }

    public void MorningPhotoButton()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickButton);

        morningPhoto.SetActive(true);
        afternoonPhoto.SetActive(false);
        eveningPhoto.SetActive(false);
    }

    public void AfternoonPhotoButton()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickButton);

        morningPhoto.SetActive(false);
        afternoonPhoto.SetActive(true);
        eveningPhoto.SetActive(false);
    }

    public void EveningPhotoButton()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickButton);

        morningPhoto.SetActive(false);
        afternoonPhoto.SetActive(false);
        eveningPhoto.SetActive(true);
    }

    public void SupportPanelButton()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickButton);

        supportPanel.SetActive(true);
    }

    public void ThanksForSupport()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickButton);

        thanksForSupportPanel.SetActive(true);
    }

    public void ExitSupportPanel()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickButton);
        
        if(thanksForSupportPanel.activeSelf == true)
        {
            thanksForSupportPanel.SetActive(false);
        }
        else if(alreadyPurchase.activeSelf == true)
        {
            alreadyPurchase.SetActive(false);
        }
        else
        {
            supportPanel.SetActive(false);
        }
    }

    public void MorningPurchaseCheckPanel()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickButton);

        purchaseCheckPanel.SetActive(true);
        purchaseCheckPanels[0].SetActive(true);
    }

    public void AfternoonPurchaseCheckPanel()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickButton);

        purchaseCheckPanel.SetActive(true);
        purchaseCheckPanels[1].SetActive(true);
    }

    public void EveningPurchaseCheckPanel()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickButton);

        purchaseCheckPanel.SetActive(true);
        purchaseCheckPanels[2].SetActive(true);
    }

    public void ExitPurchaseCheckPanel()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickButton);

        if(purchaseCheckPanels[0].activeSelf == true)
        {
            purchaseCheckPanels[0].SetActive(false);
            purchaseCheckPanel.SetActive(false);
        }
        else if(purchaseCheckPanels[1].activeSelf == true)
        {
            purchaseCheckPanels[1].SetActive(false);
            purchaseCheckPanel.SetActive(false);
        }
        else if(purchaseCheckPanels[2].activeSelf == true)
        {
            purchaseCheckPanels[2].SetActive(false);
            purchaseCheckPanel.SetActive(false);
        }
    }
}