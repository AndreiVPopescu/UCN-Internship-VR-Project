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
            admin.enabled = true;
        }
        GameObject.FindGameObjectWithTag("Import").GetComponent<Loading>().addPlayer(gameObject);
    }
}
