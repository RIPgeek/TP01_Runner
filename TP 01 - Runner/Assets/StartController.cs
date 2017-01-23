using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour {
	public Button playButton,
		leaveButton,
		hintButton;
	public Text hintText;
	public Button exitHintButton;

	public void Start()
	{
		hintText.enabled = false;
		changeStateButton(exitHintButton, false);
	}

	public void Play()
	{
		SceneManager.LoadScene("Level 01");
	}

	public void Leave()
	{
		Application.Quit();
	}

	public void Hint()
	{
		changeStateButton(playButton, false);
		changeStateButton(leaveButton, false);
		changeStateButton(hintButton, false);
		changeStateButton(exitHintButton, true);
		hintText.enabled = true;
	}

	public void exitHint()
	{
		Start();
		changeStateButton(playButton, true);
		changeStateButton(leaveButton, true);
		changeStateButton(hintButton, true);
	}

	private void changeStateButton(Button button, bool enable)
	{
		button.enabled = enable;
		button.image.enabled = enable;
		button.GetComponentInChildren<Text>().enabled = enable;
	}
}
