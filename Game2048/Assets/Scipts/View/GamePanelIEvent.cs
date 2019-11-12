using Assets.Scipts.Enum;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePanelIEvent : Game
{

    public GameOverPanel gameOverPanel;

    private Vector3 pointDowm, pointUp;
    public void OnPointDown()
    {
        //Debug.Log("OnPointDown" + Input.mousePosition);
        pointDowm = Input.mousePosition;
    }

    public void OnPointUp()
    {
        //  Debug.Log("OnPointUp" + Input.mousePosition);
        pointUp = Input.mousePosition;
        if (Vector3.Distance(pointDowm, pointUp) < 50)
        {
            return;
        }

        MoveType moveType = moveRes();
        //mergeGrid(moveType);
        merge(moveType);
        if (getPlayerSorce() == 2048)
        {
            gameOverPanel.init(false);
            Hide();
            gameOverPanel.Show();
            return;
        }
        if (!canRandomCreateNumber())
        {
            if (isGameOver())
            {
                gameOverPanel.init(true);
                Hide();
                gameOverPanel.Show();
                Debug.Log("Game Over!");
            }
           
        }
    }

    public MoveType moveRes()
    {
        float diffX = pointDowm.x - pointUp.x;
        float diffY = pointDowm.y - pointUp.y;
        float diffXY = Mathf.Abs(diffY / diffX);
        //  Debug.Log("diffX = " + diffX + ", diffY = " + diffY);
        if (diffXY >= 1.0)
        {
            if (diffY < 0)
            {
                return MoveType.Up;
            }
            else
            {
                return MoveType.Dowm;
            }
        }
        else
        {
            if (diffX < 0)
            {
                return MoveType.Right;
            }
            else
            {
                return MoveType.Lieft;
            }
        }

    }

    private void Awake()
    {
        init();
       // AudioManager._autionInstance.playeMusic(audioClip);
    }

    //重新开始
    public void restarGame()
    {
        SceneManager.LoadScene(1);
    }
    //退出
    public void returnGame()
    {
        SceneManager.LoadScene(0);
    }
    //回到上一步
    public void returnLatsStep()
    {
        if (step == 1)
        {
            Debug.Log("已经是最后一步了！");
            return;
        }
        step--;
        GameData historyGameData = history[step];
        if (historyGameData.getBestSorce() != getBestSorce())
        {
            setBestSorce(historyGameData.getBestSorce());
        }
        if (historyGameData.getPlayerSorce() != getPlayerSorce())
        {
            setPlayerSorce(historyGameData.getPlayerSorce());
        }
        Dictionary<int , int> values = historyGameData.getValues();
        for (int i = 0; i < moduleType; i++)
        {
            for (int j = 0; j < moduleType; j++)
            {
                if (values.ContainsKey(myGrids[i][j].getXId()))
                {
                    myGrids[i][j].getNumber().setValue(values[myGrids[i][j].getXId()]);
                }
                else
                {
                    myGrids[i][j].getNumber().setValue(0);
                }
            }
        }
    }
    
}
