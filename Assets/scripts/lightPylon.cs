using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightPylon : MonoBehaviour {

	void OnMouseDown()
    {
		this.transform.parent.transform.parent.transform.gameObject.GetComponent<lightPylonManager>().RaiseSinglePylon(this.gameObject);
	}

}
