using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameOverPanel : ViewBase
{

    public Text titleText;
    public Button btn_aganPlay;
    public Button btn_return;
    public GamePanelIEvent gamePanelIEvent;

    public void againPlay()
    {
        Hide();
        gamePanelIEvent.restarGame();
        gamePanelIEvent.Show();
    }

    public void returnGame()
    {
        SceneManager.LoadScene(0);
    }

    public void init(bool gameOver)
    {
        if (gameOver)
        {
            this.titleText.text = "失败了！";
            this.titleText.color = new Color32(166, 0, 0, 255);
        }
        else
        {
            this.titleText.text = "胜利了！";
            this.titleText.color = new Color32(47, 166, 15, 255);
        }
    }

}
