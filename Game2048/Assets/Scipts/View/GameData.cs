using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{
    //数据
    private int playerSorce;
    private int bestSorce;
    private Dictionary<int, int> values;

    public GameData(int playerSorce, int bestSorce, Dictionary<int, int> values)
    {
        this.playerSorce = playerSorce;
        this.bestSorce = bestSorce;
        this.values = values;
    }

    public int getPlayerSorce()
    {
        return this.playerSorce;
    }
    public int getBestSorce()
    {
        return this.bestSorce;
    }
    public Dictionary<int, int> getValues()
    {
        return this.values;
    }
}
