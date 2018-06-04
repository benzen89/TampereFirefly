using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	//switch between single screen camera and CAVE camera system
	public enum screenModes {singleScreen, CAVE}
	public screenModes ScreenMode;
	private GameObject singleScreenCamera;
	private GameObject CaveCameraSystem;

	//movement variables
	public float rotationSpeed = 100;
	public float climbSpeed = 5;
	public float normalSpeed = 10;
	public float fastSpeed = 30;
	private float rotX = 0;
	private float rotY = 0;

	// Use this for initialization
	void Start () {
		singleScreenCamera = this.transform.Find ("SingleScreenCamera").gameObject;
		CaveCameraSystem = this.transform.Find ("CaveCameraSystem").gameObject;
		switch (ScreenMode) {
		case screenModes.singleScreen:
			{
				singleScreenCamera.SetActive (true);
				CaveCameraSystem.SetActive (false);
			}
			break;
		case screenModes.CAVE:
			{
				singleScreenCamera.SetActive (false);
				CaveCameraSystem.SetActive (true);
			}
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {

		//rotation input
		rotX += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
		rotX = Mathf.Repeat (rotX, 360);
		rotY += Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
		rotY = Mathf.Clamp (rotY, -90, 90);


		//rotation
		transform.localRotation = Quaternion.AngleAxis(rotX, Vector3.up);
		transform.localRotation *= Quaternion.AngleAxis(rotY, Vector3.left);


		//movement forward/back and left/right, if Shift is pressed use fast speed, else use normal speed
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
			transform.position += transform.forward * fastSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
			transform.position += transform.right * fastSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
		} else {
			transform.position += transform.forward * normalSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
			transform.position += transform.right * normalSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
		}

		//movement up/down
		if (Input.GetKey (KeyCode.Q)) {
			transform.position += transform.up * climbSpeed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.E)) {
			transform.position -= transform.up * climbSpeed * Time.deltaTime;
		}


	}
}
