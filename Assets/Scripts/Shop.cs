using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop: MonoBehaviour
{
    // Start is called before the first frame update
    Text  moneyText;
    public int money = 100;
    public int flowerPrice = 10;
    void Start()
    {
        moneyText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "money: " + money.ToString();
    }

    public void BuyFlower()
    {
        Debug.Log("BuyFlower");
        money -= flowerPrice;
    }
}
