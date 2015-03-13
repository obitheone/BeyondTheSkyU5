using UnityEngine;
using System.Collections;

public class MenuHelp : MonoBehaviour {

	public GUISkin estilo;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Escape))
						Application.LoadLevel ("Menu_joc");
	}

	void OnGUI(){
		GUISkin temp = GUI.skin;
		GUI.skin = estilo;
		GUI.Label (new Rect (Screen.width / 2 - 400, Screen.height / 2 - 250, 800, 550),
		    "TECLADO \n\n" +
						"Arrows     - Movimiento \n" +
						"Space      - Salto / Doble Salto \n" +
						"Key C      - Cambio de camara \n" +
						"Key X      - Reposicionamiento de camara \n" +
						"Key F      - Activar habilidad 1 \n" +
						"Key G      - Activar habilidad 2 \n" +
						"Key B      - Activar habilidad 3 \n\n" +
		    "RATON \n\n" +
		           		"Boton IZQ  - Coger / Soltar objetos \n" +
		           		"Boton DER  - Lanzar objetos \n" +
		           		"Movimiento - Mover la camara \n");
		GUI.skin = temp;
	}
}
