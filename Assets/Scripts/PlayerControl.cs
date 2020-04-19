using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.5f;
    private Animator animator = null;
    public Shop shop = null;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(Time.deltaTime * speed * direction);

        float move = horizontalInput;
        if(move <= 0.01f && move >= -0.01f)
          move = verticalInput;
          
        animator.SetFloat("moving", move);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Kid")
        {
            Debug.Log("Catched Kid");
            Destroy(other.gameObject);
            shop.RacketedKid();
        }
    }
}
