using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : ViewBase
{

    public SelectModule selectModule;

    public SetPanel setPanel;

    public AudioClip audioClip;

    public void OnStartGameClick()
    {
        gameObject.SetActive(false);
        selectModule.Show();

    }
    public void OnSetGameClick()
    {
        setPanel.Show();
    }

    public void OnExitGameClick()
    {
        Application.Quit();
    }

    private void Awake()
    {
        //AudioManager._autionInstance.playeMusic(audioClip);
    }
}
