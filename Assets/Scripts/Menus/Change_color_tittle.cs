using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Change_color_tittle : MonoBehaviour {

	public Color colorStart = Color.red;
	public Color colorEnd = Color.green;
	public float duration = 1.0F;
	Text instruction;

	// Use this for initialization
	void Start () {
		instruction = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
		float lerp = Mathf.PingPong(Time.time, duration) / duration;
		instruction.material.color = Color.Lerp(colorStart, colorEnd, lerp);


	}
}
