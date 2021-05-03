using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    TurretManager TurretManager;
    MeteorManager MeteorManager;
   
    Reward reward;
    public Inventory inventory;
    public GameObject MeteorManagerObj;
    public GameObject RewardObj;
    public GameObject baseTurret;
    int wave;

    public int round;
    public int meteor;
    public GameObject[] turrets;//라운드당 가방 터렛 
    public int[] Types;

    public bool victory;
    public bool defeat;
    bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(baseTurret);
        Instantiate(baseTurret);
        
        MeteorManager = MeteorManagerObj.GetComponent<MeteorManager>();
        reward = RewardObj.GetComponent<Reward>();
        
        victory = false;
        defeat = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver == false) 
        {
            GameOver();
            End();
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            reward.SetRewardTable();
        }
       
    }


    public void ManagerSetting()
    {

    }




    public void End()
    {
        if (victory)
        {
            Debug.Log("승리");
            //승리시 보상 테이블 표출
            //다음 라운드의 metor 셋팅
            MeteorManager.NextWave();
            reward.RewardUI.gameObject.SetActive(true);
            reward.SetRewardTable();
            victory = false;
        }
        else if (defeat)
        {

            Debug.Log("패배");
            GameObject.Find("Alert").gameObject.GetComponent<AlertUI>().GameOverUI();
            GameObject.Find("Alert").gameObject.GetComponent<AlertUI>().SetText("Game Over");
            GameObject.Find("Alert").gameObject.GetComponent<AlertUI>().SetGameOverText();
            StartCoroutine(ChangeScene());
            gameOver = true;
        }

    }

    public void Interrupt()
    {

    }

    /// <summary>
    /// 포탑의 데미지 테이블과 운석의 health 테이블을 계산한다.
    /// </summary>
    public void NextTurn()
    {

        //포탑의 데미지 테이블 가져오기
        //운석의 체력 테이블 가져오기(운석오브젝트 테이블을 가져와서 해당 위치의 운석 hp를 깎는다)
        //계산
        //포탑의 공격 모션 실행 호출
        //운석 매니저의 nextTurn 호출(공격모션이 끝난 시점과 싱크를 맞출 것)
        MeteorManager.NextTurn();
    }

    void GameOver() {
        defeat = MeteorManager.gameOver;

    }
    void NextWave() {
        victory = true;
    }
    IEnumerator ChangeScene() {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("StartScene");
    }
}
