using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour {

	public float timeToBurn = 5f;
	private Renderer PlantColor;
	private bool onFire = false;
	void Start () {
		PlantColor = this.GetComponent<Renderer>();
		PlantColor.material.color = Color.green;
	}
	void Update (){
		//Fire handling
		if(onFire)
		timeToBurn -= Time.deltaTime;
		if(timeToBurn <= 0)
			Burned();
	}
	//sets plant on fire
	public void SetOnFire()
	{
		onFire = true;
		PlantColor.material.color = Color.red;
		this.tag = "FiringPlant";
	}
	//sets plant in Burned state
	void Burned()
	{
		this.tag = "BurnedPlant";
		onFire = false;
		PlantColor.material.color = Color.black;
	}
	public bool GetBurningState()
	{
		return onFire;
	}

}
