using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Shop: MonoBehaviour
{
    // Start is called before the first frame update
    Text  moneyText;
    Text  flowerText;
    public int money = 100;
    public int flowerPrice = 10;
    public Tilemap tilemap;
    public Tile flowerTile;
    bool buyMode = false;
    void Start()
    {
        moneyText = GameObject.Find("Money").GetComponent<Text>();
        flowerText = GameObject.Find("FlowerText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "money: " + money.ToString() + "p";
        flowerText.text = "Buy Flower (" + flowerPrice.ToString() + "p)";
        if(money < flowerPrice)
          flowerText.color = Color.red;
        if(buyMode == true && money >= flowerPrice)
        {
            if(Input.GetMouseButtonDown(0))
            {
                var tileGrid = tilemap.layoutGrid;
                var mousePos = Input.mousePosition;
                var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                worldPos.z = 0;
                var tilePos = tileGrid.WorldToCell(worldPos);
                Debug.Log("Changed Tile at mouse:" + mousePos.ToString() + ", world: " + worldPos.ToString() + "grid: " + tilePos.ToString());
                ChangeTileTexture(tilePos);
                money -= flowerPrice;
            }
        }
    }

    public void BuyFlower()
    {
        buyMode = true;
    }

    void ChangeTileTexture(Vector3Int tileCoord)
    {
        tilemap.SetTile(tileCoord, flowerTile);
    }


}
