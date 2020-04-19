using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour
{
    // Start is called before the first frame update
    public Shop shop = null; // from editor
    Animator animator;
    public float speed = 4;
    int target = -1;

    int GetTarget()
    {
        int numFlowers = shop.flowerPos.Count;
        if(target < 0 || target > numFlowers)
            target = Random.Range(0, numFlowers);
        Debug.Log(target);
        return target;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        int numFlowers = shop.flowerPos.Count;
    }

    Vector3 GetFlowerCoord(int target)
    {
        var coord = shop.flowerPos[target];
        coord += shop.tilemap.cellSize / 2;
        return coord;
    }

    // Update is called once per frame
    void Update()
    {
        int numFlowers = shop.flowerPos.Count;
        target = GetTarget();
        Vector3 direction = GetFlowerCoord(target) - transform.position; 
        if(direction.magnitude <=0.01f)
          direction = new Vector3(0f, 0f, 0f);
        direction = Vector3.Normalize(direction);
        float move = direction.x;
        if(move <= 0.5f && move >= -0.5f)
          move = direction.y;


        transform.Translate(Time.deltaTime * speed * direction);
        animator.SetFloat("kid_moving", move);
    }
}
