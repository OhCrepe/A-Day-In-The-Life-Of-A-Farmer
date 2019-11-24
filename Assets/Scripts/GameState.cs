using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{

    public float time;
    public bool day;
    public Text milkText, moneyText;

    private SpriteRenderer cycle;
    private int milkCounter;
    private int cash;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        cycle = GameObject.Find("DayNight").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        cycle.color = new Color(1f, 1f, 1f, time);
    }

    public void CollectMilk(){
        milkCounter++;
        UpdateMilkText();
    }

    private void UpdateMilkText(){
        milkText.text = "x " + milkCounter;
    }

    private void UpdateMoneyText(){
        moneyText.text = "x " + cash;
    }

    public void CowsToSleep(){
        GameObject[] cows = GameObject.FindGameObjectsWithTag("Cow");
        foreach(GameObject cow in cows){
            cow.GetComponent<CowBehaviour>().Sleep();
        }
    }

    public void CowsWakeUp(){
        GameObject[] cows = GameObject.FindGameObjectsWithTag("Cow");
        foreach(GameObject cow in cows){
            cow.GetComponent<CowBehaviour>().WakeUp();
        }
    }

    public int GetMilkCounter(){
        return milkCounter;
    }

    public void SellMilk(){
        milkCounter = 0;
        UpdateMilkText();
    }

    public void GainCash(int money){
        cash += money;
        UpdateMoneyText();
    }

}
