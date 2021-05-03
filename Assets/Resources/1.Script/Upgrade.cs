using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Upgrade : MonoBehaviour
{

    public int Up;
    public int Type;
    public TurretManager turretManager;
    public SpriteRenderer TypeSprite;

    GameObject target;
    public bool grabbed;
	private void Awake()
	{
        Type = 0;
        target = null;
        turretManager = GameObject.Find("TurretManager").GetComponent<TurretManager>();
        TypeSprite = GetComponent<SpriteRenderer>();
    }
	void Start()
    {
        


    }
    void Update()
    {

        TurretMove();

    }

    void TurretMove()
    {
        if (Input.GetMouseButtonDown(0))
        {

            CastRay();
            if (target != null)
            {
                if (this.gameObject == target.gameObject)
                {
                    grabbed = true;
                    OnMouseDrag();
                }


            }

        }

        if (Input.GetMouseButtonUp(0))
        {

           

            grabbed = false;

        }


    }


        void CastRay() // 유닛 히트처리 부분.  레이를 쏴서 처리합니다. 
        {
            target = null;

            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if (hit.collider != null)
            { //히트되었다면 여기서 실행

                //  Debug.Log (hit.collider.name);  //이 부분을 활성화 하면, 선택된 오브젝트의 이름이 찍혀 나옵니다. 

                target = hit.collider.gameObject;  //히트 된 게임 오브젝트를 타겟으로 지정

                //Debug.Log(target);

            }

        }

        void OnMouseDrag()
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            this.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        }

    


    void OnTriggerStay2D(Collider2D collider2D)
    {

        Debug.Log(collider2D.gameObject.name);

        if(this.gameObject.name == "trans(Clone)")
        {
            if (!grabbed)
            {
                if (collider2D.gameObject.tag == "Turret")
                {
                    collider2D.gameObject.GetComponent<Turret>().Type = Type;
                    collider2D.gameObject.GetComponent<Turret>().SetType();
                    turretManager.Endturn = true;
                    Destroy(this.gameObject);
                }
            }

        }
        else
        {
            if (!grabbed)
            {
                if (collider2D.gameObject.tag == "Turret")
                {
                    collider2D.gameObject.GetComponent<Turret>().Level += Up;
                    
                    turretManager.Endturn = true;
                    Destroy(this.gameObject);
                }
            }
        }



      

           

    }

    public int SetType()
    {
        switch (Type)
        {
            case 0:
                TypeSprite.color = Color.white;
                break;
            case 1:
                TypeSprite.color = Color.red;
                break;
            case 2:
                TypeSprite.color = Color.green;
                break;
            case 3:
                TypeSprite.color = Color.blue;
                break;

            default:

                break;




        }



        return Type;
    }

}






