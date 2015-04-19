using UnityEngine;
using System.Collections;

public class trilerosmandragora : MonoBehaviour {

	public Trileros tscript;
	public int idmandrago=1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		if (tscript) {
			if (tscript._canchoose) {
				if (tscript.idmandrago == idmandrago) {
					//hemos encontrado la muestra
					tscript.GameEnd ();
				} else {
					// no era el bueno, repetimos
					tscript.Nuevaronda ();
				}
			}
		}
	}

}
