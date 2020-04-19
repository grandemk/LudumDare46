using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Kid : MonoBehaviour
{
    // Start is called before the first frame update
    public Shop shop = null; // from editor
    Animator animator;
    public float speed = 4;
    [SerializeField]
    int target = -1;
    [SerializeField]
    Vector3 targetCoord = new Vector3(0, 0, 0);

    public Tile destroyedTile = null;

    int GetTarget()
    {
        int numFlowers = shop.flowerPos.Count;
        if(numFlowers == 0)
          return -1;
        if(target < 0 || target >= numFlowers)
        {
            target = Random.Range(0, numFlowers);
            Debug.Log("New kid target: " + target.ToString());
        }
        return target;
    }

    void Start()
    {
        // Fix prefab bug
        shop = GameObject.Find("Panel").GetComponent<Shop>();
        animator = GetComponent<Animator>();
        int numFlowers = shop.flowerPos.Count;
    }

    Vector3 GetFlowerCoord(int target)
    {
        var coord = shop.flowerPos[target];
        coord.y += shop.tilemap.cellSize.x / 2;
        coord.y += shop.tilemap.cellSize.y / 10;
        targetCoord = coord;
        return coord;
    }

    // Update is called once per frame
    void Update()
    {
        int numFlowers = shop.flowerPos.Count;
        target = GetTarget();
        float move = 0f;
        if(target >= 0)
        {
            Vector3 direction = GetFlowerCoord(target) - transform.position;
            if (direction.magnitude <= 0.01f)
                direction = new Vector3(0f, 0f, 0f);
            direction = Vector3.Normalize(direction);
            move = direction.x;
            if (move <= 0.5f && move >= -0.5f)
                move = direction.y;
            transform.Translate(Time.deltaTime * speed * direction);
        }

        animator.SetFloat("kid_moving", move);
    }
    IEnumerator DestroyGardenCoroutine(ContactPoint2D[] contacts)
    {
        yield return new WaitForSeconds(2);
        var tilemap = shop.tilemap;
        Vector3 hitPosition = Vector3.zero;
        foreach(ContactPoint2D hit in contacts)
        {
            hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
            hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
            var cellPos = tilemap.WorldToCell(hitPosition);
            tilemap.SetTile(tilemap.WorldToCell(hitPosition), destroyedTile);
            var worldPos = tilemap.CellToWorld(cellPos);
            for (int i = 0; i < shop.flowerPos.Count; ++i)
            {
                if (shop.flowerPos[i] == worldPos)
                {
                    shop.flowerPos.RemoveAt(i);
                    Debug.Log("removed flower at " + worldPos.ToString());
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        StartCoroutine(DestroyGardenCoroutine(coll.contacts));
    }
}
