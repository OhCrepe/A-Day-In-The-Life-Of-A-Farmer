using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkCow : MonoBehaviour
{

    public float minDelay, maxDelay, collectDistance;

    private GameObject milkSymbol, player; 
    public GameObject milkPrefab;
    private bool collectable;
    private GameState gamestate;

    // Start is called before the first frame update
    void Start()
    {
        milkSymbol = transform.Find("Milk").gameObject;
        collectable = true;
        gamestate = GameObject.Find("GameState").GetComponent<GameState>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if(collectable && gamestate.day && !GetComponent<CowBehaviour>().breeding && GetComponent<CowBehaviour>().adult){
            milkSymbol.SetActive(true);
        }else{
            milkSymbol.SetActive(false);
        }

    }

    IEnumerator collectMilkAndDelay(){

        gamestate.CollectMilk();
        collectable = false;
        float delay = Random.Range(minDelay, maxDelay);
        yield return new WaitForSeconds(delay);
        collectable = true;

    }

    void OnMouseDown(){
        GetComponent<AudioSource>().Play();
        if(collectable && gamestate.day && !GetComponent<CowBehaviour>().breeding && GetComponent<CowBehaviour>().adult){
            if(Vector3.Distance(player.transform.position, transform.position) < collectDistance){
                StartCoroutine(collectMilkAndDelay());
                Instantiate(milkPrefab, player.transform.Find("MilkSpawn").position, transform.rotation);
            }
        }
    }

}
