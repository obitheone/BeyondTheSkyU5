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
	public GameObject footprint;
	public float ratefootprint = 0.0f;
	private float nextfootprint = 0.0f;

    private Skills lastMode;

    void Awake()
    {
        Instance = this;
        //Application.targetFrameRate = 60;
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
        InputMovimiento(); //input y actualización del movimiento
        InputHabilidades();
        InputCamara();

        //actualizamos el movimiento del player
        if (!TP_Camera.Instance.godMode) TP_Motor.Instance.MovePlayer();
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

        TP_Motor.Instance.moveVector = lAnalogDirection;

		if (((lAnalogDirection.z != 0) || (lAnalogDirection.x != 0)) && ( !TP_Status.Instance.IsJumping())) {DrawFootPrints();}

    }

    void InputHabilidades()
    {
        //Jumping Input
        if (Input.GetButtonDown("Jump")) Jump();

        //PROVISIONAL HASTA QUE DEFINAMOS LOS BOTONES
        if (Input.GetKeyDown(KeyCode.F)) TP_Skills.Instance.ActivateSkill(SkillTypes.tractionBeam);
        if (Input.GetKeyDown(KeyCode.G)) TP_Skills.Instance.ActivateSkill(SkillTypes.liftingHook);
        if (Input.GetKeyDown(KeyCode.B)) TP_Skills.Instance.ActivateSkill(SkillTypes.blackHole);
		if (Input.GetKeyDown(KeyCode.V)) TP_Skills.Instance.ActivateSkill(SkillTypes.pushcharge);
		if (Input.GetKeyUp (KeyCode.V)) {TP_Skills.Instance.ActivateSkill (SkillTypes.push);}

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
                    TP_Camera.Instance.modoCamara = Skills.Targetting;
                    break;
            }
        }
    }

    void InputCamara()
    {
        //Designar boton para alternar entre modos de cámara { LIBRE, SEGUIMIENTO, DIOS, PUNTOS POR PANTALLA}
        rAnalogDirection = new Vector3(Input.GetAxisRaw("Mouse X"), 0f, Input.GetAxisRaw("Mouse Y"));
        Debug.DrawRay(transform.position, rAnalogDirection * 10f, Color.blue);

        if (Input.GetKeyUp(KeyCode.C) && TP_Camera.Instance.modoCamara != Skills.Targetting)
        {
            if (TP_Camera.Instance.modoCamara == Skills.Cinema)
                TP_Camera.Instance.modoCamara = Skills.Follow;
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

	void OnControllerColliderHit (ControllerColliderHit hit){
		Debug.Log(hit.gameObject.tag);
		if (hit.gameObject.tag == "ground"){
			groundType = 1;
		}
		else if (hit.gameObject.tag == "water"){
			groundType = 2;
		}
		else groundType = 3; //volando, sin colision
	}
	void DrawFootPrints()
	{
		if (Time.time > nextfootprint)
		{
			nextfootprint = Time.time + ratefootprint-Random.Range(0.01f,0.1f);
			if (groundType == 1) {
				GameObject newFootPrint = Instantiate (footprint,  transform.position, transform.rotation) as GameObject;
			}
		}
	}


}
