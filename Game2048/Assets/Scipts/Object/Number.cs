using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    public Image image;
    public Text number_text;
    public MyGrid myGrid;

    public Color[] colors;

    private const int numberConfig = 2;
  
    private void Awake()
    {
        image = transform.GetComponent<Image>();
        number_text = transform.Find("Text").GetComponent<Text>();
    }

    public void init(MyGrid myGrid)
    {
        this.myGrid = myGrid;
        setValue(numberConfig);
      
    }
    public void setValue(int number)
    {
        if (number == 0)
        {
            this.number_text.text = "";
            return;
        }
        this.number_text.color = colors[(int)Mathf.Log(number, 2) - 1];
        this.number_text.text = number.ToString();
    }
    public Number doubleValue()
    {
        this.setValue(getValue() * 2);
        return this;
    }
    public int getValue()
    {
        if (number_text.text == "")
        {
            return 0;
        }
        return int.Parse(number_text.text);
    }
}
