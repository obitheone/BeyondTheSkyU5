using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void LoadLevel(string name){
		Application.LoadLevel (name);
	}

	public void LoadLevel(int index){
		Application.LoadLevel (index);
	}

	public void ExitGame(){
		Application.Quit ();
	}
}
