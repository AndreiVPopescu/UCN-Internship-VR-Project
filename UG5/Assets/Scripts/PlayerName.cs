using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerName : NetworkBehaviour {

    [SyncVar]
    public string PlayerUniqueName;
    private Text myText;

    public override void OnStartLocalPlayer()
    {
        GetNetIdentity();
        SetIdentity();
    }
	// Use this for initialization
	void Start () {

	}

    void Awake()
    {
        myText = GetComponentInChildren<Text>();
    }

	// Update is called once per frame
	void Update ()
    {
        if (myText.text == "New Text" || myText.text=="")
        {
            SetIdentity();
        }
	}

    [Client]
    void GetNetIdentity()
    {
        CmdTellServerMyName(MakeName());
    }

    string MakeName()
    {
        return System.Environment.MachineName;
    }
    
    void SetIdentity()
    {
        if (isLocalPlayer)
        {
            PlayerUniqueName = MakeName();
        }

        myText.text = PlayerUniqueName;
    }

    [Command]
    void CmdTellServerMyName(string name)
    {
        PlayerUniqueName = name;
    }
}
