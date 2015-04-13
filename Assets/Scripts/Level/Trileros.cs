using UnityEngine;
using System.Collections;

public class Trileros : MonoBehaviour {

	public GameObject Mandrago1;
	public GameObject Mandrago2;
	public GameObject Mandrago3;
	public GameObject Muestra;
	public int idmandrago=0;
	public bool _canchoose=false;
	public bool start=false;
	public ParticleSystem P1;
	public ParticleSystem P2;
	public ParticleSystem P3;


	// Use this for initialization
	private SplineWalker _Sp1;
	private SplineWalker _Sp2;
	private SplineWalker _Sp3;

	private bool _StartGame=false;

	void Start () {
		_Sp1=Mandrago1.GetComponent("SplineWalker") as SplineWalker;
		_Sp2=Mandrago2.GetComponent("SplineWalker") as SplineWalker;
		_Sp3=Mandrago3.GetComponent("SplineWalker") as SplineWalker;
		P1.enableEmission = false;
		P2.enableEmission = false;
		P3.enableEmission = false;
		_StartGame=false;
	}

	void Update()
	{
		//boton temporal para activar el juego sin el trigger, esto se quita no sirva para nada , es de testing
		if (start) {
			start = false;
			GameStart ();
		}
		//////////////////////////////////////
	
		if (_StartGame) {
			if (_Sp1.progress == 1) P1.enableEmission = false;
			else P1.enableEmission = true;
			if (_Sp2.progress == 1) P2.enableEmission = false;
			else P2.enableEmission = true;
			if (_Sp3.progress == 1) P3.enableEmission = false;
			else P3.enableEmission = true;
			if ((_Sp1.progress == 1) && (_Sp2.progress == 1) && (_Sp3.progress == 1)) {
				
				

				_canchoose = true;
			} else {
				//ponemos la muestra aleatoriamente solo miro una.. pa que mirarlas todas :)
				if ((_Sp1.progress > 0.15) && (_Sp1.progress < 0.35))
					cambiarmuestra ();
			}
		}

	}
	void OnTriggerEnter(Collider other) {
		GameStart ();
	}

	void GameStart()
	{
		_StartGame = true;
		//ponemos la camara para que mire a las mandragoras

		// animacion de las mandragoras

		// juego de trileros!
		Nuevaronda ();
	}

	public void Nuevaronda()
	{
		_Sp1.progress = 0;
		_Sp2.progress = 0;
		_Sp3.progress = 0;
	}

	void cambiarmuestra()
	{
		idmandrago=Random.Range(1,4);

		switch (idmandrago) {
		case 1:
			// attach la muestra a la mandrago 1
			Muestra.transform.parent=Mandrago1.transform;
			// de momento la pones encima a saco;
			Muestra.transform.localPosition=Vector3.zero;
			Muestra.transform.localRotation=Quaternion.identity;
			Muestra.transform.Translate(new Vector3(0,1,0));
			break;
		case 2:
			// attach la muestra a la mandrago 1
			Muestra.transform.parent=Mandrago2.transform;
			// de momento la pones encima a saco;
			Muestra.transform.localPosition=Vector3.zero;
			Muestra.transform.localRotation=Quaternion.identity;
			Muestra.transform.Translate(new Vector3(0,1,0));
			
			break;
		case 3:
			// attach la muestra a la mandrago 1
			Muestra.transform.parent=Mandrago3.transform;
			// de momento la pones encima a saco;
			Muestra.transform.localPosition=Vector3.zero;
			Muestra.transform.localRotation=Quaternion.identity;
			Muestra.transform.Translate(new Vector3(0,1,0));
			
			break;
		default: break;
			
		}
	}
	public void GameEnd()
	{
		_StartGame = false;
		//Destruimos el Trigger que activa el juego
		Muestra.transform.Translate(new Vector3(0,2,0));
		Destroy (this.gameObject);
	}
	
}
