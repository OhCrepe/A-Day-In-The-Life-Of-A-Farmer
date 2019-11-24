using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellMilk : MonoBehaviour
{

    private GameState gamestate;
    private bool sellable;
    private Animator anim;
    private int cashIncrease;

    void Start(){
        gamestate = GameObject.Find("GameState").GetComponent<GameState>();
        sellable = true;
        anim = GetComponent<Animator>();
    }

    void OnMouseDown(){

        if(sellable && gamestate.GetMilkCounter() > 0){
            Debug.Log("Yo");
            sellable = false;
            cashIncrease = gamestate.GetMilkCounter()*10;
            anim.SetTrigger("DriveAway");
            gamestate.SellMilk();
        }

    }

    public void ReturnToFarm(){
        sellable = false;
        anim.SetTrigger("Return");
        gamestate.GainCash(cashIncrease);
        cashIncrease = 0;
    }

}
