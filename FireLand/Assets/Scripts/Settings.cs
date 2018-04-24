using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour{

		public void QuitGame()
		{
			Application.Quit();
		}
		public void ToggleSound()
		{
			if(AudioListener.volume == 0)
			AudioListener.volume = 1f;
			else
			AudioListener.volume = 0f;
		}		
	}

