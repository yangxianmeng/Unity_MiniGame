using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    private Number number;   //当前数字
    private int x;           //当前x坐标
    private int y;           //当前y坐标
    private int xId;         //当前X轴方向的id;
    private int yId;         //当前Y轴方向的id;

    private float time;

    public bool isHaveNumber()
    {
        return number != null;
    }
    public void init()
    {
       number.init(this);
       time = 0;
       transform.localScale = Vector3.zero;
    }
    public void setNumber(Number number)
    {
        this.number = number;
    }

    public Number getNumber()
    {
        return number;
    }
    public void setId(int x, int y)
    {
        this.x = x;
        this.y = y;
        this.xId = x * 100 + y;
        this.yId = y * 100 + x;
    }
    public int getX()
    {
        return x;
    }
    public int getY()
    {
        return y;
    }
    public int getXId()
    {
        return xId;
    }
    public int getYId()
    {
        return yId;
    }
    //创建数字
    public Number createNumber(GameObject numberPrefab)
    {
        GameObject obj = GameObject.Instantiate(numberPrefab, this.transform);
        this.number = obj.GetComponent<Number>();
        return this.number;
    }
    public bool isHaveValue()
    {
        if (number == null)
        {
            return false;
        }
        return number.getValue() != 0;
    }
    public int getValue()
    {
        if (number == null)
        {
            return 0;
        }
        return number.getValue();
    }

    public void doubleValue()
    {
        time = 0;
        transform.localScale = Vector3.zero;
        this.setNumber(this.getNumber().doubleValue());
    }
    private void Update()
    {
        time += Time.deltaTime * 4;
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, time);
    }
}
