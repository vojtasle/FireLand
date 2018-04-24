using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnviromentController : MonoBehaviour {
	public GameObject plantPrefab;
	public int minXSpawn=0;
	public int maxXSpawn=400;
	public int minZSpawn=0;
	public int maxZSpawn=400;
	public AudioSource windSound;
	public AudioSource fireSound;
	private Slider windPower;
	private Button addToggle;
	private Button removeToggle;
	private Button fireToggle;
	private bool addSelected = false;
	private bool fireSelected = false;
	private bool removeSelected = false;
	void Start () {
		windPower = GameObject.Find("WindPower").GetComponent<Slider>();
		SpawnPlants(800);
		fireToggle = GameObject.Find("FireTGL").GetComponent<Button>();
		removeToggle = GameObject.Find("RemoveTGL").GetComponent<Button>();
		addToggle = GameObject.Find("AddTGL").GetComponent<Button>();
		DisableMouseActions();
	}
	void Update () {
		if( Input.GetMouseButtonDown(0) )
		{
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
         	RaycastHit hit;
         
        	if( Physics.Raycast( ray, out hit ) )
        	{
				if(hit.transform.tag == "Plant"&&fireSelected)
				{
         	 		hit.transform.GetComponent<Plant>().SetOnFire();
				}
				else if(addSelected)
				{
					Vector3 position = ray.GetPoint(hit.distance);
					if(position.y <= 3.6f || position.y>50 || position.x < 80 || position.x >320 || position.z < 90 || position.z >320)
						GameObject.Find("Warning").GetComponent<Warning>().ShowWarning("You can't place plant there!", 5);
					else
						Instantiate(plantPrefab,position,Quaternion.identity);
				}
				else if(removeSelected && hit.transform.name.Contains("Plant"))
				{
					Destroy(hit.transform.gameObject);
				}
       		}
		}
		windSound.volume = windPower.value * Time.timeScale;
		GameObject firingPlant = GameObject.FindWithTag("FiringPlant") as GameObject;
		if(firingPlant != null && Time.timeScale ==1f)
			fireSound.volume = 1f;
		else
			fireSound.volume = 0f;
	}
	//Spawns plants randomly across the terrain
	public void SpawnPlants(int spawnAmount)
	{
		Clear();
		for (int i = 0; i < spawnAmount; i++)
		{
			float x = Random.Range(minXSpawn,maxXSpawn);
			float z = Random.Range(minZSpawn,maxZSpawn);
			Vector3 posXZ = new Vector3(x,0,z); 
			Vector3 nextPosition = new Vector3(x,Terrain.activeTerrain.SampleHeight(posXZ), z);
			if(nextPosition.y<= 3.6f)			
				spawnAmount++;		
			else
			Instantiate(plantPrefab, nextPosition, Quaternion.identity);
		}
	}
	//Destroys all plants in scene
	public void Clear()
	{
		GameObject[] Firingplants = GameObject.FindGameObjectsWithTag("FiringPlant");
		foreach(GameObject plant in Firingplants)
		{
			Destroy(plant);
		}
		GameObject[] Burnedplants = GameObject.FindGameObjectsWithTag("BurnedPlant");
		foreach(GameObject plant in Burnedplants)
		{
			Destroy(plant);
		}
		GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");
		foreach(GameObject plant in plants)
		{
			Destroy(plant);
		}
		
	}
	public void FreezeTime()
	{
		 if (Time.timeScale == 1.0)            
            Time.timeScale = 0.0f;        
        else
            Time.timeScale = 1.0f; 
	}
	//Disables all toggleable mouse interactions
	void DisableMouseActions()
	{

		ColorBlock red = fireToggle.colors;
        red.normalColor = Color.red;
		red.highlightedColor = Color.red;

		addSelected = false;
		fireSelected = false;
		removeSelected = false;  
        fireToggle.colors = red;
		removeToggle.colors = red;
		addToggle.colors =red;
	}
	//Toggles between mouse interactions
	public void EnableMouseInteraction(string interaction)
	{
		DisableMouseActions();
		ColorBlock green = fireToggle.colors;
        green.normalColor = Color.green;
		green.highlightedColor = Color.green;

		switch (interaction)
		{
			case "Fire":
				fireToggle.colors = green;
				fireSelected = true;
			break;
			case "Add":
				addToggle.colors = green;
				addSelected = true;
			break;
			case "Remove":
				removeToggle.colors = green;
				removeSelected = true;
			break;
			default:
			break;
		}
	}
	//Sets random plants on fire
	public void FireRandom(int plantsAmount)
	{
		GameObject[] plants = GameObject.FindGameObjectsWithTag("Plant");
		int index;
		if(plants.Length>0)
		{
			for (int i = 0; i < plantsAmount; i++)
			{
				index = Random.Range(0,plants.Length);
				GameObject plant = plants[index];
				if(plant != null)
				{
					plant.GetComponent<Plant>().SetOnFire();
				}
			}
		}
	}

}
