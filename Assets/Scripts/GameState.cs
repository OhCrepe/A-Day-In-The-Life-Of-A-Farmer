using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{

    public float time;
    public Text milkText;

    private SpriteRenderer cycle;
    private int milkCounter;

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

}
