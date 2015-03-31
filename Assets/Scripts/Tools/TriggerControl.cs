using UnityEngine;
using UnityEditor;
using System.Collections;

public enum TriggerIcon {InGameMessage = 1, ControlsInfo, Cinematic}

public class TriggerControl : MonoBehaviour {

	public TriggerIcon triggerType;

	public bool testing;

	private bool disablePlayerControl;
	public bool activateParticleSystem;
	public bool changeCameraMode;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (testing) {
			Debug.Log ("ES TRUE");
		} else {
			Debug.Log ("ES FALSE");
		}
	}

	//Cambio del Icono segun el tipo de trigger.
	void OnDrawGizmos(){

		switch (triggerType){
			case TriggerIcon.InGameMessage:
				Gizmos.DrawIcon (this.transform.position, "InGameMessage.tiff", true);
				break;
			case TriggerIcon.ControlsInfo:
				Gizmos.DrawIcon (this.transform.position, "InfoMessage.tiff", true);
				break;
		}
	}


}
