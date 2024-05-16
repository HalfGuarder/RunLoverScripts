using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public Animator animator;
    public Sprite playerDie;

    void Start()
    {
        StartCoroutine("ResetCoroutine");
        Debug.Log("OnStartPC");
    }

    void OnEnable()
    {
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Debug.Log("IgnoreLyaerCollisionFalse");
    }
    
    public void PlayerIsDie()
    {
        GameManager.Instance.CountPointControl();
        animator.SetBool("isDie", true);
        Physics2D.IgnoreLayerCollision(6, 7);
    }

    public void PlayerGetReward()
    {
        animator.SetBool("isDie", false);
    }

    public void PlayerWatchAd()
    {
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(GameManager.Instance.isMissionSuccess == false)
        {
            if(collision.transform.tag == "AirObstacleOne")
            {
                AudioManager.instance.PlaySfx(AudioManager.Sfx.CollisionAOO);

                GameManager.Instance.HealthDown();
                if(GameManager.Instance.health <= 0)
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Death);

                    PlayerIsDie();
                    StartCoroutine("ResetCoroutine");
                    GameManager.Instance.MissionFail();
                }
                else
                {
                    StartCoroutine("GetHurt");
                }
            }

            if (collision.transform.tag == "GroundObstacleOne")
            {
                AudioManager.instance.PlaySfx(AudioManager.Sfx.CollisionGOO);

                GameManager.Instance.HealthDown();
                if (GameManager.Instance.health <= 0)
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Death);

                    PlayerIsDie();
                    StartCoroutine("ResetCoroutine");
                    GameManager.Instance.MissionFail();
                }
                else
                {
                    StartCoroutine("GetHurt");
                }
            }

            if (collision.transform.tag == "GroundObstacleTwo")
            {
                AudioManager.instance.PlaySfx(AudioManager.Sfx.CollisionGOT);

                GameManager.Instance.HealthDown();
                if (GameManager.Instance.health <= 0)
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Death);

                    PlayerIsDie();
                    StartCoroutine("ResetCoroutine");
                    GameManager.Instance.MissionFail();
                }
                else
                {
                    StartCoroutine("GetHurt");
                }
            }

            if (collision.transform.tag == "Wall")
            {          
                GameManager.Instance.HealthDown();
                if(GameManager.Instance.health <= 0)
                {
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Death);

                    PlayerIsDie();
                    StartCoroutine("ResetCoroutine");
                    GameManager.Instance.MissionFail();
                }
                else
                {
                    StartCoroutine("GetHurt");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target")
        {
            GameManager.Instance.HealthUp();
        }
    }

    public IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(6, 7);
        GetComponent<Animator>().SetLayerWeight(1, 1);

        yield return new WaitForSeconds(3);

        Physics2D.IgnoreLayerCollision(6, 7, false);
        GetComponent<Animator>().SetLayerWeight(1, 0);

        yield break;
    }

    public IEnumerator ResetCoroutine()
    {
        yield return null;
        Debug.Log("ResetCoroutine");
    }
}