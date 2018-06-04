using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightPylonManager : MonoBehaviour {

	public int gridX;
	public int gridZ;

	public GameObject valopylväs;
	int valopylväsId = 1;

	public List<GameObject> pylons = new List<GameObject>();

	// Use this for initialization
	void Start () {
		for(int i = 0; i < gridX; i++){
			for(int j = 0; j < gridZ; j++){
				float posX = i * 150.0F + this.transform.position.x;
				float posY = this.transform.position.y;
				float posZ = j * 150.0F + this.transform.position.z;
				GameObject pylväsParent = Instantiate(valopylväs, new Vector3(posX, posY, posZ), Quaternion.identity);
				pylväsParent.transform.parent = this.transform;
				GameObject pylväs = pylväsParent.transform.GetChild(0).gameObject;
				pylväs.name = "valopylväs" + valopylväsId.ToString();
				valopylväsId++;
				pylons.Add(pylväs);
			}
		}
	}
	
	public void RaiseSinglePylon(GameObject pylon){
		float raiseBy = 5;
		float lowerBy = raiseBy / (pylons.Count - 1);
		Debug.Log("valopylväs: " + valopylväs.transform.localScale.y.ToString());
		Debug.Log("pylon: " + pylon.transform.localScale.y.ToString());
		if (pylon.transform.localScale.y < 100 - raiseBy){
			//raise pylon & update % text
			pylon.transform.localScale = new Vector3(pylon.transform.localScale.x, pylon.transform.localScale.y + raiseBy, pylon.transform.localScale.z);
			GameObject percentTxtRaise = pylon.transform.parent.transform.GetChild(2).gameObject;
			percentTxtRaise.transform.position = new Vector3(percentTxtRaise.transform.position.x, percentTxtRaise.transform.position.y + raiseBy*2, percentTxtRaise.transform.position.z);
			percentTxtRaise.GetComponent<TextMesh>().text = pylon.transform.localScale.y.ToString("F1") + "%";

			for (int i = 0; i < pylons.Count; i++){
				if(pylons[i].name == pylon.name){
					//do nothing
				} else {
					//lower pylon & update % text
					pylons[i].transform.localScale = new Vector3(pylons[i].transform.localScale.x, pylons[i].transform.localScale.y - lowerBy, pylons[i].transform.localScale.z);
					GameObject percentTxtLower = pylons[i].transform.parent.transform.GetChild(2).gameObject;
					percentTxtLower.transform.position = new Vector3(percentTxtLower.transform.position.x, percentTxtLower.transform.position.y - lowerBy*2, percentTxtLower.transform.position.z);
					percentTxtLower.GetComponent<TextMesh>().text = pylons[i].transform.localScale.y.ToString("F1") + "%";
				}
			}
		}
	}

	public void EvenPylons(){
		for(int k = 0; k < pylons.Count; k++){
			pylons[k].transform.localScale =  new Vector3(pylons[k].transform.localScale.x, 50, pylons[k].transform.localScale.z);
			GameObject percentTxtEven = pylons[k].transform.parent.transform.GetChild(2).gameObject;
			percentTxtEven.transform.position = new Vector3(percentTxtEven.transform.position.x, 100, percentTxtEven.transform.position.z);
			percentTxtEven.GetComponent<TextMesh>().text = "50%";
		}
	}

}