using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public float scroll_speed = 2.5f;
	float n_ypos;
	bool credits_on = true;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.Escape)|| transform.position.y >= 46) Application.LoadLevel("Menu_joc");

		else{

			n_ypos = Mathf.Lerp(transform.position.y, transform.position.y + scroll_speed,Time.deltaTime);

			transform.position = new Vector3(transform.position.x,n_ypos,transform.position.z);
		}
	}
}
