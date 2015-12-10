using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

    public Canvas menu;
    public FirstPersonController player;
    private bool mainScene;

	// Use this for initialization
	void Start ()
    {
        mainScene = Application.loadedLevelName.Equals("Main");
        menu.enabled = false;
        if (mainScene)
        {
            Cursor.visible = false;
        }
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
                if (mainScene)
                {
                    Cursor.visible = true;
                }
            }
            else { resume(); }
        }
    }

    public void resume()
    {
        menu.enabled = false;
        player.enabled = true;
        if (mainScene)
        {
            Cursor.visible = false;
        }
    }

    public void quit()
    {
        Application.Quit();
    }
}
