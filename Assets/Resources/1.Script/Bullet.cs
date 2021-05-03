using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;
    public int Velocity=2;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0 ,Velocity* Time.deltaTime ,0);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if(collision.gameObject.tag == "Enemy")
        {
            Meteor temp =  collision.gameObject.GetComponent<Meteor>();
            if(temp)
            {
                temp.health -= Damage;
                
            }


        }
        Destroy(this);

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        Destroy(this);
    }
}
