using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Meteor : MonoBehaviour
{
    MeteorManager meteorManager;
	public int health;
    public int type;
    bool turn;
	public bool gameOver;
    //운석 테이블 내에서의 좌표
    public int axisX { get; set; }
	public int axisY { get; set; }
	GameObject healthText;
	TextMeshPro t;
	public GameObject Effect;
	public GameObject MetorImg;

	public GameObject[] TurretEffect = new GameObject[8];



	
    // Start is called before the first frame update
    void Awake()
    {
		t = transform.Find("MeteorImage/health").GetComponent<TextMeshPro>();
		axisY = 0;
		gameOver = false;
		health = 200;
    }

    // Update is called once per frame
    void Update()
    {
		t.text = health.ToString();
    }
	/// <summary>
	/// 총알 부딪혔을때
	/// </summary>
	/// <param name="collision"></param>
	//public void OnCollisionEnter(Collision collision)
	//{
	//    if(collision.gameObject)
	//    {
	//        Debug.Log("총알 부딪힘");
	//        Die();
	//    }
	//}
	/// <summary>
	/// 터렛필드에 들어갔을때
	/// </summary>
	/// <param name="other"></param>
	//public void OnTriggerEnter(Collider other)
	//{
	//    if(other)
	//    {
	//        Debug.Log("운석이 터렛필드에 부딪힘");
	//    }
	//}

	private void OnDestroy()
	{
		GameObject.Find("MeteorManager").GetComponent<MeteorManager>().meteorDestroied++;
		//터지는 이펙트
	}
	void Die()
    {
		MetorImg.SetActive(false);
		Effect.SetActive(true);
		
		StartCoroutine(DestroyMeteor());
    }

	
    public void Move()
    {
		//실제 보이는 맵에서의 이동(axisX*어떤 값, axisY*어떤 값)
		axisY++;
        this.gameObject.transform.position = new Vector2(
			-3.55f+axisX*1.015f, 4.55f+axisY*(-1.015f));
		if (axisY >= 7) {
			Die();
			gameOver = true;
		}
    }
	//void SetAxisX(int x) 
	//{
	//	axisX = x;
	//}

	//void SetAxisY(int y)
	//{
	//	axisY = y;
	//}
	void ChangeHealth() {
	
	}
	public void CalcDamage() 
	{
		//들어오는 데미지의 타입, 데미지를 고려하여 남은 hp를 계산
		if (health <= 0)
		{
			Die();
		}
	}

	IEnumerator DestroyMeteor() {
		yield return new WaitForSeconds(1.5f);
		Destroy(this.gameObject);
	}
}
