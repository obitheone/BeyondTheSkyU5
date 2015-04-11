using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//public enum TriggerType {Camera = 1, HUD, Player, Particle, MultiOptions}
public enum OptionType { DisableMovement = 1, FixCamera, ChangeCameraType, Camera2D, ShowHUDMessage, KillPlayer}

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
        public Vector3 messagePos;
        public CameraTypes CameraType;
        public int Side2D;
        public GameObject message;
        public bool reversibleChange;

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
    private Option[] opcionesReversibles;
    private bool insideTrigger = false;

    void Start()
    {
        opcionesReversibles = new Option[opciones.Length];
        for (int i = 0; i < opcionesReversibles.Length; ++i)
        {
            opcionesReversibles[i] = new Option();
            opcionesReversibles[i].option = 0;
            opcionesReversibles[i].enable = false;
            opcionesReversibles[i].playerPos = new GameObject();
            opcionesReversibles[i].cameraPos = new GameObject();
            opcionesReversibles[i].lookAtObject = new GameObject();
            opcionesReversibles[i].messagePos = Vector3.zero;
            opcionesReversibles[i].CameraType = 0;
            opcionesReversibles[i].Side2D = 0;
            opcionesReversibles[i].message = new GameObject();
            opcionesReversibles[i].reversibleChange = false;
        }
    }

    void Update()
    {
    }

    void RevertChanges() //revierte los cambios realizados al entrar en un trigger
    {
        for (int i = 0; i < opciones.Length; ++i)
        {
            if (opcionesReversibles[i].reversibleChange)
            {
                switch (opcionesReversibles[i].option)
                {
                    case OptionType.FixCamera:
                        //TP_Camera.Instance.transform.position = opcionesReversibles[i].cameraPos.gameObject.transform.position;
                        //TP_Camera.Instance.transform.rotation = opcionesReversibles[i].cameraPos.transform.rotation;
                        TP_Camera.Instance.modoCamara = opcionesReversibles[i].CameraType;
                        TP_Camera.Instance.LookAtObject(this.transform, false);
                        break;

                    case OptionType.ChangeCameraType:
                        TP_Camera.Instance.modoCamara = opcionesReversibles[i].CameraType;
                        break;
                    case OptionType.Camera2D:
                        TP_Camera.Instance.modoCamara = opcionesReversibles[i].CameraType;
                        break;
                }
            }
        }
    }

    void OnTriggerEnter(Collider col){ //CUANDO ENTRO EN EL TRIGGER EJECUTO LAS OPCIONES SELECCIONADAS
        insideTrigger = true;
        for (int i = 0; i < opciones.Length; ++i)
        {
            if (opciones[i].enable)//Si la opción está habilitada
            {
                switch (opciones[i].option)
                {
                    case OptionType.FixCamera:
                        if (opciones[i].cameraPos != null)
                        {
                            //si la opcion tiene que ser reversible, guardamos el estado actual
                            if (opciones[i].reversibleChange)
                            {
                                opcionesReversibles[i].option = opciones[i].option;
                                opcionesReversibles[i].reversibleChange = true;
                                opcionesReversibles[i].cameraPos.gameObject.transform.position = TP_Camera.Instance.transform.position;
                                opcionesReversibles[i].cameraPos.gameObject.transform.rotation = TP_Camera.Instance.transform.rotation;
                                opcionesReversibles[i].CameraType = TP_Camera.Instance.GetMode();
                            }

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
                            if (opciones[i].reversibleChange)
                            {
                                opcionesReversibles[i].option = opciones[i].option;
                                opcionesReversibles[i].reversibleChange = true;
                                opcionesReversibles[i].CameraType = TP_Camera.Instance.GetMode();
                            }
                            TP_Camera.Instance.modoCamara = opciones[i].CameraType;
                        }
                        else
                        {
                            Debug.LogError("CameraType must be defined on Inspector!");
                            UnityEditor.EditorApplication.isPlaying = false;
                        }
                        break;
                    case OptionType.ShowHUDMessage:
                        if (opciones[i].message != null)
                        {
                            Instantiate(opciones[i].message, opciones[i].messagePos, Quaternion.identity);
                        }
                        else
                        {
                            Debug.LogError("The message prefab must be atached!!");
                            UnityEditor.EditorApplication.isPlaying = false;
                        }
                        break;
                    case OptionType.Camera2D:
                        if (opciones[i].reversibleChange)
                        {
                            opcionesReversibles[i].option = opciones[i].option;
                            opcionesReversibles[i].reversibleChange = true;
                            opcionesReversibles[i].CameraType = TP_Camera.Instance.GetMode();
                        }
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
    }
    
    void OnTriggerExit() //SI HAY OPCIONES REVERSIBLES AL SALIR DEL TRIGGER, LAS REVIERTO
    {
        insideTrigger = false;
        RevertChanges();
    }



	//Cambio del Icono segun el tipo de trigger.
	void OnDrawGizmos(){
		Gizmos.DrawIcon (this.transform.position, "triggerIcon.tiff", true);
	}


}
