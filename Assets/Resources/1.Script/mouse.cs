using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class mouse : MonoBehaviour
{

    private GameObject target;
    float distance = 10f;
    public List<Transform> pos;



    void Start()
    {
        pos = GameObject.Find("TurretManager").GetComponent<TurretManager>().pos;

    }
        void Update()
        {
        SetTurretPosition();
        }
        void FixedUpdate()

        {

            if (Input.GetMouseButtonDown(0))
            {

                CastRay();

                if (target == this.gameObject)
                {
                    OnMouseDrag();

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

                //Debug.Log (hit.collider.name);  //이 부분을 활성화 하면, 선택된 오브젝트의 이름이 찍혀 나옵니다. 

                target = hit.collider.gameObject;  //히트 된 게임 오브젝트를 타겟으로 지정
           
                Debug.Log(target);


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
            Debug.Log(this.transform.position);
            Vector2 temp = Vector2.zero;
            float dis = 0f;
            for (int i = 0; i < pos.Count; i++)
            {
                float tempdis = Vector2.Distance(this.transform.position, pos[i].position);

                if (i == 0)
                {
                    dis = tempdis;
                    temp = pos[i].position;
                }
                else if (dis > tempdis)
                {
                    dis = tempdis;
                    temp = pos[i].position;
                }
            }


            this.transform.position = temp;

        }
    }


}
