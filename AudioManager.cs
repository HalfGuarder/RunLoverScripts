using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip[] bgmClips;
    public float bgmVolume;
    public int bgmChannels;
    public AudioSource[] bgmPlayers;
    int bgmChannelIndex;

    public enum Bgm { StartMenu, StoryStart, StoryMode, StoryEnding, InfinityMode };

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int sfxChannels;
    AudioSource[] sfxPlayers;
    int sfxChannelIndex;

    public enum Sfx { ClickButton, Jump, Death, GetTarget, CollisionAOO, CollisionGOO=6, CollisionGOT=8, GetOrange=10 };

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayers = new AudioSource[bgmChannels];

        for (int index=0; index < bgmPlayers.Length; index++){
            bgmPlayers[index] = bgmObject.AddComponent<AudioSource>();
            bgmPlayers[index].playOnAwake = false;
            bgmPlayers[index].volume = bgmVolume;
        }

        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[sfxChannels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }
    }

    public void PlayBgm(Bgm bgm)
    {
        for (int index = 0; index < bgmPlayers.Length; index++)
        {
            int loopIndex = (index + bgmChannelIndex) % bgmPlayers.Length;

            if (bgmPlayers[loopIndex].isPlaying)
                continue;

            bgmChannelIndex = loopIndex;
            bgmPlayers[loopIndex].clip = bgmClips[(int)bgm];
            bgmPlayers[loopIndex].Play();
            break;
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int index=0; index < sfxPlayers.Length; index++) {
            int loopIndex = (index + sfxChannelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            int ranIndex = 0;

            if (sfx == Sfx.CollisionAOO || sfx == Sfx.CollisionGOO || sfx == Sfx.CollisionGOT) {
                ranIndex = Random.Range(0, 2);
            }

            if (sfx == Sfx.GetOrange){
                ranIndex = Random.Range(0, 3);
            }

            sfxChannelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    public void StopBgm()
    {
        for (int index = 0; index < bgmPlayers.Length; index++)
        {
            bgmPlayers[index].volume = 0f;
        }

    }

}
