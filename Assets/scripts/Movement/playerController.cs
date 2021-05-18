using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;

    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;
    private Inventory inventory;

    private void Start()
    {
        inventory = new Inventory();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKey(left))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(right))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y );
        }
        else if (Input.GetKey(up))
        {
            rb.velocity = new Vector2(rb.velocity.x , moveSpeed);
        }
        else if (Input.GetKey(down))
        {
            rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            rb.velocity = new Vector2(0, rb.velocity.x);
        }
    }

}
