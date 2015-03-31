using UnityEngine;
using System.Collections;

public enum CameraTypes { Follow = 1, Camera2D, Orbit, Dios, Puntos, Cinema, Targetting }

public class TP_Camera : MonoBehaviour
{
    public static TP_Camera Instance;
    public Transform[] points = new Transform[5];
    private int currentPointIndex;
    private Transform currentPoint;

    public Transform targettingPoint;
    public float targettingSmooth;
    public float rotateSmooth;
    private bool isResetingCamera;

    private bool cameraPosChanged;
    private int rotationDir;
    public GameObject objetivo;
    public float velX, velY;
    public float velOrbitX, velOrbitY;
	public float distOrbitYMin,distOrbitYMax;
    public float smoothX, smoothY;
    float angleRotated = 0;

    float x, y;
    Vector3 offset;

    public float distancia;
    float distanciaMin, distanciaMax;

    public bool godMode;
    public CameraTypes modoCamara = CameraTypes.Follow;
    private bool fixedLookAt = false;
    private Transform lookAtObject;

    void Awake()
    {
        Instance = this;
    }


    // Use this for initialization
    void Start()
    {
        x = transform.eulerAngles.x;
        godMode = false;
        cameraPosChanged = false;
        isResetingCamera = false;
    }

    void Update()
    {
        if (modoCamara != CameraTypes.Dios) godMode = false;
		if (modoCamara != CameraTypes.Cinema)SplineWalker.Instance.enabled = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        switch (modoCamara)
        {
            case CameraTypes.Follow:

                x += Input.GetAxis("Horizontal") * velX * distancia * Time.deltaTime;
                //y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                //y = ClampAngle(y, yMinLimit, yMaxLimit);
                y = 14f; // cambio manual de la inclinación

                Quaternion rotation = Quaternion.Euler(y, x, 0); //rotación por defecto

                //distancia = Mathf.Clamp(distancia - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

                //recentro la camara detrás del personaje
                if (isResetingCamera)
                {
                    x = objetivo.transform.localEulerAngles.y;
                    /*if (Vector3.Angle(objetivo.transform.forward, transform.forward) < 5f)*/ isResetingCamera = false;
                }

                Vector3 negDistance = new Vector3(0.0f, 1.0f, -distancia);
                Vector3 position = rotation * negDistance + objetivo.transform.position;

                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, smoothX * Time.deltaTime);
                transform.position = Vector3.Slerp(transform.position, position, 5 * Time.deltaTime);

                //TESTING
                offset = Camera.main.transform.position - objetivo.transform.position;
                break;

            case CameraTypes.Camera2D:

                y = 14f; // cambio manual de la inclinación

                rotation = Quaternion.Euler(y, x, 0);

                //DOESN'T WORK AS EXPECTED
                //if (rotationDir == -1) x = objetivo.transform.localEulerAngles.y + 90;
                //else if (rotationDir == 1) x = objetivo.transform.localEulerAngles.y - 90;

                //distancia = Mathf.Clamp(distancia - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

                negDistance = new Vector3(0.0f, 0.0f, -distancia);
                position = rotation * negDistance + objetivo.transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, smoothX * Time.deltaTime);
                transform.position = Vector3.Slerp(transform.position, position, 5 * Time.deltaTime);

                break;

            case CameraTypes.Orbit:

                x += Input.GetAxis("Mouse X") * velOrbitX * distancia * Time.deltaTime;
                //Debug.Log("Valor de X: " + x + " Rotacion Skyler en X: " + objetivo.transform.localEulerAngles.y);
                y -= Input.GetAxis("Mouse Y") * velOrbitY * distancia * Time.deltaTime;
                y = ClampAngle(y, distOrbitYMin, distOrbitYMax);
                //y = 20f; // cambio manual de la inclinación

                rotation = Quaternion.Euler(y, x, 0);

                //distancia = Mathf.Clamp(distancia - Input.GetAxis("Mouse ScrollWheel") * 5, distOrbitYMin, distOrbitYMax);

                negDistance = new Vector3(0.0f, 0.0f, -distancia);
                position = rotation * negDistance + objetivo.transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, smoothX * Time.deltaTime);
                transform.position = Vector3.Slerp(transform.position, position, 5 * Time.deltaTime);
                break;

            case CameraTypes.Dios:

                godMode = true;
                Vector3 angles = transform.localEulerAngles;
                float temp = angles.x;
                angles.x = 0f;
                transform.rotation = Quaternion.Euler(angles);
                transform.Translate(Input.GetAxisRaw("Horizontal") * 10f * Time.deltaTime, 0f, Input.GetAxisRaw("Vertical") * 10f * Time.deltaTime);
				if(Input.GetKey(KeyCode.Space))transform.Translate(Vector3.up * 5f * Time.deltaTime);
				if(Input.GetKey(KeyCode.LeftShift))transform.Translate(-Vector3.up * 5f * Time.deltaTime);
                angles.x += temp;

                angleRotated += Input.GetAxisRaw("Mouse Y") * 90f * Time.deltaTime;
                angleRotated = Mathf.Clamp(angleRotated, -60, 30);

                if (angleRotated < 30 && angleRotated > -60) angles.x += Input.GetAxisRaw("Mouse Y") * 90f * Time.deltaTime;
                angles.y += Input.GetAxisRaw("Mouse X") * 60f * Time.deltaTime;
                angles.z = 0f;

                transform.rotation = Quaternion.Euler(angles);

                break;

            case CameraTypes.Puntos:

                if (fixedLookAt) this.transform.LookAt(lookAtObject);
                //Punto de visualización en el mapa
                if (Input.GetKeyUp(KeyCode.KeypadPlus))
                {
                    cameraPosChanged = true;
                    if (currentPointIndex == 4) currentPointIndex = 0;
                    else ++currentPointIndex;
                }
                else if (Input.GetKeyUp(KeyCode.KeypadMinus))
                {
                    cameraPosChanged = true;
                    if (currentPointIndex == 0) currentPointIndex = 4;
                    else --currentPointIndex;
                }

                //actualizo posición de cámara
                switch (cameraPosChanged)
                {
                    case true:
                        if (Vector3.Distance(transform.position,points[currentPointIndex].position) < 0.5f)cameraPosChanged = false;
                        this.transform.position = Vector3.Slerp(transform.position,points[currentPointIndex].position,2*Time.deltaTime);
                        this.transform.rotation = Quaternion.Slerp(transform.rotation, points[currentPointIndex].rotation, 2* Time.deltaTime);
                        break;
                    case false: break;
                }
                break;

            case CameraTypes.Cinema:

                //codigo de movimiento de cámara aqui
			Debug.Log (Vector3.Distance(transform.position,SplineWalker.Instance.spline.GetPoint(0f)));
				if (Vector3.Distance(transform.position,SplineWalker.Instance.spline.GetPoint(0f)) > 0.5f){
					transform.position = Vector3.Slerp(transform.position,SplineWalker.Instance.spline.GetPoint(0f), 2f * Time.deltaTime);
				} else if(!SplineWalker.Instance.enabled){
					SplineWalker.Instance.progress = 0f;
					SplineWalker.Instance.enabled = true;
				}
                break;

            case CameraTypes.Targetting:

                transform.position = Vector3.Slerp(transform.position, targettingPoint.position, targettingSmooth * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, targettingPoint.rotation, rotateSmooth * Time.deltaTime);
				x = objetivo.transform.localEulerAngles.y;
                break;

        }
    }

    public void SetMode(CameraTypes mode){
        modoCamara = mode;
    }

    public void ActiveCamera2D(int direction)
    {
        modoCamara = CameraTypes.Camera2D;
        rotationDir = direction;
    }

    public void LookAtObject(Transform position, bool enable)
    {
        if (enable)
        {
            fixedLookAt = true;
            lookAtObject = position;
        }
        else
            fixedLookAt = false;
    }

	public CameraTypes GetMode()
	{
		return modoCamara;
	}

    public void ResetCameraPosition()
    {
        isResetingCamera = !isResetingCamera;
    }

	private float ClampAngle (float angle, float min, float max) {
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp(angle, min, max);
	}
}
