using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireAngle : MonoBehaviour {
	private Slider windPower;
	private Slider windDirection;
	private bool onFire;
	private bool canFire;
	
	// Use this for initialization
	void Start () {
		windPower = GameObject.Find("WindPower").GetComponent<Slider>();
		windDirection = GameObject.Find("WindDirection").GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
			transform.position = new Vector3(transform.parent.position.x,transform.parent.position.y+0.5f, transform.parent.position.z);
			transform.rotation = Quaternion.Euler(0,windDirection.value,0);
			transform.position += this.transform.forward*windPower.value;
			onFire = transform.parent.GetComponent<Plant>().GetBurningState();
	}
	void OnTriggerStay(Collider col){
		if(col.tag == "Plant" && onFire)
				col.GetComponent<Plant>().SetOnFire();
	}
}
