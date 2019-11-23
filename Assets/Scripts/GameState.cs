using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

    public float time;

    private SpriteRenderer cycle;

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
}
