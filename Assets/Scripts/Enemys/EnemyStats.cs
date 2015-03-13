using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

	public int startingHealth = 100; 	//vida inicial
	public float currentHealth;			//vida actual
	public float sinkSpeed = 0.5f; 		//velocidad en que se hunde al morir
	public AudioClip deathClip;			//sonido al morir
	public AudioClip damageClip;		//sonido al morir

	AudioSource enemyAudio;
	CapsuleCollider capsuleCollider;
	public bool isDead;
	bool isSinking;

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
	}

	void Death ()
	{
		isDead = true;
		
		capsuleCollider.isTrigger = true;
		//anim.SetTrigger ("Dead"); //activamos la animacion de la muerte.
		enemyAudio.clip = deathClip;
		enemyAudio.Play();
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
		if (hit.gameObject.tag == "Beamer") 
		{
			Rigidbody body = hit.collider.attachedRigidbody;
			TakeDamage(body.sleepVelocity*200,hit.transform.position);	
		}
	}
}
