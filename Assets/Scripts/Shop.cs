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
    public GameObject killZonePrefab = null;
    public List<AudioSource> audioSrcShot = null;
    public List<AudioSource> audioSrcDeath = null;
    public AudioClip deathClip = null;
    public List<AudioClip> shotClip = null;
    public AudioClip gardenClip = null;
    public PlayerAnnouncement announcement;
    bool win = false;

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

    public int GetNumFlowers()
    {
        return flowerPos.Count;
    }

    // Update is called once per frame
    void Update()
    {
        int numFlowers = flowerPos.Count;
        if (numFlowers >= 100 && win == false)
        {
            win = true;
            announcement.ShowFor("You Win !", "success", 5f);
        }
        moneyText.text = "Money: " + money.ToString() + "p / 150p";
        flowerText.text = "Place Flower (" + flowerPrice.ToString() + "p)";
        flowerNumText.text = "Your garden contains " + numFlowers + " flowers";
        if(money < flowerPrice)
          flowerText.color = Color.red;
        else
          flowerText.color = Color.black;

        if (Input.GetMouseButtonDown(1))
        {
            buyMode = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            buyMode = true;
        }

        if(buyMode == true)
        {
            if(money >= flowerPrice)
            {
                if (Input.GetMouseButton(0))
                {
                    var mousePos = Input.mousePosition;
                    var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                    worldPos.z = 0;
                    var cellPos = tilemap.WorldToCell(worldPos);
                    if (ChangeTileTexture(cellPos))
                    {
                        Debug.Log("Changed Tile at mouse:" + mousePos.ToString() + ", world: " + worldPos.ToString() + "grid: " + cellPos.ToString());
                        flowerPos.Add(tilemap.CellToWorld(cellPos));
                        money -= flowerPrice;

                        for (int i = 0; i < audioSrcShot.Count; ++i)
                        {
                            if (audioSrcShot[i].isPlaying == false)
                            {
                                audioSrcShot[i].clip = gardenClip;
                                audioSrcShot[i].Play();
                                break;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(1))
            {
                var mousePos = Input.mousePosition;
                var worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                var obj = Instantiate(killZonePrefab);
                obj.transform.position = new Vector3(worldPos.x, worldPos.y, 0);
                var shotIdx = Random.Range(0, shotClip.Count);
                for (int i = 0; i < audioSrcShot.Count; ++i)
                {
                    if(audioSrcShot[i].isPlaying == false)
                    {
                        audioSrcShot[i].clip = shotClip[shotIdx];
                        audioSrcShot[i].Play();
                        break;
                    }
                }
            }
            

        }
    }

    public void RacketedKid()
    {
        if(money < 150)
            money += 5;
        for (int i = 0; i < audioSrcDeath.Count; ++i)
        {
            if (audioSrcDeath[i].isPlaying == false)
            {
                audioSrcDeath[0].clip = deathClip;
                audioSrcDeath[0].volume = 0.1f;
                audioSrcDeath[0].Play();
                break;
            }
        }
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
