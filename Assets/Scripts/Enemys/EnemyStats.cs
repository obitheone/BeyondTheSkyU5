using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

	public int startingHealth = 100; 	//vida inicial
	public float currentHealth;			//vida actual
	public float sinkSpeed = 0.5f; 		//velocidad en que se hunde al morir
	public AudioClip deathClip;			//sonido al morir
	public AudioClip damageClip;		//sonido al morir
	public float StunTime = 3;

	private float Stuntimer=0;
	private AudioSource enemyAudio;
	private CapsuleCollider capsuleCollider;
	public bool isDead;
	public bool isStun;
	private bool isSinking;


	//Animator anim; //animacion de muerte
	//ParticleSystem hitParticles; //efecto al golpear al enemigo.
	
	
	void Awake ()
	{
		//anim = GetComponent <Animator> ();
		enemyAudio = GetComponent <AudioSource> ();
		//hitParticles = GetComponentInChildren <ParticleSystem> ();
		capsuleCollider = GetComponent <CapsuleCollider> ();
		currentHealth = startingHealth;
	}
		
	void Update ()
	{
		if(isSinking)
		{
			transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
		}
		if (isStun)
		{
			if (Stuntimer>StunTime) {isStun=false;Stuntimer=0;}
			else Stuntimer+=Time.deltaTime;
		}
	}

	public void TakeDamage (float amount, Vector3 hitPoint)
	{
		if(isDead)
			return;
		enemyAudio.clip = damageClip;
		enemyAudio.Play ();
		
		currentHealth -= amount;
		
		//hitParticles.transform.position = hitPoint;
		//hitParticles.Play();
		
		if(currentHealth <= 0)
		{
			Death ();
			StartSinking ();
		}
		else Stun ();
	}

	void Death ()
	{
		isDead = true;
		
		capsuleCollider.isTrigger = true;
		//anim.SetTrigger ("Dead"); //activamos la animacion de la muerte.
		enemyAudio.clip = deathClip;
		enemyAudio.Play();
	}

	void Stun()
	{
		isStun=true;
		Stuntimer=0;
	}

	public void StartSinking ()
	{
		GetComponent <NavMeshAgent> ().enabled = false;
		GetComponent <Rigidbody> ().isKinematic = true;
		isSinking = true;
		Destroy (gameObject, 2f);
	}

	void  OnCollisionEnter (Collision hit)
	{
		Vector3 velocity=hit.gameObject.GetComponent <Rigidbody> ().velocity;
		if ((hit.gameObject.tag == "Beamer") && (!Vector3.Equals(velocity,Vector3.zero)))
		{
			//cambiamos su estad a aturdido.
			/////
			Rigidbody body = hit.collider.attachedRigidbody;
			TakeDamage(body.sleepVelocity*200,hit.transform.position);	
		}
	}
}
