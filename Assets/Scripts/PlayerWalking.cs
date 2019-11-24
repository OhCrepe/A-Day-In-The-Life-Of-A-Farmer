using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{

    public float speed,
                 horBoundary,
                 verBoundary;

    private bool moving;
    private Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moving = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        SetVelocity();
        //ClampPosition();
        SetDirection();

    }

    private void SetVelocity(){

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(x, y);
        movement.Normalize();
        movement *= speed;
        rb.velocity = movement;
        if(rb.velocity.magnitude > 0.1f){
            if(!moving){
                anim.SetTrigger("Walk");
                moving = true;
            }
        }else if(x == 0 && y == 0){
            if(moving){
                anim.SetTrigger("NotWalk");
                moving = false;
                Debug.Log("NotWalk");
            }
        }

    }

    private void SetDirection(){

        Vector3 vel = rb.velocity;
        if(vel.y < -0.1f){
            anim.SetTrigger("Forward");
        }else if(vel.y > 0.1f){
            anim.SetTrigger("Backward");
        }else if(vel.x < -0.1f){
            anim.SetTrigger("Left");
        }else if(vel.x > 0.1f){
            anim.SetTrigger("Right");
        }
        Debug.Log(vel);

    }

    private void ClampPosition(){

        rb.position = new Vector2(Mathf.Clamp(rb.position.x, -horBoundary, horBoundary),
                                    Mathf.Clamp(rb.position.y, - verBoundary, verBoundary));

    }

}
