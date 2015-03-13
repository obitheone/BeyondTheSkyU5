using UnityEditor;
using UnityEngine;
using System.Collections;

public enum SkillTypes { noSkill, tractionBeam, liftingHook, blackHole }

public class TP_Skills : MonoBehaviour {

    //ATTRIBUTES

    //PUBLIC
    public static TP_Skills Instance;
    private GameObject _object;
    private GameObject _beamobject;
    private GameObject _tractorobject;
    private Vector3 _hitpoint;
    private bool _beam = false;
    private bool _tractor = false;
    private float _lateral = 0f;
    private float _horizontal = 0f;

    public float speed = 0.02f;
    public GameObject player;
    public GameObject righthand;
    public GameObject lefthand;
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
		_currentObjectHookScript = player.GetComponent("SK_LiftingHook") as SK_LiftingHook;
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
		if (_beam) 
        {
			_currentObjectBeamScript.offset_lateral=_lateral;
			_currentObjectBeamScript.offset_horizontal=_horizontal;
			_currentLightningBoltScriptR.target=_beamobject.transform.position;

			if (Input.GetMouseButton(0)) 
			{ 
				_currentObjectBeamScript.energy=_currentObjectBeamScript.energy+20*Time.deltaTime;
			}

			if (Input.GetMouseButtonUp (1)) 
			{ 
				if (_currentObjectBeamScript.energy<0) _currentObjectBeamScript.energy=0;
				throwobject(_currentObjectBeamScript.energy);
			}
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
		default: break;
		}	
	}

	void OnGUI()
	{
		if (_beam)
        {			
			string temp=_currentObjectBeamScript.energy.ToString();
			GUI.Label (new Rect (10, 10, 150, 20), "Push Force: "+temp);
		}
	}

    public void ActivateSkill(SkillTypes skill)
    {
        enabledSkill = skill;
    }

	private void deactivatetractorbeam()
	{
		_currentObjectBeamScript.enabled= false;
		_currentLightningBoltScriptR.enabled=false;
		_beam=false;
		_lateral=0;
		_horizontal=0;
		enabledSkill = SkillTypes.noSkill;
	}

	private void activatetractorbeam()
	{
		//Iluminate (Color.black,"Beamer");

		Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 1000,(1<<LayerMask.NameToLayer("Beamer"))))
		{
			if (hit.collider.gameObject.tag == "Beamer"){
				//if(hit.collider.gameObject.GetComponent<SK_TractorBeam>() == null)
				//{
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
				//}
			}
		}
		enabledSkill = SkillTypes.noSkill;
	}

	private void activateliftingHook()
	{
		//Iluminate (Color.black,"Tractor");
		Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 1000))
		{
			if (hit.collider.gameObject.tag == "Tractor"){

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
			}
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
		if(Physics.Raycast(ray, out hit, 500))
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
		
}
	