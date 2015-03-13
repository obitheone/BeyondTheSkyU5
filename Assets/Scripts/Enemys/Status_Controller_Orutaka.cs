using UnityEngine;
using System.Collections;

public class Status_Controller_Orutaka : MonoBehaviour {

	private const int ATTACK=0;
	private const int LOOK=1;
	private const int PATROL=2;
	private const int DEAD=3;
	
	private const int CHARGE_ATTACK=0;
	private bool is_attaking = false;
	
	//variables de configuracion del enemigo
	public float look_time=3f;		//tiempo que vamos a observarlo
	
	//variables de evaluacion de la toma de decisiones
	public float player_distace=100f;  //distancia del jugador
	public float lookingTime=0f;	//tiempo que hace que lo estamos observando
	public bool player_view=false;	//vemos al player?
	//public bool is_attack_finish=false;
	//variables de salida
	public int state;
	public int attack_type;
	private EnemyStats ES;
	// Use this for initialization
	void Start () {
		state = PATROL;
		player_distace = 100;
		lookingTime = 0;
		player_view = false;
		is_attaking = false;
		ES = GetComponent<EnemyStats> () as EnemyStats;
	}
	
	// Update is called once per frame
	void Update () {

		if (!ES.isDead) {
						if (player_view) { //vemos al player
								if (lookingTime < look_time) {
										state = LOOK;
								} else {

										state = ATTACK;
										attack_type = CHARGE_ATTACK;
										is_attaking = true;
								}
						} else { //no vemos al player
								state = PATROL;

						}
				} 
		else {
			state = DEAD;
				}
	}
}
