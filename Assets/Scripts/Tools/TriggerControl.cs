using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//public enum TriggerType {Camera = 1, HUD, Player, Particle, MultiOptions}
public enum OptionType { DisableMovement = 1, FixCamera, ChangeCameraType, Camera2D, DronMessage, KillPlayer, PlayAnimation}

public class TriggerControl : MonoBehaviour {

    //public TriggerType type;

    [System.Serializable]
    public class Option
    {
        public OptionType option;
        public bool enable;
        public GameObject playerPos;
        public GameObject cameraPos;
        public GameObject lookAtObject;
        public CameraTypes CameraType;
        public int Side2D;
        public int[] messagesToShow;
        public GameObject objectToAnim;
        public string animationName;

        public Option() { }

        /*public Option(Option opt)
        {
            // TODO: Complete member initialization
            this.option = opt.option;
            this.enable = opt.enable;
            this.playerPos = opt.playerPos;
            this.cameraPos = opt.cameraPos;
            this.CameraType = opt.CameraType;
            this.Side2D = opt.Side2D;
            this.axis = opt.axis;
            this.message = opt.message;
            this.reversibleChange = opt.reversibleChange;
        }*/
    }

    public Option[] enterOptions;
    public Option[] exitOptions;
    private bool insideTrigger = false;

    void Start()
    {
    }

    void Update()
    {
    }

    private void ProcessOptions(Collider col, bool exit)
    {
        switch (col.tag)
        {
            case "Player":
                for (int i = 0; i < enterOptions.Length; ++i)
                {
                    if (exit)
                    {
                        if (exitOptions[i].enable)//Si la opcion sigue deshabilitada, cambio las opciones, si no, es que no se hace nada
                        {
                            switch (exitOptions[i].option)
                            {
                                case OptionType.FixCamera:
                                    if (exitOptions[i].cameraPos != null)
                                    {
                                        TP_Camera.Instance.transform.position = exitOptions[i].cameraPos.transform.position;
                                        TP_Camera.Instance.transform.rotation = exitOptions[i].cameraPos.transform.rotation;
                                        TP_Camera.Instance.SetMode(CameraTypes.Puntos);
                                        if (enterOptions[i].lookAtObject != null) TP_Camera.Instance.LookAtObject(exitOptions[i].lookAtObject.transform, true);
                                    }
                                    else
                                    {
                                        Debug.LogError("Camera position must be set in Inspector");
                                        UnityEditor.EditorApplication.isPlaying = false;
                                    }
                                    break;
                                case OptionType.ChangeCameraType:
                                    if (exitOptions[i].CameraType != 0)
                                    {
                                        TP_Camera.Instance.modoCamara = exitOptions[i].CameraType;
                                    }
                                    else
                                    {
                                        Debug.LogError("CameraType must be defined on Inspector!");
                                        UnityEditor.EditorApplication.isPlaying = false;
                                    }
                                    break;
                                case OptionType.DronMessage:
                                    if (exitOptions[i].messagesToShow.Length != 0)
                                    {
                                        DronController.Instance.ActivateState(DronStates.Talking);
                                        TP_Camera.Instance.modoCamara = CameraTypes.MessageReading;
                                        TP_Status.Instance.SetControllable(false);
                                        DronController.Instance.MessagesToShow(exitOptions[i].messagesToShow.Length, exitOptions[i].messagesToShow);//REVISAR
                                    }
                                    else
                                    {
                                        Debug.LogError("The messages number must be greater than 0!!!");
                                        UnityEditor.EditorApplication.isPlaying = false;
                                    }
                                    break;
                                case OptionType.Camera2D:
                                    TP_Camera.Instance.ActiveCamera2D(exitOptions[i].Side2D);
                                    break;
                                case OptionType.KillPlayer:
                                    Debug.Log("Soy: " + col.tag);
                                    TP_Status.Instance.SubsVida(100);
                                    break;
                                case OptionType.DisableMovement:
                                    TP_Status.Instance.SetControllable(false);
                                    break;
                                case OptionType.PlayAnimation:
                                    if (exitOptions[i].objectToAnim != null || exitOptions[i].animationName != "")
                                    {
                                        //exitOptions[i].objectToAnim.Play(exitOptions[i].animationName);
                                        exitOptions[i].objectToAnim.GetComponent<Animator>().Play(exitOptions[i].animationName);
                                    }
                                    else
                                    {
                                        Debug.LogError("Animator and animation name must be defined!!!");
                                        UnityEditor.EditorApplication.isPlaying = false;
                                    }
                                    break;

                            }
                        }
                        else
                        { //si la opcion de salida esta disable, desactivo la entrada ( no volverá a activarse )
                            enterOptions[i].enable = false;
                        }
                    }
                    else
                    {
                        if (enterOptions[i].enable)//Si la opción está habilitada
                        {
                            switch (enterOptions[i].option)
                            {
                                case OptionType.FixCamera:
                                    if (enterOptions[i].cameraPos != null)
                                    {
                                        TP_Camera.Instance.transform.position = enterOptions[i].cameraPos.transform.position;
                                        TP_Camera.Instance.transform.rotation = enterOptions[i].cameraPos.transform.rotation;
                                        TP_Camera.Instance.SetMode(CameraTypes.Puntos);
                                        if (enterOptions[i].lookAtObject != null) TP_Camera.Instance.LookAtObject(enterOptions[i].lookAtObject.transform, true);
                                    }
                                    else
                                    {
                                        Debug.LogError("Camera position must be set in Inspector");
                                        UnityEditor.EditorApplication.isPlaying = false;
                                    }
                                    break;
                                case OptionType.ChangeCameraType:
                                    if (enterOptions[i].CameraType != 0)
                                    {
                                        TP_Camera.Instance.modoCamara = enterOptions[i].CameraType;
                                    }
                                    else
                                    {
                                        Debug.LogError("CameraType must be defined on Inspector!");
                                        UnityEditor.EditorApplication.isPlaying = false;
                                    }
                                    break;
                                case OptionType.DronMessage:
                                    if (enterOptions[i].messagesToShow.Length != 0)
                                    {
                                        DronController.Instance.ActivateState(DronStates.Talking);
                                        TP_Camera.Instance.modoCamara = CameraTypes.MessageReading;
                                        TP_Status.Instance.SetControllable(false);
                                        DronController.Instance.MessagesToShow(enterOptions[i].messagesToShow.Length, enterOptions[i].messagesToShow);//REVISAR
                                    }
                                    else
                                    {
                                        Debug.LogError("The messages number must be greater than 0!!!");
                                        UnityEditor.EditorApplication.isPlaying = false;
                                    }
                                    break;
                                case OptionType.Camera2D:
                                    TP_Camera.Instance.ActiveCamera2D(enterOptions[i].Side2D);
                                    break;
                                case OptionType.KillPlayer:
                                    Debug.Log("Soy: " + col.tag);
                                    TP_Status.Instance.SubsVida(100);
                                    break;
                                case OptionType.DisableMovement:
                                    TP_Status.Instance.SetControllable(false);
                                    break;
                                case OptionType.PlayAnimation:
                                    if (enterOptions[i].objectToAnim != null || enterOptions[i].animationName != "")
                                    {
                                        exitOptions[i].objectToAnim.GetComponent<Animator>().Play(exitOptions[i].animationName);
                                    }
                                    else
                                    {
                                        Debug.LogError("Animator and animation name must be defined!!!");
                                        UnityEditor.EditorApplication.isPlaying = false;
                                    }
                                    break;

                            }
                        }
                    }
                }
                break;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        ProcessOptions(col, false);
    }
    
    void OnTriggerExit(Collider col) //SI HAY OPCIONES REVERSIBLES AL SALIR DEL TRIGGER, LAS REVIERTO
    {
        ProcessOptions(col, true);
    }



	//Cambio del Icono segun el tipo de trigger.
	void OnDrawGizmos(){
		Gizmos.DrawIcon (this.transform.position, "triggerIcon.tiff", true);
	}


}
