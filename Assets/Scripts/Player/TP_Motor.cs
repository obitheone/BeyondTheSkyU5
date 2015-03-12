using UnityEngine;
using System.Collections;

public class TP_Motor : MonoBehaviour {

    //ATTRIBUTES

    //PUBLIC
    public static TP_Motor Instance;
    public Vector3 moveVector;
    Vector3 targetDir;
    public float moveSpeed;
    public float jumpSpeed;
    public float reJumpSpeed;
    public float dampSpeed;
    public float rotSpeed;
    public float gravity;
    public float reJumpDelay;



    //PRIVATE
    private float verticalMovement;

    void Awake()
    {
        Instance = this;
        moveVector = targetDir = Vector3.zero;
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

        //obtener movimiento del joystick y calcular dirección de movimiento
        targetDir = moveVector.x * right + moveVector.z * forward;
        //targetDir = targetDir.normalized;

        //aplicar velocidad de movimiento
        targetDir *= moveSpeed;

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
//            Debug.Log("Salto en UpdateMovement(): " + TP_Status.Instance.IsJumping());
            TP_Status.Instance.SetReJumping(false);
            targetDir.y = 0;
        }

    }

    void FacePlayerToMovementDir()
    {
        if (moveVector != Vector3.zero)
        {
            targetDir.y = 0f;
            Quaternion faceDir = Quaternion.LookRotation(targetDir);
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
            verticalMovement = jumpSpeed;
            TP_Status.Instance.SetJumping(true);
        }
        else if (!TP_Status.Instance.IsReJumping() && verticalMovement < reJumpDelay)
        {
            verticalMovement = reJumpSpeed;
            TP_Status.Instance.SetReJumping(true);
        }
    }

    void ApplyGravity()
    {
        if (!TP_Controller.Instance.controlador.isGrounded)
            verticalMovement -= gravity * Time.deltaTime;
    }


}
