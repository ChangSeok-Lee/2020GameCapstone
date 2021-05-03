using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class moveTo : MonoBehaviour
{

    public Vector3 initial;
    public Vector3 destination;
    public int time;
    public float speed=10;
   

    void Start()
    {
     

        Destroy(this.gameObject, 4f);
    }
    // Update is called once per frame
    void Update()
    {
        if (initial != null && destination != null)
            this.transform.position = Vector3.MoveTowards(this.transform.position, destination, speed*Time.deltaTime);


        
    }
}
