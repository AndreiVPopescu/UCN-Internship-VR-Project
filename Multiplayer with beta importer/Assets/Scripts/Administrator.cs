using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

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
            if (CrossPlatformInputManager.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.P))
            {
                RpcOpenDoor(crosshairNear.transform.position, crosshairFar.transform.position - crosshairNear.transform.position);
            }
            if (CrossPlatformInputManager.GetButtonDown("Fire2"))
            {
                RpcHideObject(crosshairNear.transform.position, crosshairFar.transform.position - crosshairNear.transform.position);
            }
        }
    }

    [ClientRpc]
    void RpcOpenDoor(Vector3 pos1, Vector3 pos2)
    {
        ray.origin = pos1;
        ray.direction = pos2;
        if (Physics.Raycast(ray, out hit, 100))
        {
            hitgo = hit.transform.gameObject;
            //hitgo.transform.SetParent(null);
            if (hitgo.tag == "Model")
            {
                hitgo.gameObject.SetActive(false);
                StartCoroutine(CloseDoor(hitgo.transform));
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.black, 30);
    }

    [ClientRpc]
    void RpcHideObject(Vector3 pos1, Vector3 pos2)
    {
        ray.origin = pos1;
        ray.direction = pos2;
        if (Physics.Raycast(ray, out hit, 100))
        {
            hitgo = hit.transform.gameObject;
            //hitgo.transform.SetParent(null);
            if (hitgo.tag == "Model")
            {
                hitgo.GetComponentInChildren<MeshRenderer>().enabled = false;
                StartCoroutine(MakeTransparent(hitgo.transform));
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.black, 30);
    }

    IEnumerator CloseDoor(Transform t)
    {
        Debug.Log("Close before");
        yield return new WaitForSeconds(10f);
        Debug.Log("Close after");
        t.gameObject.SetActive(true);
    }

    IEnumerator MakeTransparent(Transform t)
    {
        yield return new WaitForSeconds(10f);
        t.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
    }
}
