using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
//using UnityEditorInternal;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Inventory inventory;
    public SpriteRenderer LevelSprite;
    public SpriteRenderer TypeSprite;
    public TurretManager turretManager;
    public GameObject Info;
    public Sprite infoSprite;
    public GameObject[] Effect;
   

    public int InitialDamage ;
    public int Level ;
    public int re_tri;
    public int Type ;

    public bool EndTurn;
    public bool signal;


    public List<Sprite> TypeSprites =new List<Sprite>();
    public List<Sprite> Levelsprites = new List<Sprite>();
    public List<Sprite> Turretsprites = new List<Sprite>();
    public List<Transform> pos;



    private GameObject target;
    public Vector3 trans;

    public float delay;

    public bool loaded;
    public bool grabbed;
    public bool Click;
    public int pre;
    public int a = 0;

    void Awake()
    {
       
        inventory = GameObject.Find("Item").GetComponent<Inventory>();
        TypeSprite = this.GetComponent<SpriteRenderer>();
        turretManager = GameObject.Find("TurretManager").GetComponent<TurretManager>();
        LevelSprite = transform.Find("GameObject").GetComponent<SpriteRenderer>();
        EndTurn = false;
        pos = GameObject.Find("TurretManager").GetComponent<TurretManager>().pos;
        GameObject.Find("TurretManager").GetComponent<TurretManager>().turret.Add(this);
        Click = false;
        delay = 0;
        Type = 0;
        Level = 1;
        re_tri = 0;
    }

    void Start()
    {
        //Click = false;
        //delay = 0;
        //Type = 3;
        //Level = 4;
        //re_tri = 0;
    }
    // Start is called before the first frame update

    private void Update()
    {
        if (EndTurn)
        {
            SetType();
            Attack();
            if (!loaded)
            {
                EndTurn = false;
            }
            EndTurn = false;
        }


        delay += Time.deltaTime;

        if (delay >= 2f && Click== false)
        {
            Click = true;
            
        }
       




        if (loaded)
            SetTurretPosition();

        SetLevel();

       
            TurretMove();
        // Attack();
        ViewTurretInfo();
        Debug.Log("type : " + Type);
    }



    public void Attack()
    {

        int row = (int)(a % 3);
        int column = (int)(a / 3);
        GameObject temp;
        GameObject temp2;
        moveTo move;
        switch (this.gameObject.name)
        {

            case "CrossStar(Clone)":


                temp = Instantiate(Effect[3], this.transform.position, Quaternion.identity);

                move = temp.AddComponent<moveTo>();
                move.destination = this.transform.position + new Vector3(0, 1f * (row + 4), 0);

                move.initial = this.transform.position;
                move.speed = 8;


                break;


            case "Circle(Clone)":


                temp = Instantiate(Effect[5], this.transform.position, Quaternion.identity);
               
                move = temp.AddComponent<moveTo>();
                move.destination = this.transform.position + new Vector3(0, 1f * (row + 5), 0);
              
                move.initial = this.transform.position;
                move.speed = 8;


                break;





            case "Rect(Clone)":

                temp = Instantiate(Effect[1], this.transform.position, Quaternion.identity);

                move = temp.AddComponent<moveTo>();
                move.destination = this.transform.position + new Vector3(0, 1f * ( 1), 0);

                move.initial = this.transform.position;
                move.speed = 50;

              //  Debug.Log("Rect");
                break;





            case "Re_Tri(Clone)":


                break;





            case "Cross(Clone)":

                temp = Instantiate(Effect[2], this.transform.position, Quaternion.identity);

                move = temp.AddComponent<moveTo>();
                move.destination = Vector3.zero+ Vector3.up*2;

                move.initial = this.transform.position;
                move.speed = 7;






                break;





            case "Penta(Clone)":


                temp= Instantiate(Effect[0], this.transform.position, Quaternion.identity);
                move= temp.AddComponent<moveTo>();
               // temp.transform.position = this.transform.position + new Vector3(0, 1f * (row + 1),0) ;
                move.destination = this.transform.position + new Vector3(0, 1f * (row + 1), 0);
                move.initial = this.transform.position;
                break;




            case "Tri(Clone)":
                if (column >= 1)
                {
                    temp = Instantiate(Effect[1], this.transform.position, Quaternion.identity);
                   
                    temp.transform.position = this.transform.position + new Vector3(1.021f * -1, 0, 0);
                    Destroy(temp, 4f);
                }
                if (column <= 6)
                {
                    temp = Instantiate(Effect[1], this.transform.position, Quaternion.identity);
                    Destroy(temp, 4f);

                    temp.transform.position = this.transform.position + new Vector3(1.021f * 1, 0, 0);
                }

                break;



            case "Rhombus(Clone)":
                if (column >= 2)
                {
                    temp = Instantiate(Effect[4], this.transform.position, Quaternion.identity);
                    move = temp.AddComponent<moveTo>();
                    move.destination = this.transform.position + new Vector3(1.021f * -2, 0, 0);
                    move.initial = this.transform.position;
                }
                if (column <= 5)
                {
                    temp = Instantiate(Effect[4], this.transform.position, Quaternion.identity);
                    move = temp.AddComponent<moveTo>();
                    move.destination = this.transform.position + new Vector3(1.021f * 2, 0, 0);
                    move.initial = this.transform.position;
                }


                break;


            default:
                break;


        }
    }


    void TurretMove()
    {
        if (Input.GetMouseButtonDown(0))
        {

            CastRay();
            if (target != null)
            {
                if ("Turret" == target.gameObject.tag && this.gameObject == target.gameObject)
                {

                    if (grabbed == false)
                    {
                        trans = this.gameObject.transform.position;
                    }

                   
                    grabbed = true;
                    OnMouseDrag();

                }



                if ("Upgrade" == target.gameObject.tag && this.gameObject == target.gameObject)
                {

                    Debug.Log("upgrade");

                    OnMouseDrag();

                }


            }

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


    void SetTurretPosition()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (target != null)
            {
                if ("Turret" == target.gameObject.tag && this.gameObject == target.gameObject)
                {
                    Debug.Log(this.name);
                    Vector2 temp = Vector2.zero;
                    float dis = 0f;

                    //제일 가까운 오브젝트
                    for (int i = 0; i < pos.Count; i++)
                    {
                        float tempdis = Vector2.Distance(this.transform.position, pos[i].position);
                        if (i == 0)
                        {
                            dis = tempdis;
                            temp = pos[i].position;
                            a = i;
                        }
                        else if (dis >= tempdis)
                        {
                            dis = tempdis;
                            temp = pos[i].position;
                            a = i;
                        }
                    }


                    if (turretManager.checkposition(a, this.gameObject))
                    {

                        if (target.gameObject.tag == "Turret")
                        {
                            this.transform.position = temp;
                            turretManager.Resetposition(pre);
                            // Debug.Log("pre " + pre + "  a :" + a);
                            turretManager.setposition(a, this.gameObject);
                            pre = a;
                            turretManager.Endturn = true;
                        }

                      
                    }

                    else
                    {
                        if (target.gameObject.tag == "Turret")
                        {     Debug.Log("원래 위치로");
                            this.transform.position = trans;
                            if (!turretManager.CheckTurret(this.gameObject))
                            {
                                this.transform.position = inventory.transform.position;
                            }
                        }



                    }



                }



             




                

            }

            inventory.Setting();

            grabbed = false;
            Click = false;
            delay = 0;
        }
    }



    public void SetLevel()
    {
        if (this.gameObject.tag == "Turret")
        { 
        if (((Level - 1) + re_tri) >= 5)
        { LevelSprite.sprite = Levelsprites[4]; }
        else { LevelSprite.sprite = Levelsprites[((Level - 1) + re_tri) % Levelsprites.Count]; }
        }
    }


    public void ViewTurretInfo()
    {
        if (Input.GetMouseButtonDown(1))
        {

            CastRay();

            if (target != null)
            {
                if ("Turret" == target.gameObject.tag && this.gameObject == target.gameObject)
                {

                    Debug.Log(target.gameObject);
                    Vector3 direction = (Vector3.zero - target.transform.position).normalized;
                    float distance = 2.5f;

                    Info.transform.position = this.transform.position + direction * distance;
                    Info.GetComponent<SpriteRenderer>().sprite = infoSprite;


                }
            }

        }
        else if (Input.GetMouseButtonUp(1))
        {
            Info.GetComponent<SpriteRenderer>().sprite = null;
        }


    }


    public int GetDamage()
    {
        if (((Level - 1) + re_tri) >= 5)
            return 4 * InitialDamage;
        else
            return (re_tri + Level) * InitialDamage;
    }
    public int Gettype()
    {
        return Type;
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

    public void OnTriggerEnter2D(Collider2D col)
    {
       
        if(col.tag == "Inventory")
        {
            
            loaded = false;

            col.gameObject.GetComponent<Inventory>().AddItem(this.gameObject);
            turretManager.DeleteTurret(this.gameObject);
            re_tri = 0;

        }
       
    }
    public void OnTriggerExit2D(Collider2D col)
    {

      
        if (col.tag == "Inventory")
        {
            loaded = true;

            col.gameObject.GetComponent<Inventory>().DeleteItem(this.gameObject);

        }

    }

  


}
