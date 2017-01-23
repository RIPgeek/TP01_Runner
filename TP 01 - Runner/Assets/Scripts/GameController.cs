using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

	public HeroController hero;
	public Button ResetButton;
	public Button QuitButton;
	public Image youDied;

	// Use this for initialization
	void Start()
	{
		ResetButton.enabled = false;
		ResetButton.image.enabled = false;
		ResetButton.GetComponentInChildren<Text>().enabled = false;
		QuitButton.enabled = false;
		QuitButton.image.enabled = false;
		QuitButton.GetComponentInChildren<Text>().enabled = false;
		youDied.enabled = false;
	}

	public void die()
	{
		ResetButton.enabled = true;
		ResetButton.image.enabled = true;
		ResetButton.GetComponentInChildren<Text>().enabled = true;
		QuitButton.enabled = true;
		QuitButton.image.enabled = true;
		QuitButton.GetComponentInChildren<Text>().enabled = true;
		youDied.CrossFadeAlpha(0, 0, false);
		youDied.enabled = true;
		youDied.CrossFadeAlpha(1, 1, false);
	}

	public void restart()
	{
		Start();
		hero.restart();
	}

	public void stop()
	{
		SceneManager.LoadScene("Start");
	}

	public void win()
	{
		SceneManager.LoadScene("Win");
	}
}
