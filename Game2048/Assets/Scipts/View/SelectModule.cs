using Assets.Scipts.Const;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectModule : ViewBase
{
    public MenuPanel menuPanel;

    public void OnSelectModelClick(int count)
    {
        PlayerPrefs.SetInt(GloablConst.MODULE_KEY, count);
        //跳转场景;
        AsyncOperation pro = SceneManager.LoadSceneAsync(1);
        Debug.Log("LoadScene Progress : = " +  pro.progress);
    }
   
    public override void Hide()
    {
        base.Show();
        menuPanel.Show();
    }
}
