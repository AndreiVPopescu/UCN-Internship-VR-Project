using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public class OptionsMenu : MonoBehaviour {

    public Canvas menu;
    public FirstPersonController player;

	// Use this for initialization
	void Start ()
    {
        menu.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (CrossPlatformInputManager.GetButtonDown("Cancel"))
        {
            if (!menu.enabled)
            {
                menu.enabled = true;
                player.enabled = false;
            }
            else { resume(); }
        }
    }

    public void resume()
    {
        menu.enabled = false;
        player.enabled = true;
    }

    public void quit()
    {
        Application.Quit();
    }
}
