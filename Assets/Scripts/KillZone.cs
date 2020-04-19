using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public Shop shop = null;

    void Start() {
        // Fix prefab bug
        shop = GameObject.Find("Panel").GetComponent<Shop>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Kill Zone collision");
        if(other.gameObject.tag == "Kid")
        {
            Destroy(other.gameObject);
            shop.RacketedKid();
        }
    }
}
