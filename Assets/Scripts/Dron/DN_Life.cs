using UnityEngine;
using System.Collections;

public class DN_Life : MonoBehaviour {


	public int life;
	public Texture[] text;
	// Use this for initialization
	void Start () {
		//life = TP_Status.Instance.GetVida();
	}
	
	// Update is called once per frame
	void Update () {
		life = TP_Status.Instance.GetVida();
		if(Input.GetKey("0")){

			life -= 2;
			Debug.Log("Vida: " + life);

		}
		if(life <= 0) transform.GetComponent<Renderer>().material.mainTexture = text[0];
		else if(life > 5 && life <= 6) transform.GetComponent<Renderer>().material.mainTexture = text[1];
		else if(life > 6 && life <= 12) transform.GetComponent<Renderer>().material.mainTexture = text[2];
		else if(life > 12 && life <= 18) transform.GetComponent<Renderer>().material.mainTexture = text[3];
		else if(life > 18 && life <= 24) transform.GetComponent<Renderer>().material.mainTexture = text[4];
		else if(life > 24 && life <= 30) transform.GetComponent<Renderer>().material.mainTexture = text[5];
		else if(life > 30 && life <= 36) transform.GetComponent<Renderer>().material.mainTexture = text[6];
		else if(life > 36 && life <= 42) transform.GetComponent<Renderer>().material.mainTexture = text[7];
		else if(life > 42 && life <= 48) transform.GetComponent<Renderer>().material.mainTexture = text[8];
		else if(life > 48 && life <= 54) transform.GetComponent<Renderer>().material.mainTexture = text[9];
		else if(life > 54 && life <= 60) transform.GetComponent<Renderer>().material.mainTexture = text[10];
		else if(life > 60 && life <= 66) transform.GetComponent<Renderer>().material.mainTexture = text[11];
		else if(life > 66 && life <= 72) transform.GetComponent<Renderer>().material.mainTexture = text[12];
		else if(life > 72 && life <= 78) transform.GetComponent<Renderer>().material.mainTexture = text[13];
		else if(life > 78 && life <= 84) transform.GetComponent<Renderer>().material.mainTexture = text[14];
		else if(life > 84 && life <= 100) transform.GetComponent<Renderer>().material.mainTexture = text[15];

		if(life <= 0) life = 100;
	
	}
}
