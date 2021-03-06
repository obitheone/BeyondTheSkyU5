//using UnityEditor;
using UnityEngine;
using System.Collections;

public enum SkillTypes { noSkill, tractionBeam, liftingHook, blackHole, push,pushcharge }

public class TP_Skills : MonoBehaviour {

    //ATTRIBUTES

    //PUBLIC
    public static TP_Skills Instance;
    private GameObject _object;
    public GameObject _beamobject;
    private GameObject _tractorobject;
    private Vector3 _hitpoint;
    private bool _beam = false;
    private bool _tractor = false;
	private bool _push = false;
	private bool _pushcharge = false;
    private float _lateral = 0f;
    private float _horizontal = 0f;
	private float _energypush =1f;

    public float speed = 0.02f;
    public GameObject player;
    public GameObject righthand;
    public GameObject lefthand;
	public ParticleEmitter lefthandpushconcentration;
	public GameObject blackhole;
	public GameObject forcepush;

	public GameObject sphericfx;
	public GameObject sphericringfx;
	public GameObject electrical;
	public GameObject electrical1;
	public GameObject electrical2;
	public GameObject electrical3;

    private SkillTypes enabledSkill = SkillTypes.noSkill; 

	private SK_TractorBeam _currentObjectBeamScript;
	private SK_LiftingHook _currentObjectHookScript;
	private FX_LightningBolt _currentLightningBoltScriptR;
	private FX_LightningBolt _currentLightningBoltScriptL;
    //PRIVATE



    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start ()
    {
		_beam = false;
		_currentLightningBoltScriptR = righthand.GetComponent("FX_LightningBolt") as FX_LightningBolt;
		_currentLightningBoltScriptL = lefthand.GetComponent("FX_LightningBolt") as FX_LightningBolt;
		//_currentObjectHookScript = player.GetComponent("SK_LiftingHook") as SK_LiftingHook;
	}

	void LateUpdate()
	{
		//esto es a eliminar.
		if (Input.GetKey(KeyCode.Keypad6)) 
		{
			_lateral=_lateral+speed;
		}
		if (Input.GetKey(KeyCode.Keypad4)) 
		{
			_lateral=_lateral-speed;
		}
		if (Input.GetKey(KeyCode.Keypad8)) 
		{
			_horizontal=_horizontal+speed;
		}
		if (Input.GetKey(KeyCode.Keypad5)) 
		{
			_horizontal=_horizontal-speed;
		}
	}
	void FixedUpdate()	
	{
		if (_beam) {
			if (_beamobject){
				_currentObjectBeamScript.offset_lateral = _lateral;
				_currentObjectBeamScript.offset_horizontal = _horizontal;
				_currentLightningBoltScriptR.target = _beamobject.transform.position;

				if (Input.GetMouseButton (0)) { 
					_currentObjectBeamScript.energy = _currentObjectBeamScript.energy + 20 * Time.deltaTime;
				}

				if (Input.GetMouseButtonUp (1)) { 
					if (_currentObjectBeamScript.energy < 0)
						_currentObjectBeamScript.energy = 0;
					throwobject (_currentObjectBeamScript.energy);
				}

			}
			else
			{
				deactivatetractorbeam();
			}
		} else 
			if (_push) 
			{
				push();
			}
			if (_pushcharge) 
			{
				pushcharge(10.0f);
			}
	}
	// Update is called once per frame
	void Update () {
	
		switch (enabledSkill)
		{
        case SkillTypes.tractionBeam:

			if (!_beam){
				if (Input.GetMouseButtonDown (0)) {
					activatetractorbeam();
					}
			}
			else
			{
				deactivatetractorbeam();
			}
			break;
		
		case SkillTypes.liftingHook:
			if (!_tractor){
				if (Input.GetMouseButtonDown (0)) {
					activateliftingHook();
				}
			}
			else
			{
				deactivateliftingHook();
			}
			break;
		case SkillTypes.push:
			_push=true;
			_pushcharge=false;
			break;
		case SkillTypes.pushcharge:
			_pushcharge=true;
			_push=false;
			break;
		case SkillTypes.blackHole:
			if (Input.GetMouseButtonDown (0)) {activateblackhole();}
			break;
		default: break;
		}	
	}

	void OnGUI()
	{
		string temp;
		if (_beam)
        {			
			temp=_currentObjectBeamScript.energy.ToString();
			GUI.Label (new Rect (10, 10, 150, 20), "Push Force: "+temp);
		}
		//if (_pushcharge)
		//{			
			temp=_energypush.ToString();
			GUI.Label (new Rect (10, 10, 150, 20), "Push Force: "+temp);
		//}
	}

    public void ActivateSkill(SkillTypes skill)
    {


		//if (enabledSkill == SkillTypes.noSkill) enabledSkill = skill;

		//else if ((enabledSkill==SkillTypes.pushcharge)&&(skill==SkillTypes.push)) enabledSkill = skill;
		enabledSkill = skill;
		//Debug.Log ("actuvada Skill:"+enabledSkill);
    }

	public void deactivatetractorbeam()
	{

			_currentObjectBeamScript.enabled= false;
			_currentLightningBoltScriptR.enabled=false;
			_beam=false;
			_lateral=0;
			_horizontal=0;

			//activamos la burbuja
			sphericfx.GetComponent<ParticleEmitter>().emit = false;
			sphericringfx.GetComponent<ParticleEmitter>().emit = false;
			sphericfx.GetComponent<ParticleRenderer> ().enabled=false;
			sphericringfx.GetComponent<ParticleRenderer>().enabled=false;
			electrical.GetComponent<ParticleSystem> ().enableEmission = false;
			electrical1.GetComponent<ParticleSystem> ().enableEmission = false;
			electrical2.GetComponent<ParticleSystem> ().enableEmission = false;
			electrical3.GetComponent<ParticleSystem> ().enableEmission = false;


			//lo atachamos al player
			sphericfx.transform.parent=righthand.transform;
			sphericfx.transform.localPosition=Vector3.zero;
			sphericfx.transform.localRotation=Quaternion.identity;

			enabledSkill = SkillTypes.noSkill;
	}

	private void activatetractorbeam()
	{
		//Iluminate (Color.black,"Beamer");

		Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 1000,(1<<LayerMask.NameToLayer("Beamer"))))
		{
					_beamobject=hit.collider.gameObject;

					if (!_beamobject.GetComponent("SK_TractorBeam")) _beamobject.AddComponent<SK_TractorBeam>();

					_currentObjectBeamScript= _beamobject.GetComponent("SK_TractorBeam") as SK_TractorBeam;

					_currentObjectBeamScript.enabled= true;
					_currentLightningBoltScriptR.enabled=true;
					
					_horizontal = 1f;
					_lateral = 0.7f;
						
					_currentObjectBeamScript.player=player;
					_currentLightningBoltScriptR.target=hit.collider.gameObject.transform.position;
					_beam=true;

					//activamos la burbuja
					sphericfx.GetComponent<ParticleEmitter>().emit = true;
					sphericringfx.GetComponent<ParticleEmitter>().emit = true;
					sphericfx.GetComponent<ParticleRenderer> ().enabled=true;
					sphericringfx.GetComponent<ParticleRenderer>().enabled=true;
					electrical.GetComponent<ParticleSystem> ().enableEmission = true;
					electrical1.GetComponent<ParticleSystem> ().enableEmission = true;
					electrical2.GetComponent<ParticleSystem> ().enableEmission = true;
					electrical3.GetComponent<ParticleSystem> ().enableEmission = true;

					//lo atachamos al objeto
					sphericfx.transform.parent=_beamobject.transform;
					sphericfx.transform.localPosition=Vector3.zero;
					sphericfx.transform.localRotation=Quaternion.identity;

		}
		enabledSkill = SkillTypes.noSkill;
	}

	private void activateliftingHook()
	{
		//Iluminate (Color.black,"Tractor");
		Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 1000,(1<<LayerMask.NameToLayer("Tractor"))))
		{
			//if (hit.collider.gameObject.tag == "Tractor"){

				_tractorobject=hit.collider.gameObject;
					
				if (!player.GetComponent("SK_LiftingHook")) 
				{
					player.AddComponent<SK_LiftingHook>();
					_currentObjectHookScript= player.GetComponent("SK_LiftingHook") as SK_LiftingHook;
				}

				_currentObjectHookScript.enabled= true;
				_currentLightningBoltScriptL.enabled=true;

				_currentObjectHookScript.hitpoint=hit.point;
				_hitpoint=hit.point;
				_tractor=true;
				_currentLightningBoltScriptL.target=hit.point;
			//}
		}
		enabledSkill = SkillTypes.noSkill;
	}
	private void deactivateliftingHook()
	{
		_currentObjectHookScript.enabled= false;
		_currentLightningBoltScriptL.enabled=false;

		_tractor=false;
		enabledSkill = SkillTypes.noSkill;
	}

	private void throwobject(float energy)
	{
		//miramos si el raton en este momento hace hit sobre algun objeto, si es asi lanzamos en esa direccion
		Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 1000))
		{

			Vector3 direction=(hit.collider.transform.position-_beamobject.transform.position);
			direction.Normalize();

			if (_beamobject.transform!=hit.transform)
			{
				_beamobject.GetComponent<Rigidbody>().AddForce(direction*energy,ForceMode.Impulse);
			}
		}
		else//si no hace hit marcamos una distancia de 100 y apuntamos alli lanzando en esa direccion.
		{
			_beamobject.GetComponent<Rigidbody>().AddForce(ray.direction.normalized * energy);
		}
		deactivatetractorbeam();
	}

	private void pushcharge(float energy)
	{
		_energypush += energy;
		lefthandpushconcentration.emit = true;
		lefthandpushconcentration.enabled = true;
		lefthandpushconcentration.minEnergy = (_energypush - 50) / 10;

		//enabledSkill = SkillTypes.noSkill;
		//_pushcharge = false;
	}
	private void push()
	{
		if (_energypush > 300.0f) {
			GameObject newProjectile = Instantiate (forcepush, righthand.transform.position, righthand.transform.rotation) as GameObject;
			//newProjectile.GetComponent<Rigidbody>().velocity = transform.TransformDirection(player.transform.forward*15);
			newProjectile.GetComponent<SK_Push> ().direction = -player.transform.forward;
		} else {

			Vector3 explosionPos = new Vector3 (righthand.transform.position.x, righthand.transform.position.y, righthand.transform.position.z);

			Collider[] colliders = Physics.OverlapSphere (explosionPos, _energypush);
			foreach (Collider hit in colliders) {
				if ((hit) && (hit.GetComponent<Rigidbody> ())) {


					Vector3 directionToTarget = player.transform.position - hit.gameObject.transform.position;
					float angle = Vector3.Angle (player.transform.forward, directionToTarget);
					float distance = directionToTarget.magnitude;
				
					if ((Mathf.Abs (angle) > 130) && distance < 10) {
						//Debug.Log(Mathf.Abs(angle));
						//hit.GetComponent<Rigidbody>().AddExplosionForce (_energypush, explosionPos, _energypush / 10, 3);
						hit.GetComponent<Rigidbody> ().AddForce (-1 * (_energypush * (10 - distance)) * directionToTarget.normalized);

					}
				}
			}
		}


		enabledSkill = SkillTypes.noSkill;
		_push = false;
		_pushcharge = false;
		_energypush = 1.0f;
		lefthandpushconcentration.ClearParticles ();
		lefthandpushconcentration.emit = false;
		lefthandpushconcentration.enabled = false;
	}
	private void activateblackhole()
	{
		Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 1000))
		{
			GameObject newProjectile = Instantiate( blackhole, hit.point, hit.transform.rotation ) as GameObject;
			//FX_Blackhole script= newProjectile.GetComponent("FX_Blackhole") as FX_Blackhole;
			//script.power = _energypush / 5;
			//script.radius = _energypush / 10;
			//script.time = 1;
		}
		enabledSkill = SkillTypes.noSkill;
	}

}
	