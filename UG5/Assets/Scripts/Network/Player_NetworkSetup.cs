using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player_NetworkSetup : NetworkBehaviour
{
    [SerializeField]
    Camera FPSCharacterCam;
    [SerializeField]
    AudioListener audioListener;
    [SerializeField]
    Administrator admin;
    public GameObject cam;
    public GameObject cyl;
    public GameObject bar;
    public GameObject loading;
    public GameObject crosshair;
	// Use this for initialization
	void Start ()
    {
        if (isLocalPlayer)
        {
            GetComponent<CharacterController>().enabled = true;
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            FPSCharacterCam.enabled = true;
            audioListener.enabled = true;
            crosshair.SetActive(true);
            cam.SetActive(true);
            admin.enabled = true;
            cyl.SetActive(false);
            bar.SetActive(false);
        }
        Loading go = GameObject.FindGameObjectWithTag("Import").GetComponent<Loading>();
        if (go.enabled)
        {
            go.addPlayer(gameObject);
        }
    }
}
