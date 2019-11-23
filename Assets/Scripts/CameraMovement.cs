using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject target;
    public float horBoundary, verBoundary;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = this.transform.position - target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.transform.position + offset;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -horBoundary, horBoundary),
                                        Mathf.Clamp(transform.position.y, -verBoundary, verBoundary),
                                        transform.position.z);
    }
}
