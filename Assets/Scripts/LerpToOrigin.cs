using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToOrigin : MonoBehaviour
{
    Vector3 original;

    [SerializeField] float time;
    [SerializeField] float amplitude;
    float begin;


    // Start is called before the first frame update
    void Start()
    {
        original = transform.position;
        begin = time;
    }

    // Update is called once per frame
    void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
            transform.position = Vector3.Lerp(Vector3.zero, original, time / begin);
        }
        else
        {
            transform.position = new Vector3(0, Mathf.Cos(Time.time) * -amplitude);
        }
    }
}
