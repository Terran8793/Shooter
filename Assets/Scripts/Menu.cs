using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public GameObject mainMenuHolder;
	public GameObject optionsMenuHolder;

	public Slider[] volumeSliders;
	public Toggle[] resolutionToggles;
	public Toggle fullscreenToggle;
	public int[] screenWidths;
	int activeScreenResIndex;

	void Start(){
		activeScreenResIndex = PlayerPrefs.GetInt ("screem res index");
		bool isFullscreen = (PlayerPrefs.GetInt ("fullsceen") == 1) ? true : false;

		for (int i = 0; i < resolutionToggles.Length; i++) 
		{
			resolutionToggles [i].isOn = i == activeScreenResIndex;
		}

		fullscreenToggle.isOn = isFullscreen;
	}

	public void Play()
	{
		SceneManager.LoadScene ("Enemy Ai");
	}

	public void Quit()
	{
		Application.Quit ();
	}

	public void OptionsMenu()
	{
		mainMenuHolder.SetActive (false);
		optionsMenuHolder.SetActive (true);
	}

	public void MainMenu()
	{
		mainMenuHolder.SetActive (true);
		optionsMenuHolder.SetActive (false);
	}

	public void SetScreenResolution(int i)
	{
		if (resolutionToggles [i].isOn) 
		{
			activeScreenResIndex = i;
			float aspectRatio = 16 / 9f;
			Screen.SetResolution (screenWidths [i], (int)(screenWidths [i] / aspectRatio), false);
			PlayerPrefs.SetInt("screen res indes", activeScreenResIndex);
			PlayerPrefs.Save ();
		}
	}

	public void SetFullscreen(bool isFullscreen)
	{
		for (int i = 0; i < resolutionToggles.Length; i++) 
		{
			resolutionToggles [i].interactable = !isFullscreen;
		}
		if (isFullscreen) {
			Resolution[] allResolutions = Screen.resolutions;
			Resolution maxResolutions = allResolutions [allResolutions.Length - 1];
			Screen.SetResolution (maxResolutions.width, maxResolutions.height, true);
		}else{
			SetScreenResolution (activeScreenResIndex);
		}

		PlayerPrefs.SetInt ("fullscreen", ((isFullscreen) ? 1 : 0));
		PlayerPrefs.Save ();
	}

	public void SetMasterVolume(float value){
	}

	public void SetMusicVolume(float value){
	}

	public void SetSfxVolume(float value){
	}

}
