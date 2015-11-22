using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class PlayerName : NetworkBehaviour {
    [SerializeField]
    public GameObject mat;
    [SyncVar]
    public string PlayerUniqueName;
    //private Text myText;
    [SerializeField] public Text myText;
    public float r;

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
        //myText = gameObject.GetComponentInChildren
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
        if (isLocalPlayer)
        {
            CmdTellServerMyName(MakeName());
        }
    }

    string MakeName()
    {
        return GameObject.FindGameObjectWithTag("Network").GetComponent<NetworkManagerHUD>().name;
    }

    void SetIdentity()
    {
        if (isLocalPlayer)
        {
            PlayerUniqueName = MakeName();
        }

        myText.text = PlayerUniqueName;
        string Uname = PlayerUniqueName.ToUpper();
        r = (Convert.ToInt32(Uname[0]) - 64) * 9/100;
        float g = (Convert.ToInt32(Uname[1]) - 64) * 9/100;
        float b = (Convert.ToInt32(Uname[2]) - 64) * 9/100;
        mat.GetComponent<MeshRenderer>().material.color = new Color(r,g,b);
    }

    [Command]
    void CmdTellServerMyName(string name)
    {
        PlayerUniqueName = name;
    }
}
