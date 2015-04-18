using UnityEngine;
using System.Collections;

public class TP_Controller : MonoBehaviour {


    //ATTRIBUTES

    //PUBLIC
    public static TP_Controller Instance;

    public Vector3 lAnalogDirection;
    public Vector3 rAnalogDirection;
    public float deadZone;
	public int groundType;
    public CharacterController controlador;
	//public GameObject footprint;
	//public GameObject[] footprintpool;
	//public int defaultPoolAmount = 6;
	//public float ratefootprint = 1.0f;
	//private int Poolindex = 0;
	//private float nextfootprint = 0.0f;
    private CameraTypes lastMode;

    void Awake()
    {
		Instance = this;
		//Application.targetFrameRate = 60;
		//creamos el pool de objetos.

	/*	footprintpool = new GameObject[defaultPoolAmount];

		for (int i=0; i<defaultPoolAmount; ++i) {
			footprintpool [i] = Instantiate (footprint, transform.position, transform.rotation) as GameObject;
		}*/
	}	
		// Use this for initialization
	void Start()
    {
	    lAnalogDirection.y = rAnalogDirection.y = 0f;
        controlador = GetComponent<CharacterController>();

	}
	
	// Update is called once per frame
	void Update()
    {
        InputCamara();

        if (TP_Status.Instance.IsControllable())
        {
            InputMovimiento(); //input y actualización del movimiento
            InputHabilidades();
        }else
            InputInterface();

        if (!TP_Camera.Instance.godMode) TP_Motor.Instance.MovePlayer();//actualizamos el movimiento del player
	}

    //Get input from controller
    void InputMovimiento()
    {
        //Joysticks Input
        Debug.DrawRay(transform.position, lAnalogDirection * 10f, Color.green);

        if (Input.GetAxisRaw("Horizontal") > deadZone || Input.GetAxisRaw("Horizontal") < -deadZone)
            lAnalogDirection.x = Input.GetAxisRaw("Horizontal");
        else
            lAnalogDirection.x = 0f;

        if (Input.GetAxisRaw("Vertical") > deadZone || Input.GetAxisRaw("Vertical") < -deadZone)
            lAnalogDirection.z = Input.GetAxisRaw("Vertical");
        else
            lAnalogDirection.z = 0f;

        //Update Status
        if (lAnalogDirection == Vector3.zero) TP_Status.Instance.SetMoving(false,0f);
        else
        {
            lAnalogDirection = lAnalogDirection.normalized;
            
            TP_Status.Instance.SetMoving(true, lAnalogDirection.sqrMagnitude);
            //Debug.Log("SQR: " + lAnalogDirection.sqrMagnitude);
        }

        TP_Motor.Instance.moveVector = lAnalogDirection;

		//if (((lAnalogDirection.z != 0) || (lAnalogDirection.x != 0)) && ( !TP_Status.Instance.IsJumping())) {DrawFootPrints();}

    }

    void InputInterface()
    {
        if (Input.GetKeyDown(KeyCode.Space))//boton para quitar el mensaje
        {
            //Activar siguiente mensaje, pero si no hya, desactivar el modo mensaje.
            DronController.Instance.ActiveNextMessage();
        }
    }

    void InputHabilidades()
    {
		//Jumping Input
        if (Input.GetButtonDown("Jump")) Jump();

        //PROVISIONAL HASTA QUE DEFINAMOS LOS BOTONES
        if (Input.GetKeyDown(KeyCode.F)) TP_Skills.Instance.ActivateSkill(SkillTypes.tractionBeam);
        else if (Input.GetKeyDown(KeyCode.G)) TP_Skills.Instance.ActivateSkill(SkillTypes.liftingHook);
        else if (Input.GetKeyDown(KeyCode.B)) TP_Skills.Instance.ActivateSkill(SkillTypes.blackHole);
		else if (Input.GetKeyUp (KeyCode.V)) {TP_Skills.Instance.ActivateSkill (SkillTypes.push);}
		else if (Input.GetKey (KeyCode.V)) {TP_Skills.Instance.ActivateSkill (SkillTypes.pushcharge);}


		/*Event e = Event.current;
		if (e.isKey && e.type == EventType.KeyDown) {
			switch (e.keyCode) {
			case KeyCode.F:
				TP_Skills.Instance.ActivateSkill(SkillTypes.tractionBeam);
				break;
			case KeyCode.G:
				TP_Skills.Instance.ActivateSkill(SkillTypes.liftingHook);
				break;    
			case KeyCode.B:
				TP_Skills.Instance.ActivateSkill(SkillTypes.blackHole);
				break;  
			case KeyCode.V:
				TP_Skills.Instance.ActivateSkill (SkillTypes.pushcharge);
				break; 
			default:
				break;
			}
		} else 
		if (e.isKey && e.type == EventType.keyUp) {
			switch (e.keyCode) {
				case KeyCode.V:
					TP_Skills.Instance.ActivateSkill (SkillTypes.push);
				break; 
			}
		}*/

        //Apuntar
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (TP_Status.Instance.IsTargetting())
            {
                case true:
                    TP_Status.Instance.SetTargetting(false);
                    TP_Camera.Instance.modoCamara = lastMode;
                    break;
                case false:
                    TP_Status.Instance.SetTargetting(true);
                    lastMode = TP_Camera.Instance.modoCamara;
                    TP_Camera.Instance.modoCamara = CameraTypes.Targetting;
                    break;
            }
        }
    }

    void InputCamara()
    {
        //Designar boton para alternar entre modos de cámara { LIBRE, SEGUIMIENTO, DIOS, PUNTOS POR PANTALLA}
        rAnalogDirection = new Vector3(Input.GetAxisRaw("Mouse X"), 0f, Input.GetAxisRaw("Mouse Y"));
        Debug.DrawRay(transform.position, rAnalogDirection * 10f, Color.blue);

        if (Input.GetKeyUp(KeyCode.C) && TP_Camera.Instance.modoCamara != CameraTypes.Targetting)
        {
            if (TP_Camera.Instance.modoCamara == CameraTypes.Cinema)
                TP_Camera.Instance.modoCamara = CameraTypes.Follow;
            else
                TP_Camera.Instance.modoCamara += 1;

            Debug.Log("El modo de Camara es: " + TP_Camera.Instance.modoCamara);
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            TP_Camera.Instance.ResetCameraPosition();
        }
        
    }

    void Jump()
    {
        TP_Motor.Instance.Jump();
    }

/*	void OnControllerColliderHit (ControllerColliderHit hit){
		//Debug.Log(hit.gameObject.tag);
		if (hit.gameObject.tag == "ground"){
			groundType = 1;
		}
		else if (hit.gameObject.tag == "water"){
			groundType = 2;
		}
		else groundType = 3; 
	}
	void DrawFootPrints()
	{
		if (Time.time > nextfootprint)
		{
			nextfootprint = Time.time + ratefootprint-Random.Range(0.05f,0.1f);
			if (groundType == 1) {
				footprintpool[Poolindex].SetActiveRecursively(false);
				footprintpool[Poolindex].transform.position=transform.position;
				footprintpool[Poolindex].transform.rotation= transform.rotation;
				footprintpool[Poolindex].SetActiveRecursively(true);
				++Poolindex;
				if (Poolindex >= defaultPoolAmount) Poolindex=0;
			}
		}
	}*/


}
