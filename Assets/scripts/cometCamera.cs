using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cometCamera : MonoBehaviour {

	public GameObject camOrb;
	public GameObject mainCam;

	bool rotating = false;
	bool clicked;
	float doubleClickTimer = -1;
	Vector3 targetPos;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1)){
			checkDoubleClick();
		}
		cancelDoubleClick();

		//Rotate if right mouse down
        if (Input.GetMouseButtonDown(1))
			rotating = true;
		if (Input.GetMouseButtonUp(1))
			rotating = false;
		if(rotating){
			Rotate();
		}

		Zoom();

	}

	void Rotate(){
		float deltaX = Input.GetAxis("Mouse Y") * 2;
		float deltaY = Input.GetAxis("Mouse X") * 2;

		float rotX = camOrb.transform.rotation.eulerAngles.x;
		float rotY = camOrb.transform.rotation.eulerAngles.y;

		float toX;
		float toY;

		toX = Mathf.Clamp(rotX + deltaX, 1, 89);	//clamp so it does not go upside down or under ground
		toY = rotY + deltaY;

		Quaternion toRotation = Quaternion.Euler(toX, toY, 0);
		camOrb.transform.rotation = toRotation;
	}

	void Zoom(){
		float zoomDelta;
		zoomDelta = Input.GetAxis("Mouse ScrollWheel") * 50;
		float posZ;
		posZ = Mathf.Clamp(mainCam.transform.localPosition.z + zoomDelta, -1000, 0);

		mainCam.transform.localPosition = new Vector3(0, 0, posZ);
	}

	void cancelDoubleClick(){
		if(clicked){
			doubleClickTimer -= Time.deltaTime;
		}
		if(doubleClickTimer < -0.1)
			clicked = false;
	}

	void checkDoubleClick(){
		if(clicked){
			//second click
			goToTargetPosition();
		} else {
			//first click
			//get target position
			getTargetPosition();
			clicked = true;
			doubleClickTimer = 0.25f;
		}
	}

	void getTargetPosition(){
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity)){
			targetPos = hit.point;
		}
	}

	void goToTargetPosition(){
		this.transform.position = targetPos;
	}

}
