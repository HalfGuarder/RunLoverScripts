using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class EndingSceneManager : MonoBehaviour
{
public PlayableDirector timeline;

public PlayerCollision player;

    public void EndingSceneStop()
    {
        player.StartCoroutine("ResetCoroutine");

        timeline.Pause();

        StartCoroutine("ReturnStartMenuRoutine");
    }

    IEnumerator ReturnStartMenuRoutine()
    {       
        yield return new WaitForSeconds(24.7f);

        SceneManager.LoadScene(0);
    }

    public void ReturnButton()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ClickButton);

        StartCoroutine("WaitButtonSoundRoutine");
    }

    IEnumerator WaitButtonSoundRoutine()
    {
        yield return new WaitForSeconds(0.35f);

        SceneManager.LoadScene(0);
    }

}
