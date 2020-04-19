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
    Text  flowerNumText;
    public int money = 100;
    public int flowerPrice = 10;
    public Tilemap tilemap;
    public Tile flowerTile;
    bool buyMode = false;


    public List<Vector3> flowerPos = new List<Vector3>();

    void FindFlowers()
    {
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            if (tilemap.GetTile(localPlace) == flowerTile)
            {
                flowerPos.Add(tilemap.CellToWorld(localPlace));
            }
        }
    }
    
    void Start()
    {
        moneyText = GameObject.Find("Money").GetComponent<Text>();
        flowerText = GameObject.Find("FlowerText").GetComponent<Text>();
        flowerNumText = GameObject.Find("FlowerNumText").GetComponent<Text>();
        FindFlowers();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "money: " + money.ToString() + "p";
        flowerText.text = "Buy Flower (" + flowerPrice.ToString() + "p)";
        flowerNumText.text = "Your garden contains " + flowerPos.Count + " flowers";
        if(money < flowerPrice)
          flowerText.color = Color.red;
        if(buyMode == true && money >= flowerPrice)
        {
            if(Input.GetMouseButtonDown(0))
            {
                var mousePos = Input.mousePosition;
                var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                worldPos.z = 0;
                var tilePos = tilemap.WorldToCell(worldPos);
                Debug.Log("Changed Tile at mouse:" + mousePos.ToString() + ", world: " + worldPos.ToString() + "grid: " + tilePos.ToString());
                if(ChangeTileTexture(tilePos))
                {
                    flowerPos.Add(tilemap.CellToWorld(tilePos));
                    money -= flowerPrice;
                }
            }
        }
    }

    public void RacketedKid()
    {
        money += 40;
    }

    public void BuyFlower()
    {
        buyMode = true;
    }

    bool ChangeTileTexture(Vector3Int tileCoord)
    {
        if (tilemap.GetTile(tileCoord) == flowerTile)
          return false;

        tilemap.SetTile(tileCoord, flowerTile);
        return true;
    }
}
