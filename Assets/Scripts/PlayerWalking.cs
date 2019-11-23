using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{

    public float speed,
                 horBoundary,
                 verBoundary;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        SetVelocity();
        ClampPosition();

    }

    private void SetVelocity(){

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(x, y);
        movement.Normalize();
        movement *= speed;
        rb.velocity = movement;

    }

    private void ClampPosition(){

        rb.position = new Vector2(Mathf.Clamp(rb.position.x, -horBoundary, horBoundary),
                                    Mathf.Clamp(rb.position.y, - verBoundary, verBoundary));

    }

}
