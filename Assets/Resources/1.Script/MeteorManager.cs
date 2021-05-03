using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//class Setting {
//    int wave;
//    int meteor;
//    string type;
//    int health;

//    public Setting() { }
//    public void SetProperty(int wave, int meteor, string type, int health) 
//    {
//        this.wave = wave;
//        this.meteor = meteor;
//        this.type = type;
//        this.health = health;
//    }
//}

public class MeteorManager : MonoBehaviour
{

	public GameManager gameManager;
	public int Total;

	public int placed;

	public bool EndTurn;

	public bool gameOver;
	//R, G, B, W, S
	public GameObject[] meteorPrefab;
	public GameObject spawn;

	int exPosition;
	public int wave { get; set; }
	int meteorCount;
	int meteorHealth;
	public int meteorDestroied;

	public int meteorStack;
	string[] types =
		{ "ww", "wwww", "wwwwRR","wwRwwwRR",
			"wwRwwRwwRw", "wwwwRRwwRRwS",
"wwwwRRwwRRwSww",
"wwwwRRwwRRwSwwww",
"wwwwRRwwRRwSwwwwRR",
"wwwwRRwwRRwSwwRwwwRR",
"wwwwRRwwRRwSwwRwwRwwRw",
"wwwwRRwwRRwSwwwwRRwwRRwS",
"wwwwRRwwRRwSwwwwRRwwRRwSww",
"wwwwRRwwRRwSwwwwRRwwRRwSwwww",
"wwwwRRwwRRwSwwwwRRwwRRwSwwwwRR",
"wwwwRRwwRRwSwwwwRRwwRRwSwwRwwwRR",
"wwwwRRwwRRwSwwwwRRwwRRwSwwRwwRwwRw",
"wwwwRRwwRRwSwwwwRRwwRRwSwwwwRRwwRRwS",
"wwwwRRwwRRwSwwwwRRwwRRwSwwwwRRwwRRwSww",
"wwwwRRwwRRwSwwwwRRwwRRwSwwwwRRwwRRwSwwww",
"wwwwRRwwRRwSwwwwRRwwRRwSwwwwRRwwRRwSwwwwRR",
"wwwwRRwwRRwSwwwwRRwwRRwSwwwwRRwwRRwSwwRwwwRR",
"wwwwRRwwRRwSwwwwRRwwRRwSwwwwRRwwRRwSwwRwwRwwRw",
"wwwwRRwwRRwSwwwwRRwwRRwSwwwwRRwwRRwSwwwwRRwwRRwS",
"wwwwwwwwwwRRRRRRRRRRRRSSSSSSRRRRRRRRRRRRwwwwwwwwww"
};
	//          터렛
	//      w, r, g, b
	//    w
	// 운 r
	// 석 g
	//    b
	//    s
	//계수 결정 후 자료형도 결정
	float[,] dmgCoef = { {1,   1,   1,    1 },
						 {1,   1,   0.5f, 1.5f },
						 {1,   1.5f,1,    0.5f },
						 {1,   0.5f,1.5f, 1 },
						 {0.5f,1,   1,    1 }
	};
	int waveIndex;
	//현재 맵에 있는 운석
	List<GameObject> MeteorList;

	//대기중인 운석
	Queue<GameObject> MeteorPool;


	//공격하는 터렛들의 정보
	List<AttackClass> attackClasses;


	//Setting[] settings;

	// Start is called before the first frame update
	void Start()
	{
		waveIndex = 0;
		meteorStack = 2;
		wave = 1;
		meteorCount = 2;
		meteorHealth = 21;
		MeteorList = new List<GameObject>();
		gameOver = false;
		exPosition = Random.Range(0, 8);
	}

	// Update is called once per frame
	void Update()
	{
		if (waveIndex > 0 && meteorStack <= meteorDestroied)
		{
			//  Debug.Log("m_stack : " + meteorStack);
			// Debug.Log("m_count : " + meteorCount);
			// Debug.Log("m_destroied : " + meteorDestroied);
			GameObject.Find("GameManager").GetComponent<GameManager>().victory = true;
			waveIndex = 0;
		}
		//Debug.Log("waveIndex : " + waveIndex);
		//Debug.Log("ListCount : " + MeteorList.Count);
	}
	/// <summary>
	/// 운석의 속성, 위치, 랜덤설정후 배치
	/// </summary>
	void MeteorPlace(int wave)
	{
		int position;

		//프리팹 인스턴스화 슈팅게임 참고
		//meteorpool에 대기
		if (waveIndex < meteorCount)
		{

			position = Random.Range(0, 8);

			if (exPosition == position)
			{
				position = (position + 1) % 8;
			}
			exPosition = position;
			//Debug.Log("rand : " + position);
			GameObject meteor = new GameObject();
			switch (types[wave - 1][waveIndex])
			{
				case 'w':
					meteor = Instantiate(meteorPrefab[0], spawn.transform);
					meteor.GetComponent<Meteor>().type = 0;
					break;
				case 'R':
					int rgb = Random.Range(1, 4);
					switch (rgb)
					{
						case 1:
							meteor = Instantiate(meteorPrefab[1], spawn.transform);
							break;
						case 2:
							meteor = Instantiate(meteorPrefab[2], spawn.transform);
							break;
						case 3:
							meteor = Instantiate(meteorPrefab[3], spawn.transform);
							break;
					}
					meteor.GetComponent<Meteor>().type = rgb;
					break;
				case 'S':
					meteor = Instantiate(meteorPrefab[4], spawn.transform);
					meteor.GetComponent<Meteor>().type = 4;
					break;
			}
			meteor.transform.position = new Vector2((float)-3.55 + position * (1.015f), 4.55f);
			meteor.GetComponent<Meteor>().axisX = position;
			meteor.GetComponent<Meteor>().health = meteorHealth;

			MeteorList.Add(meteor);
			//waveIndex++;
		}
	}

	/// <summary>
	/// 운석이 이동
	/// </summary>
	public void NextTurn()
	{


		//데미지를 받는다.
		calcDmg();


		if (meteorDestroied >= meteorStack)
			return;
		//운석이 다음 위치로 이동한다.(운석 테이블에 있는 모든 운석들의 move를 호출)
		StartCoroutine(MoveMeteor());

		//새로 배치할 운석이 있다면 배치




	}
	public void NextWave()
	{
		waveIndex = 0;
		wave++;
		meteorCount = wave * 2;
		meteorStack += meteorCount;
		meteorHealth = 12 + 9 * wave;
		//meteorDestroied = 0;
		MeteorList.Clear();
		if (wave > 25)
			SceneManager.LoadScene("StartScene");
	}

	public void calcDmg()
	{
		if (MeteorList.Count == 0 || attackClasses.Count == 0)
		{
			return;
		}
		foreach (AttackClass a in attackClasses)
		{
			a.game.GetComponent<Turret>().Attack();
			for (int i = 0; i < 7; i++)
			{//가로
				for (int j = 0; j < 8; j++)
				{//세로
					if (a.Area[i, j] == 1)
					{//딜범위면 실행

						Vector3 pos = this.transform.position + new Vector3(-1.015f * i, 1.015f * j, 0);
						foreach (GameObject g in MeteorList)
						{
							if (g == null)
								continue;
							int x;
							int y;
							Meteor m = g.GetComponent<Meteor>();
							x = m.axisX;
							y = m.axisY;
							if (j == x && i == y)
							{
								m.health -= (int)(dmgCoef[m.type, a.Type] * a.Damage);
								m.CalcDamage();
							}
						}
					}
				}
			}
		}
	}

	public void setAttackClass(List<AttackClass> attackClasses)
	{
		this.attackClasses = attackClasses;
		NextTurn();
	}

	IEnumerator MoveMeteor()
	{
		yield return new WaitForSeconds(1.7f);
		foreach (GameObject g in MeteorList)
		{
			if (g == null)
				continue;
			g.GetComponent<Meteor>().Move();
		}
		foreach (GameObject g in MeteorList)
		{
			if (g == null)
				continue;
			if (g.GetComponent<Meteor>().gameOver == true)
			{
				gameOver = true;

			}
		}
		MeteorPlace(wave);
		waveIndex++;
	}
}
