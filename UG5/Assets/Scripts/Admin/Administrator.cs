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
    public bool admin=true;
    public bool coold = false;
    
    // Use this for initialization
    void Start ()
    {
        admin = GameObject.FindGameObjectWithTag("Network").GetComponent<NetworkManagerHUD>().isHost;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if ((CrossPlatformInputManager.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.P)) && !coold)
        {
            if (admin)
            {
                RpcLockPlayer(crosshairNear.transform.position, crosshairFar.transform.position - crosshairNear.transform.position);
            }
            OpenDoor(crosshairNear.transform.position, crosshairFar.transform.position - crosshairNear.transform.position);
            coold = true;
            Invoke("resetCD", 3.0f);
        }
        if (CrossPlatformInputManager.GetButtonDown("Fire2") && !coold)
        {
            CmdHideObject(crosshairNear.transform.position, crosshairFar.transform.position - crosshairNear.transform.position);
            coold = true;
            Invoke("resetCD", 3.0f);
        }
    }

    private void resetCD()
    {
        coold = false;
    }

    [ClientRpc]
    void RpcLockPlayer(Vector3 pos1, Vector3 pos2)
    {
        ray.origin = pos1;
        ray.direction = pos2;
        if (Physics.Raycast(ray, out hit, 100))
        {
            hitgo = hit.transform.gameObject;
            if (hitgo.tag.Equals("Player"))
            {
                hitgo.GetComponent<CharacterController>().enabled = false;
                StartCoroutine(LockPlayer(hitgo.transform));
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.black, 30);
    }
    
    void OpenDoor(Vector3 pos1, Vector3 pos2)
    {
        ray.origin = pos1;
        ray.direction = pos2;
        if (Physics.Raycast(ray, out hit, 100))
        {
            hitgo = hit.transform.gameObject;
            if (hit.transform.parent.tag.Equals("Import"))
            {
                hitgo.SetActive(false);
                StartCoroutine(CloseDoor(hitgo.transform));
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.black, 30);
    }

    [Command]
    void CmdHideObject(Vector3 pos1, Vector3 pos2)
    {
        ray.origin = pos1;
        ray.direction = pos2;
        if (Physics.Raycast(ray, out hit, 100))
        {
            hitgo = hit.transform.gameObject;
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
        yield return new WaitForSeconds(5f);
        Debug.Log("Close after");
        t.gameObject.SetActive(true);
    }

    IEnumerator MakeTransparent(Transform t)
    {
        yield return new WaitForSeconds(10f);
        t.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
    }

    IEnumerator LockPlayer(Transform t)
    {
        yield return new WaitForSeconds(10f);
        t.gameObject.GetComponent<CharacterController>().enabled = true;
    }
}
