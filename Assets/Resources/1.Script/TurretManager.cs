using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public MeteorManager meteorManager;

    public bool Endturn;

    int column = 8;
    int row =3;
    Sprite renderer;
    public List<Transform> pos;
    public List<Turret> turret= new List<Turret>();

    public GameObject[] array = new GameObject[24];
    public GameObject[] turretarray = new GameObject[24];
    public int[,] check = new int[3,8];
    
   

    void Start()
    {
        
        meteorManager = GameObject.Find("MeteorManager").GetComponent<MeteorManager>();


        for (int i = 0; i < column; i++)
        {
           
            for (int j = 0; j < row; j++)
            {
                Vector3 temp = new Vector3(1.021f * i, -1f * j, 0);
                Vector3 position = this.transform.position + temp;
                GameObject game = new GameObject("position");
                
                game.AddComponent<SpriteRenderer>().sprite = renderer;
             
                pos.Add(game.transform);
                game.transform.position = position;

                array[i*row+j] = game;
                check[j, i] = 0;

            }
           
        }
    }

    void FixedUpdate()
    {
        EndTurn();
    }
    void EndTurn()
    {
        if (Endturn)
        {
            Debug.Log("공격");
            TurretAttack();
            Endturn = false;
        }
    }

    public bool checkposition(int i , GameObject gameObject )
    {
        row = (int)(i % 3);
        column =(int)( i / 3);

       if(check[row,column] == 1)
        {
            Debug.Log("존재");
            if (turretarray[row* column+row] != null)
            {
                if (gameObject == turretarray[row * column + row])
                {
                    return true;
                }
            }


            return false;
        }
       else
        {
          //  Debug.Log("비엇음");
            return true;
        }

    }

    public void setposition(int i, GameObject gameObject)
    {
        row = (int)(i % 3);
        column = (int)(i / 3);
        check[row, column] = 1;
        turretarray[i] = gameObject;
        //Debug.Log(array[row, column]);
    }
    public void Resetposition( int i)
    {

        row = (int)(i % 3);
        column = (int)(i / 3);
        check[row, column] = 0;
        turretarray[i] = null;
       
    }


    public void TurretAttack()
    {
        List<AttackClass> attackClass= new List<AttackClass>();
        for ( int i  = 0; i < turretarray.Length;i++)
        {
            if (turretarray[i] == null)
                continue;
            //공격정보 설정
            else 
            {
                AttackClass attack = new AttackClass();
                Turret turret = turretarray[i].GetComponent<Turret>();

             //   Debug.Log(turretarray[i].transform.position);

                int A_row = (int)(i % 3);
                int A_column = (int)(i / 3);

                if (A_row == 0)
                {
                    
                     turret.re_tri = 0; 
                }
                if (A_row == 1)
                {
                    if (turretarray[i - A_row])
                    {
                        if (turretarray[i - A_row].gameObject.name == "Re_Tri(Clone)")
                        {

                        }
                        else { turret.re_tri = 0; }
                    }
                    else { turret.re_tri = 0; }
                }
                if (A_row == 2)
                {
                    if (turretarray[i - A_row+1])
                    {
                        if (turretarray[i - A_row+1].gameObject.name == "Re_Tri(Clone)")
                        {

                        }
                        else 
                        {
                            if (turretarray[i - A_row])
                            {
                                if (turretarray[i - A_row ].gameObject.name == "Re_Tri(Clone)")
                                {

                                }

                            }
                            else { turret.re_tri = 0; }
                        }
                    }
                    else if (turretarray[i - A_row])
                    {
                        if (turretarray[i - A_row].gameObject.name == "Re_Tri(Clone)")
                        {

                        }

                    }
                    else { turret.re_tri = 0; }

                }

                attack.game = turretarray[i];
                attack.name = turret.name;
                attack.Type = turret.Gettype();
                attack.Damage = turret.GetDamage();
                string name = turret.gameObject.name;

                switch (name) {
                   
                    case "CrossStar(Clone)":
                        for (int j = A_column; j <= A_column; j++)//세로
                        {
                            if (j < 0 || j > 7)
                                continue;

                            for (int k = 2; k < 5; k++)//가로
                            {

                                attack.Area[k, j] = 1;
                                Debug.Log(k + " " + j);
                            }
                        }

                        Debug.Log("CrossStar");
                        break;


                    case "Circle(Clone)":
                        for(int j = A_column - 1; j< A_column + 2; j++  )//세로
                        {
                            if (j < 0 || j > 7)
                                continue;


                            for(int k =0; k < 4; k++)//가로
                            {
                                if (k < 0 || k > 6)
                                    continue;

                                attack.Area[k, j] = 1;
                            }
                        }

                        Debug.Log("Circle");
                        break;





                    case "Rect(Clone)":

                        for (int j = A_column; j < A_column + 1; j++)//세로
                        {
                            if (j < 0 || j > 7)
                                continue;
                            for (int k = 0; k < 7; k++)//가로
                            {
                                if (k < 0 || k > 6)
                                    continue;

                                attack.Area[k, j] = 1;
                             //   Debug.Log(k + " " + j);
                            }
                        }




                        //Debug.Log("Rect");
                        break;





                    case "Re_Tri(Clone)":

                        if (turretarray[i+1]&& (i%3+1)/3==0)//뒤에 포탑이 있으면
                        {
                            turretarray[i + 1].GetComponent<Turret>().re_tri = turret.Level;
                        }

                        if (turretarray[i + 2] && (i%3 + 2) / 3 == 0)//뒤에 포탑이 있으면
                        {
                            turretarray[i + 2].GetComponent<Turret>().re_tri = turret.Level;
                        }
                        break;





                    case "Cross(Clone)":
                        for (int j = 0; j < 8 ; j++)//세로
                        {
                            if (j < 0 || j > 7)
                                continue;

                            for (int k = 0; k < 7; k++)//가로
                            {
                                if (k < 0 || k > 6)
                                    continue;

                                attack.Area[k, j] = 1;
                                Debug.Log(k + " " + j);
                            }
                        }
                        Debug.Log("Cross");
                        break;





                    case "Penta(Clone)":

                        for (int j = A_column - 1; j <= A_column + 1; j++)//세로
                        {
                            if (j < 0 || j > 7)
                                continue;

                            for (int k = 3; k < 7; k++)//가로
                            {
                               
                                attack.Area[k, j] = 1;
                                Debug.Log(k + " " + j);
                            }
                        }

                        Debug.Log("Penta");
                        break;




                    case "Tri(Clone)":

                        for (int k = 0; k < 7; k++)//가로
                        {
                            if (k < 0 || k > 6)
                                continue;

                            if(A_column - 1>=0)
                                attack.Area[k, A_column - 1] = 1;
                            if(A_column + 1<=7)
                                attack.Area[k, A_column + 1] = 1;
                            Debug.Log(k + " " + (A_column - 1) + " " + (A_column + 1));
                        }


                        Debug.Log("Tri");
                        break;



                    case "Rhombus(Clone)":

                        for (int k = 0; k < 7; k++)//가로
                        {
                            if (k < 0 || k > 6)
                                continue;

                            if (A_column - 2 >= 0)
                                attack.Area[k, A_column - 2] = 1;
                            if (A_column + 2 <= 7)
                                attack.Area[k, A_column + 2] = 1;
                            Debug.Log(k + " " + (A_column - 2) + " " + (A_column + 2));
                        }

                        Debug.Log("Rhombus");
                        break;


                    default:
                        break;
                    

                }

                attackClass.Add(attack);

                attack = null;

            }

            //포탑 매니저로 보내는 코드
           

        }
        meteorManager.setAttackClass(attackClass);

        //for(int i = 0; i < attackClass.Count;i++)
        //{
        //    Debug.Log(attackClass[i].name + "    count :" + i);
        //   for(int j = 0; j < 8; j++)
        //    {
        //        for(int k = 0; k < 7; k++)
        //        {
                   
        //                Debug.Log( " value :  "+ attackClass[i].Area[k, j] + "   j : "+j + " k : " + k);
        //        }
        //    }
        //}


    }

    public void DeleteTurret(GameObject game)
    {
        for( int i =0; i < turretarray.Length;i++)
        {
            if(turretarray[i])
            {
                if(turretarray[i]==game)
                {
                    turretarray[i] = null;
                    break;
                }
            }

        }

        
    }

    public bool CheckTurret(GameObject game)
    {
        for (int i = 0; i < turretarray.Length; i++)
        {
            if (turretarray[i])
            {
                if (turretarray[i] == game)
                {
                    return true;
                }
            }

        }
        return false;

    }

}

