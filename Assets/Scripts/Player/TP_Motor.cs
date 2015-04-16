using UnityEngine;
using System.Collections;

public class TP_Motor : MonoBehaviour {

    //ATTRIBUTES

    //PUBLIC
    public static TP_Motor Instance;
    public Vector3 moveVector;
    Vector3 targetDir;
    public float walkSpeed;
    public float runSpeed;
    public float jumpSpeed;
    public float reJumpSpeed;
    public float dampSpeed;
    public float rotSpeed;
    public float gravity;
    public float reJumpDelay;



    //PRIVATE
    private float verticalMovement;
    private float moveSpeed;

    void Awake()
    {
        Instance = this;
        moveVector = targetDir = Vector3.zero;
        moveSpeed = walkSpeed;
    }

	// Use this for initialization
	void Start () {
        verticalMovement = 0f;
	}

    private void UpdateMovement() //REVISAR
    {
        Transform camara = TP_Camera.Instance.transform;

        //coordenadas de cámara a wolrd space
        Vector3 forward = camara.TransformDirection(Vector3.forward);
        forward.y = 0f;
        forward = forward.normalized;

        //calcular forward y right en funcion de coordenadas de camara
        Vector3 right = new Vector3(forward.z, 0f, -forward.x);

        moveSpeed = walkSpeed * (1 - moveVector.sqrMagnitude) + runSpeed * moveVector.sqrMagnitude;

        //obtener movimiento del joystick y calcular dirección de movimiento
        targetDir = moveVector.x * right + moveVector.z * forward;
        //targetDir = targetDir.normalized;

        //aplicar velocidad de movimiento
        Debug.Log(targetDir.sqrMagnitude);
        targetDir *= moveSpeed;

        if (!TP_Status.Instance.IsControllable()) targetDir = Vector3.zero;

        //aplicar gravedad si no está en el suelo
        //if (!TP_Controller.Instance.controlador.isGrounded) verticalMovement -= gravity * Time.deltaTime;
        ApplyGravity();

        //añadir velocidad vertical
        targetDir.y = verticalMovement;

        //actualizar movimiento del controlador
        TP_Controller.Instance.controlador.Move(targetDir * Time.deltaTime);

        if (TP_Controller.Instance.controlador.isGrounded)
        {
            TP_Status.Instance.SetJumping(false);
            TP_Status.Instance.SetReJumping(false);
            targetDir.y = 0;
        }

    }

    void FacePlayerToMovementDir()
    {
        if (moveVector.sqrMagnitude > 0f && TP_Status.Instance.IsControllable()) //moveVector != Vector3.zero)
        {
            targetDir.y = 0f;
            Quaternion faceDir = Quaternion.LookRotation(targetDir);
			TP_Controller.Instance.transform.rotation = Quaternion.Slerp(TP_Controller.Instance.transform.rotation, faceDir, dampSpeed * Time.deltaTime);
        }
        else
        {
            Quaternion faceDir = Quaternion.LookRotation(TP_Controller.Instance.transform.forward);
            TP_Controller.Instance.transform.rotation = Quaternion.Slerp(TP_Controller.Instance.transform.rotation, faceDir, dampSpeed * Time.deltaTime);
        }
    }

    public void MovePlayer()
    {
        UpdateMovement();
        FacePlayerToMovementDir();
    }

    public void Jump()
    {
        if (TP_Controller.Instance.controlador.isGrounded && !TP_Status.Instance.IsJumping())
        {
            TP_Status.Instance.SetJumping(true);
            verticalMovement = jumpSpeed;
        }
        else if (!TP_Status.Instance.IsReJumping() && verticalMovement < reJumpDelay)
        {
            TP_Status.Instance.SetReJumping(true);
            verticalMovement = reJumpSpeed;
        }
        else verticalMovement = 0f;
    }

    void ApplyGravity()
    {
        if (!TP_Controller.Instance.controlador.isGrounded)
            verticalMovement -= gravity * Time.deltaTime;
    }

    public void SetSpeed(bool state)//cambio la velocidad si está corriendo o andando
    {
        if (state)//andando
        {
            moveSpeed = walkSpeed;
        }
        else moveSpeed = runSpeed;
    }
}
