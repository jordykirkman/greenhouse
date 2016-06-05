using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {
	private Valve.VR.EVRButtonId padButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
//	private Valve.VR.EVRButtonId padButton2 = Valve.VR.EVRButtonId.k_EButton_Axis2;
//	private Valve.VR.EVRButtonId padButton3 = Valve.VR.EVRButtonId.k_EButton_Axis3;
//	private Valve.VR.EVRButtonId padButton4 = Valve.VR.EVRButtonId.k_EButton_Axis4;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	private GameObject pickup;
	private Vector3 controllerAxis;
	private Vector2 touchAxis;
	public Transform cube;
	public Transform sphere;
	public Transform capsule;
	public Transform tree;

	private SteamVR_Controller.Device controller {
		get {
			return SteamVR_Controller.Input((int)trackedObj.index);
		}
	}
	private SteamVR_TrackedObject trackedObj;

	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		touchAxis = new Vector3 (0, 0, 0);
		controllerAxis = new Vector2 (0, 0);
	}

	// Update is called once per frame
	void Update () {

		if(controller == null) {
			Debug.Log("Controller not initialized");
			return;
		}

		touchAxis = controller.GetAxis();
		controllerAxis = this.transform.localPosition;

		if (controller.GetPressDown(triggerButton) && pickup != null) {
			pickup.transform.parent = this.transform;
			pickup.GetComponent<Rigidbody>().isKinematic = true;
		}
		if (controller.GetPressUp(triggerButton) && pickup != null) {
			pickup.transform.parent = null;
			pickup.GetComponent<Rigidbody>().isKinematic = false;
		}

		if (controller.GetPressDown(padButton)) {
			if(touchAxis.y > 0 && touchAxis.x < 0) {
				Instantiate(cube, new Vector3(controllerAxis.x, controllerAxis.y, controllerAxis.z), Quaternion.identity);
			}
			if(touchAxis.y > 0 && touchAxis.x > 0) {
				Instantiate(sphere, new Vector3(controllerAxis.x, controllerAxis.y, controllerAxis.z), Quaternion.identity);
			}
			if(touchAxis.y < 0 && touchAxis.x < 0) {
				Instantiate(capsule, new Vector3(controllerAxis.x, controllerAxis.y, controllerAxis.z), Quaternion.identity);
			}
			if(touchAxis.y < 0 && touchAxis.x > 0) {
				Instantiate(tree, new Vector3(controllerAxis.x, 0, controllerAxis.z), Quaternion.identity);
			}
		}
	}

	private void OnTriggerEnter(Collider collider) {
		pickup = collider.gameObject;
	}

	private void OnTriggerExit(Collider collider) {
		pickup = null;
	}

}