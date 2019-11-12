using Assets.Scipts.Const;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPanel : ViewBase
{

    public Slider slider_sould;
    public Slider slider_music;

    public void OnBtnCloseClick()
    {
        Hide();
    }

    public void OnBtnSouldValueChange()
    {
        PlayerPrefs.SetFloat(GloablConst.SOULD_KEY, slider_sould.value);
        AudioManager._autionInstance.setSound(slider_sould.value);
    }
    public void OnBtnMusicValueChange()
    {
        PlayerPrefs.SetFloat(GloablConst.MUSIC_KEY, slider_music.value);
        AudioManager._autionInstance.setMusic(slider_music.value);
    }

    public void OnBtnMusicValueChange(float f)
    {
        PlayerPrefs.SetFloat(GloablConst.MUSIC_KEY, f);
    }

    public override void Show()
    {
        base.Show();
        //初始化设置面板
        slider_sould.value = PlayerPrefs.GetFloat(GloablConst.SOULD_KEY);
        slider_music.value = PlayerPrefs.GetFloat(GloablConst.MUSIC_KEY);
    }
}
