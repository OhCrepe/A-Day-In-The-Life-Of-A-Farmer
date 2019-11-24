using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBehaviour : MonoBehaviour
{

    private SpriteRenderer renderer;
    private float timeTilDeath;
    private Vector3 speed;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        timeTilDeath = 1f;
        speed = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(1f, 1.5f), 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * Time.deltaTime;
        timeTilDeath -= Time.deltaTime;
        renderer.color = new Color(1f, 1f, 1f, timeTilDeath);
        if(timeTilDeath <= 0f){
            Destroy(gameObject);
        }
    }
}
