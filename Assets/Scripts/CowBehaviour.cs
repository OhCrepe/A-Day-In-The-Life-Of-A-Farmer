using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowBehaviour : MonoBehaviour
{

    public float speed, minWalkDelay, maxWalkDelay;
    public bool breeding;

    private Animator anim;
    private Vector3 destination, previousLocation;
    private bool moving;
    private GameState gamestate;
    private Rigidbody2D rb;
    public GameObject mate;
    public GameObject child;
    public GameObject heart;

    private float growth;
    public bool adult;

    void Awake(){
        gamestate = GameObject.Find("GameState").GetComponent<GameState>();
        anim = GetComponent<Animator>();
        destination = transform.position;
        previousLocation = transform.position;
        moving = false;
        breeding = false;
        StartCoroutine(DecideDestination());
        StartCoroutine(Breeding());
        rb = GetComponent<Rigidbody2D>();
        growth = 1f;
        adult = true;
    }

    public void BeBorn(){
        growth = 0.5f;
        adult = false;
        transform.localScale = new Vector3(growth, growth, 1f);
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

        if(!adult){
            growth += Time.deltaTime/200;
            if(growth >= 1f){
                growth = 1f;
                adult = true;
            }
            transform.localScale = new Vector3(growth, growth, 1f);
        }
        rb.velocity = new Vector3();
        if(breeding && gamestate.day){
            transform.position = Vector3.MoveTowards(transform.position, mate.transform.position, speed*Time.deltaTime);
            if(Vector3.Distance(transform.position, mate.transform.position) <= 2f){
                Breed();
            }
        }else if(moving && gamestate.day){
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

    IEnumerator Breeding(){

        yield return new WaitForSeconds(Random.Range(40f, 60f));
        if(!breeding && adult){
            GameObject[] cows = GameObject.FindGameObjectsWithTag("Cow");
            foreach(GameObject cow in cows){
                if(cow != this.gameObject){
                    if(!cow.GetComponent<CowBehaviour>().breeding){
                        breeding = true;
                        mate = cow;
                        cow.GetComponent<CowBehaviour>().BreedWith(gameObject);
                        anim.SetTrigger("Walk");
                        break;
                    }
                }
            }
            if(mate == null){
                StartCoroutine(Breeding());
            }
        }

    }

    public void BreedWith(GameObject cow){
        breeding = true;
        mate = cow;
        anim.SetTrigger("Walk");
    }

    private void Breed(){
        Vector3 distance = mate.transform.position - transform.position;
        distance = distance * 0.5f;
        Vector3 childPos = transform.position + distance;
        GameObject bornChild = Instantiate(child, childPos, transform.rotation);
        bornChild.GetComponent<CowBehaviour>().BeBorn();
        mate.GetComponent<CowBehaviour>().NotBreeding();
        NotBreeding();

    }

    public void NotBreeding(){
        Instantiate(heart, transform.Find("HeartSpawn").position, transform.rotation);
        breeding = false;
        mate = null;
        anim.SetTrigger("Stand");
        StartCoroutine(Breeding());
    }

}
