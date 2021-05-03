using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertUI : MonoBehaviour
{
	public float waitTime;
	public GameObject alertUI;

	private void Start()
	{
		SetUI();
		Debug.Log("Start");
	}
	public void SetUI() {
		Debug.Log("setUI");
		if (alertUI.gameObject.activeSelf == true)
		{
			alertUI.gameObject.SetActive(false);
			Debug.Log("true");
		}
		else {
			alertUI.gameObject.SetActive(true);
			StartCoroutine(Wait(waitTime));
			
			Debug.Log("false");
		}
	}
	public void GameOverUI() {
		Debug.Log("setUI");
		if (alertUI.gameObject.activeSelf == true)
		{
			alertUI.gameObject.SetActive(false);
			Debug.Log("true");
		}
		else
		{
			alertUI.gameObject.SetActive(true);
			StartCoroutine(Wait(5f));

			Debug.Log("false");
		}
	}
	public void SetText(string text) {
		alertUI.transform.Find("WaveText").GetComponent<Text>().text = text;
	}
	public void SetGameOverText() {
		int wave = GameObject.Find("MeteorManager").GetComponent<MeteorManager>().wave;
		alertUI.transform.Find("HighScore").GetComponent<Text>().text="Wave - " + wave;
		alertUI.transform.Find("HighScore").gameObject.SetActive(true);
	}
	IEnumerator Wait(float time) 
	{
		yield return new WaitForSeconds(time);
		alertUI.gameObject.SetActive(false);
	}
}
