using UnityEngine;
using System.Collections;

public class newdbg : MonoBehaviour {

	bool change = false;
	bool change2 = false;
	bool change3 = false;
	bool all = false;
	bool first = true;
	bool first2 = true;
	public string[] tags;
	private Material[] oldMaterials;
	private Material[] oldMaterials2;
	public Material mat;
	public Material mat2;
	private GameObject[] allobj;
	private GameObject[] allobj2;

	void Start () {

	}

	void Update () {

		string input = Input.inputString; 
		switch (input) {

		case "1":

			foreach(string str in tags){
				foreach(GameObject gameObj in GameObject.FindGameObjectsWithTag(str))
				{
					if(!change)	gameObj.GetComponent<Renderer>().enabled = false;
					else if(change)	gameObj.GetComponent<Renderer>().enabled = true;
									//gameObj.renderer.material.color = Color.red;

				}
			}
			change= !change;
			break;
		
		case "2":
			if (first) {

				allobj = GameObject.FindGameObjectsWithTag("Beamer");
				oldMaterials = new Material[allobj.Length];

				for (var i = 0; i < allobj.Length; ++i) {

					oldMaterials[i] = allobj[i].GetComponent<Renderer>().material;
					Debug.Log(oldMaterials[i]);
				}
				first = false;
			}

			for(var i = 0; i < allobj.Length; i++)
				{
				if(!change2){

					allobj[i].GetComponent<Renderer>().material = mat;
					allobj[i].GetComponent<Renderer>().enabled = true;
				}
				else if(change2){

					allobj[i].GetComponent<Renderer>().material = oldMaterials[i];
				
				}

					
				}
			change2 = !change2;
			break;
		case "3":
			if (first2) {
				
				allobj2 = GameObject.FindGameObjectsWithTag("Tractor");
				oldMaterials2 = new Material[allobj2.Length];
				
				for (var i = 0; i < allobj2.Length; ++i) {
					
					oldMaterials2[i] = allobj2[i].GetComponent<Renderer>().material;
					Debug.Log(oldMaterials2[i]);
				}
				first2 = false;
			}
			
			for(var i = 0; i < allobj2.Length; i++)
			{
				if(!change3){
					
					allobj2[i].GetComponent<Renderer>().material = mat2;
					allobj2[i].GetComponent<Renderer>().enabled = true;
				}
				else if(change3){
					
					allobj2[i].GetComponent<Renderer>().material = oldMaterials2[i];
					
				}
				
				
			}
			change3 = !change3;
			break;
		}
	}
}
