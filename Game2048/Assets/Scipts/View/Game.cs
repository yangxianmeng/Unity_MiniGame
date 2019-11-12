using Assets.Scipts.Const;
using Assets.Scipts.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : ViewBase
{

    public int moduleType;         //当前模式

    public Text bestSource;         //最高分
    public Text playerSource;       //当前分

    public Button btn_last;         //上一步
    public Button btn_reStart;      //重新开始
    public Button btn_exit;         //退出

    public Transform desk;          //桌面
    public GameObject gridPrefab;  //单元格预制体
    public GameObject numberPrefab;//数字预制体

    public MyGrid[][] myGrids;              //当前创建的所有的单元格
    private int sorce = 0;

    public Dictionary<int, GameData> history = new Dictionary<int, GameData>();
    public int step;

    public AudioClip audioClip;
    public AudioClip audioSound;
    //初始化数据
    public void init()
    {
        bestSource.text = PlayerPrefs.GetInt(GloablConst.BEST_SORCE, 0).ToString();
        moduleType = PlayerPrefs.GetInt(GloablConst.MODULE_KEY, 4);//默认4*4
        initGrid();
        canRandomCreateNumber();
        record();
    }

    //初始化格子
    public void initGrid()
    {
        GridLayoutGroup gridLayoutGroup = desk.GetComponent<GridLayoutGroup>();
        gridLayoutGroup.constraintCount = moduleType;
        int gridSize = getGridSize(moduleType);
        gridLayoutGroup.cellSize = new Vector2(gridSize, gridSize);
        myGrids = new MyGrid[moduleType][];
        for (int i = 0; i < moduleType; i++)
        {
            for (int j = 0; j < moduleType; j++)
            {
                if (myGrids[i] == null)
                {
                    myGrids[i] = new MyGrid[moduleType];
                }
                myGrids[i][j] = createGrid();
                myGrids[i][j].setId(i, j);
            }
        }
    }
    //计算下单元格的大小
    private int getGridSize(int moduleType)
    {
        RectTransform rectTransform = desk.GetComponent<RectTransform>();
        float width = rectTransform.sizeDelta.x;
        if (width > rectTransform.sizeDelta.y)
        {
            width = rectTransform.sizeDelta.y;
        }
        return (int)(width / moduleType - 2);
    }
    //创建单元格
    private MyGrid createGrid()
    {
        GameObject obj = GameObject.Instantiate(gridPrefab, desk);
        MyGrid myGrid = obj.GetComponent<MyGrid>();
        myGrid.createNumber(numberPrefab);
        return obj.GetComponent<MyGrid>();
    }
    //是否可以随机一个格子创建数字
    public bool canRandomCreateNumber()
    {
        List<MyGrid> noNumberGrids = new List<MyGrid>();
        for (int i = 0; i < moduleType; i++)
        {
            for (int j = 0; j < moduleType; j++)
            {
                if (myGrids[i][j].getValue() == 0)
                {
                    noNumberGrids.Add(myGrids[i][j]);
                }
            }
        }
        if (noNumberGrids.Count == 0)
        {
            return false;
        }
        int index = UnityEngine.Random.Range(0, noNumberGrids.Count);
        noNumberGrids[index].init();
        return true;
    }

    //把一个格子上的数字 移动到指定的空格子上
    public void moveToEmptyGrid(MyGrid currentGrid, MyGrid targetEmptyGrid)
    {
        targetEmptyGrid.getNumber().setValue(currentGrid.getNumber().getValue());
        currentGrid.getNumber().setValue(0);
    }
    //合并两个NumberValue相同的格子
    private void mergeGrid(MyGrid currentGrid, MyGrid targetGrid)
    {
        targetGrid.doubleValue();
        currentGrid.getNumber().setValue(0);
        currentGrid.setNumber(currentGrid.getNumber());
        updateSorce(targetGrid.getValue());
    }
    private void updateSorce(int number)
    {
        if (sorce < number)
        {
            setPlayerSorce(number);
        }
        if (number > PlayerPrefs.GetInt(GloablConst.BEST_SORCE))
        {
            setBestSorce(number);
        }
    }
    public void merge(MoveType moveType)
    {
        for (int i = 0; i < moduleType; i++)
        {
            switch (moveType)
            {
                case MoveType.Up:
                    _mergeUp(i, false);
                    _mergeUp(i, true);
                    break;
                case MoveType.Dowm:
                    _mergeDown(i, false);
                    _mergeDown(i, true);
                    break;
                case MoveType.Right:
                    _mergeRight(i, false);
                    _mergeRight(i, true);
                    break;
                case MoveType.Lieft:
                    _mergeLieft(i, false);
                    _mergeLieft(i, true);
                    break;
            }
        }
        record();
        AudioManager._autionInstance.playSound(audioSound);
    }
    private void _mergeRight(int x, bool isMove)
    {
        for (int y = 0; y < moduleType; y++)
        {
            MyGrid currentGrid = myGrids[x][y];
            if (currentGrid.isHaveValue())
            {
                int m = y;
                while (m < moduleType - 1 )
                {
                    m++;
                    MyGrid targetGrid = myGrids[x][m];
                    if (myGrids[x][m].isHaveValue() && !isMove)
                    {
                        if (currentGrid.getValue() == targetGrid.getValue())
                        {
                            mergeGrid(currentGrid, targetGrid);
                            y++;
                        }
                        break;
                    }
                    if (!myGrids[x][m].isHaveValue() && isMove)
                    {
                        moveToEmptyGrid(currentGrid, targetGrid);
                    }
                }
            }
        }
    }
    private void _mergeLieft(int x, bool isMove)
    {
        for (int y = moduleType - 1; y >= 0; y--)
        {
            MyGrid currentGrid = myGrids[x][y];
            if (currentGrid.isHaveValue())
            {
                int m = y;
                while (m > 0)
                {
                    m--;
                    MyGrid targetGrid = myGrids[x][m];
                    if (myGrids[x][m].isHaveValue() && !isMove)
                    {
                        if (currentGrid.getValue() == targetGrid.getValue())
                        {
                            mergeGrid(currentGrid, targetGrid);
                            y--;
                        }
                        break;
                    }
                    if (!myGrids[x][m].isHaveValue() && isMove)
                    {
                        moveToEmptyGrid(currentGrid, targetGrid);
                    }
                }
            }
        }
    }
    private void _mergeUp(int y, bool isMove)
    {
        for (int x = moduleType - 1; x >= 0; x--)
        {
            MyGrid currentGrid = myGrids[x][y];
            if (currentGrid.isHaveValue())
            {
                int m = x;
                while (m > 0)
                {
                    m--;
                    MyGrid targetGrid = myGrids[m][y];
                    if (targetGrid.isHaveValue() && !isMove)
                    {
                        if (currentGrid.getValue() == targetGrid.getValue())
                        {
                            mergeGrid(currentGrid, targetGrid);
                            x--;
                        }
                        break;
                    }
                    if (!targetGrid.isHaveValue() && isMove)
                    {
                        moveToEmptyGrid(currentGrid, targetGrid);
                    }
                }
            }
        }
    }
    private void _mergeDown(int y, bool isMove)
    {
        for (int x = 0; x < moduleType; x++)
        {
            MyGrid currentGrid = myGrids[x][y];
            if (currentGrid.isHaveValue())
            {
                int m = x;
                while (m < moduleType - 1)
                {
                    m++;
                    MyGrid targetGrid = myGrids[m][y];
                    if (targetGrid.isHaveValue() && !isMove)
                    {
                        if (currentGrid.getValue() == targetGrid.getValue())
                        {
                            mergeGrid(currentGrid, targetGrid);
                            x++;
                        }
                        break;
                    }
                    if (!targetGrid.isHaveValue() && isMove)
                    {
                        moveToEmptyGrid(currentGrid, targetGrid);
                    }
                }
            }
        }
    }

    public bool isGameOver()
    {
        for (int i = 0; i < moduleType - 1; i+=2)
        {
            for (int j = 0; j < moduleType - 1; j+=2)
            {
                //下
                if (myGrids[i][j].getValue() == myGrids[i + 1][j].getValue())
                {
                    return false;
                }
                //右
                if (myGrids[i][j].getValue() == myGrids[i][j + 1].getValue())
                {
                    return false;
                }
            }
        }
        return true;
    }
    public void record()
    {
        step++;
        Dictionary<int, int> mValues = new Dictionary<int, int>();
        for (int i = 0; i < moduleType; i++)
        {
            for (int j = 0; j < moduleType; j++)
            {
                if(myGrids[i][j].getValue() != 0)
                mValues.Add(myGrids[i][j].getXId(), myGrids[i][j].getValue());
            }
        }
        if (history.ContainsKey(step))
        {
            history.Remove(step);
        }
        history.Add(step, new GameData(getPlayerSorce(), getBestSorce(), mValues));
    }

    public int getPlayerSorce()
    {
        return int.Parse(playerSource.text);
    }

    public void setPlayerSorce(int number)
    {
        sorce = number;
        playerSource.text = sorce.ToString();
    }

    public int getBestSorce()
    {
        return int.Parse(bestSource.text);
    }

    public void setBestSorce(int number)
    {
        PlayerPrefs.SetInt(GloablConst.BEST_SORCE, number);
        bestSource.text = number.ToString();
    }
   
}
