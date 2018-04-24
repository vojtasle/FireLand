using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warning : MonoBehaviour {

	public GameObject warningPanel;
	public Text warningMessage;
	private float showTime;
	private bool warningShowen = false;
	
	void Update()
	{
		if(showTime>0)
		showTime -= Time.deltaTime;
		if(warningShowen && showTime<=0)
		{
			HideWarning();
		}
	}
	void Start()
	{
		warningMessage.text = "";
	}
	public void ShowWarning(string warningMessage, float timeToDisplay)
	{
		showTime = timeToDisplay;
		warningPanel.SetActive(true);
		this.warningMessage.text = warningMessage;
		warningShowen = true;
	}
	private void HideWarning()
	{
		warningPanel.SetActive(false);
		this.warningMessage.text = "";
		warningShowen = false;
	}
}
