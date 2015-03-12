using UnityEngine;
using System.Collections;

public class DN_LookAt : MonoBehaviour {
	public Transform target;

	void Update() {
		transform.LookAt(target);
	}
}