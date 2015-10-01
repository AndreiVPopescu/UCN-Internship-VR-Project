using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Administrator : NetworkBehaviour {
    public GameObject crosshairNear;
    public GameObject crosshairFar;
    private RaycastHit hit;
    private Ray ray;
    private GameObject hitgo;
    public bool admin;
    // Use this for initialization
    void Start ()
    {
        admin = GameObject.FindGameObjectWithTag("Network").GetComponent<NetworkManagerHUD>().isHost;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (admin)
        {
            if (Input.GetKeyDown("joystick 1 button 0") || Input.GetKeyDown(KeyCode.P))
            {
                RpcOpenDoor(crosshairNear.transform.position, crosshairFar.transform.position - crosshairNear.transform.position);
            }
        }
    }

    [ClientRpc]
    void RpcOpenDoor(Vector3 pos1, Vector3 pos2)
    {
        ray.origin = crosshairNear.transform.position;
        ray.direction = (crosshairFar.transform.position - crosshairNear.transform.position);
        if (Physics.Raycast(ray, out hit, 100))
        {
            hitgo = hit.transform.gameObject;
            hitgo.transform.SetParent(null);
            hitgo.AddComponent<NetworkTransform>();
            Vector3 pos = hitgo.transform.localPosition;
            hitgo.transform.RotateAround(pos, hitgo.transform.up, 90);
        }
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.black, 30);
    }
}
