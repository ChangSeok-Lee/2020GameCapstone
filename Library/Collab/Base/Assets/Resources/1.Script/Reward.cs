using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class RewardItem {
    public string item;
    public string name;
    public int name_index;
    public int level;
    public int type;
}
public class Reward : MonoBehaviour
{

    //RewardUI의 각 버튼에 해당
    Image Slot1;
    Image Slot2;
    Image Slot2_L;
    Image Slot3;
    Image Slot3_L;
    [SerializeField]
    public GameObject RewardUI;
    [SerializeField]
    GameObject[] RewardTable;

    RewardItem[] reward= new RewardItem[3];


    string item;
    string name;
    int level;
    int type;
	private void Awake()
	{
        Slot1 = RewardUI.transform.Find("Reward/Choice1/Image").gameObject.GetComponent<Image>();
        Slot2 = RewardUI.transform.Find("Reward/Choice2/Image").gameObject.GetComponent<Image>();
        Slot2_L = RewardUI.transform.Find("Reward/Choice2/Image/level").gameObject.GetComponent<Image>();
        Slot3 = RewardUI.transform.Find("Reward/Choice3/Image").gameObject.GetComponent<Image>();
        Slot3_L = RewardUI.transform.Find("Reward/Choice2/Image/level").gameObject.GetComponent<Image>();
        reward[0] = new RewardItem();
        reward[1] = new RewardItem();
        reward[2] = new RewardItem();
        
        
    }


	public void SetRewardTable() 
    {
        SetSlot1();
        SetSlot2();
        SetSlot3();
    }


    public void SelectReward(int index) 
    {
        //reward 오브젝트 생성
        GameObject rw = new GameObject();
        switch (index) {
            case 1:
                if (reward[0].level == 1)
                {
                    rw = Instantiate(RewardTable[8], this.transform);
                }
                else
                {
                    rw = Instantiate(RewardTable[9], this.transform);
                }
                break;
            case 2:
                {
                    rw = Instantiate(RewardTable[reward[1].name_index], this.transform);
                    rw.GetComponent<Turret>().Level = reward[1].level;
                    rw.GetComponent<Turret>().Type = reward[1].type;
                }
                break;
            case 3:
                if (reward[2].item == "turret")
                {
                    rw = Instantiate(RewardTable[reward[2].name_index], this.transform);
                }
                else if (reward[2].item == "trans") {
                    rw = Instantiate(RewardTable[10], this.transform);
                }
                break;
        }
        GameObject.Find("Item").GetComponent<Inventory>().AddItem(rw);
        int wave = GameObject.Find("MeteorManager").GetComponent<MeteorManager>().wave;
        GameObject.Find("Alert").gameObject.GetComponent<AlertUI>().SetText("Wave - " +wave);
        GameObject.Find("Alert").gameObject.GetComponent<AlertUI>().SetUI();
        RewardUI.SetActive(false);
    }

    void SetSlot1() {
        reward[0].item = "levup";
        int upgrade = Random.Range(0, 10);
        if (upgrade < 8) {
            //1level 상승권
            //reward = Instantiate();
            Slot1.sprite = Resources.Load("6.Sprite/levup1", typeof(Sprite)) as Sprite;
            reward[0].level = 1;
        } else {
			//2level 상승권
			//reward = Instantiate();
			Slot1.sprite = Resources.Load("6.Sprite/levup2", typeof(Sprite)) as Sprite;
            reward[0].level = 2;
        }
    }
    void SetSlot2() {
        reward[1].item = "turret";
        int level = Random.Range(0, 100);
        if (level < 96)
        {   //level1 터렛을 지급
            reward[1].level = 1;
            Slot2_L.sprite = Resources.Load("6.Sprite/level1", typeof(Sprite)) as Sprite;
            float type = Random.Range(0, 100);
            if (type < 37.5)
            {
                int rgb = Random.Range(0, 3);
                switch (rgb) 
                { 
                    case 0:
                        Slot2.color = new Color(1f, 0, 0);
                        reward[1].type = 1;
                        //r속성
                        break;
                    case 1:
                        Slot2.color = new Color(0, 1f, 0);
                        reward[1].type = 2;
                        //g속성
                        break;
                    case 2:
                        Slot2.color = new Color(0, 0, 1f);
                        reward[1].type = 3;
                        //b속성
                        break;
                }
            } 
            else 
            {
                Slot2.color = new Color(1f, 1f, 1f);
                reward[1].type = 0;
            }
        }
        else 
        {
            reward[1].level = 2;
            reward[1].type = 0;
            // lever2 터렛 기본 속성 지급
            Slot2_L.sprite = Resources.Load("6.Sprite/level2", typeof(Sprite)) as Sprite;
        }

        int tower = Random.Range(0, 8);
        switch (tower)
        {
            case 0://t_circle
                reward[1].name = "t_circle";
                break;
            case 1://t_cross
                reward[1].name = "t_cross";
                break;
            case 2://t_crosstar
                reward[1].name = "t_crosstar";
                break;
            case 3://t_penta
                reward[1].name = "t_penta";
                break;
            case 4://t_re-tri
                reward[1].name = "t_re-tri";
                break;
            case 5://t_rect
                reward[1].name = "t_rect";
                break;
            case 6://t_rhombus
                reward[1].name = "t_rhombus";
                break;
            case 7://t_tri
                reward[1].name = "t_tri";
                break;
        }
        reward[1].name_index = tower;
        Slot2.sprite = Resources.Load("6.Sprite/"+reward[1].name, typeof(Sprite)) as Sprite;
    }
    void SetSlot3() 
    {
        int item = Random.Range(0, 10);
        if (item < 6)
        {
            reward[2].item = "trans";
            Slot3.sprite = Resources.Load("6.Sprite/trans", typeof(Sprite)) as Sprite;
            //속성변경 3가지중 하나
            int rgb = Random.Range(0, 3);
            switch (rgb)
            {
                case 0:
                    Slot3.color = new Color(1f, 0, 0);
                    reward[2].type = 1;
                    //r속성
                    break;
                case 1:
                    Slot3.color = new Color(0, 1f, 0);
                    reward[2].type = 2;
                    //g속성
                    break;
                case 2:
                    Slot3.color = new Color(0, 0, 1f);
                    reward[2].type = 3;
                    //b속성
                    break;
            }
        }
        else 
        {
            reward[2].item = "turret";
            //타워 1단계 노말지급
            int tower = Random.Range(0, 8);
            switch (tower) {
                case 0://t_circle
                    reward[2].name = "t_circle";
                    break;
                case 1://t_cross
                    reward[2].name = "t_cross";
                    break;
                case 2://t_crosstar
                    reward[2].name = "t_crosstar";
                    break;
                case 3://t_penta
                    reward[2].name = "t_penta";
                    break;
                case 4://t_re-tri
                    reward[2].name = "t_re-tri";
                    break;
                case 5://t_rect
                    reward[2].name = "t_rect";
                    break;
                case 6://t_rhombus
                    reward[2].name = "t_rhombus";
                    break;
                case 7://t_tri
                    reward[2].name = "t_tri";
                    break;
            }
            Slot3.sprite = Resources.Load("6.Sprite/" + reward[2].name, typeof(Sprite)) as Sprite;
            Slot3.color = new Color(1f, 1f, 1f);
            reward[2].name_index = tower;
            reward[2].level = 1;
            reward[2].type = 0;
        }
    }
}
