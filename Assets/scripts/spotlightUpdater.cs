using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spotlightUpdater : MonoBehaviour {

	GameObject[] pylons;
	GameObject closestPylon;
	GameObject secondClosestPylon;
	GameObject thirdClosestPylon;
	float closestDistance = 10000f;
	float secondClosestDistance = 10000f;
	float thirdClosestDistance = 10000f;

	// Use this for initialization
	void Start () {
		pylons = GameObject.FindGameObjectsWithTag("pylon");
		for(int i = 0; i < pylons.Length; i++){
			float myDistance = Vector3.Distance(pylons[i].transform.position, transform.position);
			if( myDistance < closestDistance){
				//second and third steps one down
				thirdClosestDistance = secondClosestDistance;
				secondClosestDistance = closestDistance;
				if(secondClosestPylon)
					thirdClosestPylon = secondClosestPylon;
				if(closestPylon)
					secondClosestPylon = closestPylon;
				//set new closest distance
				closestDistance = myDistance;
				closestPylon = pylons[i];
				//Debug.Log("found closer pylon");
			} else if(myDistance < secondClosestDistance){
				//second steps one down
				thirdClosestDistance = secondClosestDistance;
				if(secondClosestPylon)
					thirdClosestPylon = secondClosestPylon;
				//set new second closest pylon
				secondClosestDistance = myDistance;
				secondClosestPylon = pylons[i];
			} else if (myDistance < thirdClosestDistance){
				thirdClosestDistance = myDistance;
				thirdClosestPylon = pylons[i];
			}
		}
		SetLightIntensity();
		InvokeRepeating("SetLightIntensity", 3f, 3f);
	}

	void SetLightIntensity(){
		float closestY = closestPylon.transform.localScale.y;
		float secondClosestY = secondClosestPylon.transform.localScale.y;
		float thirdClosestY = thirdClosestPylon.transform.localScale.y;
		this.GetComponent<Light>().intensity = (closestY + secondClosestY + thirdClosestY) / 225;
	}

}
