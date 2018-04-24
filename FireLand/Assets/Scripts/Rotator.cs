using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotator : MonoBehaviour {
	public Slider windDirection;
	void Update () {
		transform.rotation = Quaternion.Euler(0,0,-windDirection.value);
	}
}
