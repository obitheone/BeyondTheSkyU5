using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//public enum TriggerType {Camera = 1, HUD, Player, Particle, MultiOptions}
public enum OptionType { DisableMovement = 1, FixCamera, ChangeCameraType, Camera2D, DronMessage, KillPlayer}

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
        public GameObject messagePos;
        public CameraTypes CameraType;
        public int Side2D;
        public int[] messagesToShow;

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

    public Option[] opciones;
    private bool insideTrigger = false;

    void Start()
    {
    }

    void Update()
    {
    }

    void RevertChanges() //revierte los cambios realizados al entrar en un trigger
    {
        for (int i = 0; i < opciones.Length; ++i)
        {
            //PENDIENTE DE HACER
        }
    }

    void OnTriggerEnter(Collider col)
    {
        switch (col.gameObject.tag)
        {
            case "Player":
                for (int i = 0; i < opciones.Length; ++i)
                {
                    if (opciones[i].enable)//Si la opción está habilitada
                    {
                        switch (opciones[i].option)
                        {
                            case OptionType.FixCamera:
                                if (opciones[i].cameraPos != null)
                                {
                                    TP_Camera.Instance.transform.position = opciones[i].cameraPos.transform.position;
                                    TP_Camera.Instance.transform.rotation = opciones[i].cameraPos.transform.rotation;
                                    TP_Camera.Instance.SetMode(CameraTypes.Puntos);
                                    if (opciones[i].lookAtObject != null) TP_Camera.Instance.LookAtObject(opciones[i].lookAtObject.transform, true);
                                }
                                else
                                {
                                    Debug.LogError("Camera position must be set in Inspector");
                                    UnityEditor.EditorApplication.isPlaying = false;
                                }
                                break;
                            case OptionType.ChangeCameraType:
                                if (opciones[i].CameraType != 0)
                                {
                                    TP_Camera.Instance.modoCamara = opciones[i].CameraType;
                                }
                                else
                                {
                                    Debug.LogError("CameraType must be defined on Inspector!");
                                    UnityEditor.EditorApplication.isPlaying = false;
                                }
                                break;
                            case OptionType.DronMessage:
                                if (opciones[i].messagesToShow.Length != 0)
                                {
                                    DronController.Instance.ActivateState(DronStates.Talking);
                                    TP_Camera.Instance.modoCamara = CameraTypes.MessageReading;
                                    TP_Status.Instance.SetControllable(false);
                                    DronController.Instance.MessagesToShow(opciones[i].messagesToShow.Length, opciones[i].messagesToShow);//REVISAR
                                }
                                else
                                {
                                    Debug.LogError("The messages number must be greater than 0!!!");
                                    UnityEditor.EditorApplication.isPlaying = false;
                                }
                                break;
                            case OptionType.Camera2D:
                                TP_Camera.Instance.ActiveCamera2D(opciones[i].Side2D);
                                break;
                            case OptionType.KillPlayer:
                                Debug.Log("Soy: " + col.tag);
                                TP_Status.Instance.SubsVida(100);
                                break;
                            case OptionType.DisableMovement:
                                TP_Status.Instance.SetControllable(false);
                                break;
                        }
                    }
                }
                break;
        }
    }
    
    void OnTriggerExit(Collider col) //SI HAY OPCIONES REVERSIBLES AL SALIR DEL TRIGGER, LAS REVIERTO
    {
        switch(col.tag)
        { 
            case "Player":
                for (int i = 0; i < opciones.Length; ++i)
                {
                    if (opciones[i].enable)//Si la opción está habilitada
                    {
                        if (opciones[i].option == OptionType.DronMessage) opciones[i].enable = false;
                    }
                }
                break;
        }
    }



	//Cambio del Icono segun el tipo de trigger.
	void OnDrawGizmos(){
		Gizmos.DrawIcon (this.transform.position, "triggerIcon.tiff", true);
	}


}
