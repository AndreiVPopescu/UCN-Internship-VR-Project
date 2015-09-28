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
    public GameObject crosshair;
    public NetworkManagerHUD managerHud;
	// Use this for initialization
	void Start ()
    {
        managerHud = GameObject.FindGameObjectWithTag("Network").GetComponent<NetworkManagerHUD>();
        if (isLocalPlayer)
        {
            GetComponent<CharacterController>().enabled = true;
            GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            FPSCharacterCam.enabled = true;
            audioListener.enabled = true;
            crosshair.SetActive(true);
        }
        if (managerHud.isHost)
        {
            admin.enabled = true;
        }
	}
}
