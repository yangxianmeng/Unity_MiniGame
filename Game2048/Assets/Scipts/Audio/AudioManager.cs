using Assets.Scipts.Const;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _autionInstance;

    private AudioSource bg, sound;


    private void Awake()
    {
        _autionInstance = this;
        bg = transform.Find("Bg").GetComponent<AudioSource>();
        sound = transform.Find("sound").GetComponent<AudioSource>();

        //获取声音的大小
        bg.volume = PlayerPrefs.GetFloat(GloablConst.MUSIC_KEY, 0.5f);
        sound.volume = PlayerPrefs.GetFloat(GloablConst.SOULD_KEY, 0.5f);
    }
    public void playeMusic(AudioClip audioClip)
    {
        bg.loop = true;
        bg.Play();
    }
    public void playSound(AudioClip audioClip)
    {
        sound.PlayOneShot(audioClip);
    }



    public void setMusic(float v)
    {
        bg.volume = v;
    }
    public void setSound(float v)
    {
        sound.volume = v;
    }
}
