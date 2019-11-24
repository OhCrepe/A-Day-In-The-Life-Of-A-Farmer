using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowBehaviour : MonoBehaviour
{

    public float speed, minWalkDelay, maxWalkDelay;

    private Animator anim;
    private Vector3 destination, previousLocation;
    private bool moving;
    private GameState gamestate;
    private Rigidbody2D rb;

    void Start(){
        gamestate = GameObject.Find("GameState").GetComponent<GameState>();
        anim = GetComponent<Animator>();
        destination = transform.position;
        previousLocation = transform.position;
        moving = false;
        StartCoroutine(DecideDestination());
        rb = GetComponent<Rigidbody2D>();
    }

    public void WakeUp(){
        anim.SetTrigger("WakeUp");
        StartCoroutine(DecideDestination());
    }
    public void Sleep(){
        anim.SetTrigger("Sleep");
        destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = new Vector3();
        if(moving && gamestate.day){
            transform.position = Vector3.MoveTowards(transform.position, destination, speed*Time.deltaTime);
            if(Vector3.Distance(transform.position, destination) < 0.01f || Vector3.Distance(transform.position, previousLocation) < 0.01f){
                anim.SetTrigger("Stand");
                StartCoroutine(DecideDestination());
                moving = false;
                destination = transform.position;
            }
            previousLocation = transform.position;
        }
        if(!gamestate.day){
            moving = false;
            destination = transform.position;
        }

    }

    IEnumerator DecideDestination(){
        Debug.Log("Waiting");
        yield return new WaitForSeconds(Random.Range(minWalkDelay, maxWalkDelay));
        float distance = Random.Range(2f, 5f);
        float angle = Random.Range(0f, 360f);
        destination = new Vector3(transform.position.x + (distance*Mathf.Sin(angle)),
                                    transform.position.y + (distance*Mathf.Cos(angle)),
                                    transform.position.z);
        if(gamestate.day){
            if(destination.x < transform.position.x){
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }else{
                transform.rotation = new Quaternion(0, 180, 0, 0);
            }
            moving = true;
            anim.SetTrigger("Walk");
        }
    }

}
