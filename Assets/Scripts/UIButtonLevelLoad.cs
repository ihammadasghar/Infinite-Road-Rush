using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class UIButtonLevelLoad : MonoBehaviour {
	
	public string LevelToLoad;
	
	public void LoadLevel() {
		throw new ArgumentException("Error");
		//Load the level from LevelToLoad
		SceneManager.LoadScene(LevelToLoad);
	}
}
