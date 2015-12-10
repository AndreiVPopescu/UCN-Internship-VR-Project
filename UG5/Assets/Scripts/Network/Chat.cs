using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Chat : NetworkBehaviour
{
    [SyncVar(hook = "OnNewMessage")]
    public string newMessage;
    public GameObject lastMessage;
    public GameObject input;
    public GameObject content;
    public Transform canvasTransform;
	// Use this for initialization
	void Start ()
    {
        //addMessage("test", "msg1");
        //addMessage("test2", "msg2");
	
	}
    
    void OnNewMessage(string msg)
    {
        GameObject message = new GameObject();
        message.transform.parent = content.transform;
        message.transform.localScale = new Vector3(1, 1, 1);
        message.AddComponent<RectTransform>();
        message.AddComponent<Text>();
        Text messageText = message.GetComponent<Text>();
        messageText.text = msg;
        messageText.alignment = TextAnchor.MiddleLeft;
        messageText.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        messageText.fontSize = 10;
        messageText.color = Color.black;
        message.transform.localPosition = new Vector3(lastMessage.transform.localPosition.x, lastMessage.transform.localPosition.y - 12, lastMessage.transform.localPosition.z);
        message.transform.localRotation = lastMessage.transform.localRotation;
        message.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 15);
        lastMessage = message;
    }

    public void takeMessage()
    {
        CmdsetMessage(GameObject.FindGameObjectWithTag("Network").GetComponent<NetworkManagerHUD>().name, input.GetComponent<Text>().text);
        Debug.Log("message taken");
    }

    //[Command]
    void CmdsetMessage(string name, string msg)
    {
        newMessage = name + ": " + input.GetComponent<Text>().text;
        Debug.Log("message changed");
    }
}
