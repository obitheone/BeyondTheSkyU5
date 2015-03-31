using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TriggerType {Camera = 1, HUD, Player, Particle, MultiOptions}
public enum OptionType { DisableMovement = 1, FixedCamera, ChangeCameraType, RotateCamera, ShowHUDMessage }

public class TriggerControl : MonoBehaviour {

    public TriggerType type;

    [System.Serializable]
    public class Option
    {
        public OptionType option;
        public bool enable;
        public GameObject PlayerPos;
        public GameObject CameraPos;
        public CameraTypes CameraType;
        public float degrees;
        public Vector3 axis;
        public GameObject gameobject;
        public bool reversibleChange;

        public Option() { }

        public Option(Option opt)
        {
            // TODO: Complete member initialization
            this.option = opt.option;
            this.enable = opt.enable;
            this.PlayerPos = opt.PlayerPos;
            this.CameraPos = opt.CameraPos;
            this.CameraType = opt.CameraType;
            this.degrees = opt.degrees;
            this.axis = opt.axis;
            this.gameobject = opt.gameobject;
            this.reversibleChange = opt.reversibleChange;
        }
    }

    public Option[] opciones;
    private Option[] opcionesReversibles;

    void Start()
    {
        opcionesReversibles = new Option[opciones.Length];
        for (int i = 0; i < opcionesReversibles.Length; ++i)
        {
            opcionesReversibles[i] = new Option();// opciones[i].option;
            opcionesReversibles[i].option = 0;
            opcionesReversibles[i].enable = false;//opciones[i].enable;
            opcionesReversibles[i].PlayerPos = new GameObject();//opciones[i].PlayerPos;
            opcionesReversibles[i].CameraPos = new GameObject();//opciones[i].CameraPos;
            opcionesReversibles[i].CameraType = 0;//opciones[i].CameraType;
            opcionesReversibles[i].degrees = 0;//opciones[i].degrees;
            opcionesReversibles[i].axis = Vector3.zero;//opciones[i].axis;
            opcionesReversibles[i].gameobject = new GameObject();//opciones[i].gameobject;
            opcionesReversibles[i].reversibleChange = false;//opciones[i].reversibleChange;
        }
    }


    void OnTriggerEnter(Collider col){ //CUANDO ENTRO EN EL TRIGGER EJECUTO LAS OPCIONES SELECCIONADAS
        for (int i = 0; i < opciones.Length; ++i)
        {
            switch (opciones[i].option)
            {
                case OptionType.FixedCamera:
                    if (opciones[i].CameraPos != null)
                    {
                        //si la opcion tiene que ser reversible, guardamos el valor actual
                        if (opciones[i].reversibleChange)
                        {
                            opcionesReversibles[i].option = opciones[i].option;
                            opcionesReversibles[i].reversibleChange = true;
                            opcionesReversibles[i].CameraPos.gameObject.transform.position = TP_Camera.Instance.transform.position;
                            opcionesReversibles[i].CameraPos.gameObject.transform.rotation = TP_Camera.Instance.transform.rotation;
                            opcionesReversibles[i].CameraType = TP_Camera.Instance.GetMode();
                            Debug.Log("Opcion " + i + ": CameraPos: " + opcionesReversibles[i].CameraPos.gameObject.transform.position);
                            Debug.Log("Opcion " + i + ": Camera Mode: " + opcionesReversibles[i].CameraType);
                        }
                        TP_Camera.Instance.transform.position = opciones[i].CameraPos.transform.position;
                        TP_Camera.Instance.transform.rotation = opciones[i].CameraPos.transform.rotation;
                        TP_Camera.Instance.SetMode(CameraTypes.Puntos);
                    }
                    else
                        Debug.LogWarning("Camera position must be set in Inspector");
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
            }
        }
    }
    
    void OnTriggerExit() //SI HAY OPCIONES REVERSIBLES AL SALIR DEL TRIGGER, LAS REVIERTO
    {
        for (int i = 0; i < opciones.Length; ++i)
        {
            Debug.Log("Opcion " + i + ": Es reversible: " + opcionesReversibles[i].reversibleChange);
            Debug.Log("Opcion " + i + ": CameraPos: " + opcionesReversibles[i].CameraPos.gameObject.transform.position);
            Debug.Log("Opcion " + i + ": CameraPos: " + opcionesReversibles[i].CameraPos);
            Debug.Log("Opcion " + i + ": Camera Mode: " + opcionesReversibles[i].CameraType);
            if (opcionesReversibles[i].reversibleChange)
            {
                switch (opcionesReversibles[i].option)
                {
                    case OptionType.FixedCamera:
                        TP_Camera.Instance.transform.position = opcionesReversibles[i].CameraPos.gameObject.transform.position;
                        TP_Camera.Instance.transform.rotation = opcionesReversibles[i].CameraPos.transform.rotation;
                        TP_Camera.Instance.modoCamara = opcionesReversibles[i].CameraType;
                        break;

                    case OptionType.ChangeCameraType:
                        TP_Camera.Instance.modoCamara = opcionesReversibles[i].CameraType;
                        break;
                }
            }
        }
    }

	//Cambio del Icono segun el tipo de trigger.
	void OnDrawGizmos(){
		switch (type){
			case TriggerType.HUD:
				Gizmos.DrawIcon (this.transform.position, "InGameMessage.tiff", true);
				break;
		}
	}


}
