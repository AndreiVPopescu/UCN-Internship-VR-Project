using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Administrator : MonoBehaviour {

    public GameObject crosshairNear;
    public GameObject crosshairFar;
    private RaycastHit hit;
    private Ray ray;
    private GameObject hitgo;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("joystick 1 button 0") || Input.GetKeyDown(KeyCode.P))
        {
            ray.origin = crosshairNear.transform.position;
            ray.direction = (crosshairFar.transform.position - crosshairNear.transform.position);
            if (Physics.Raycast(ray, out hit, 100))
            {
                hitgo = hit.transform.gameObject;
                hitgo.transform.SetParent(null);
                hitgo.AddComponent<NetworkTransform>();
                Vector3 pos = hitgo.transform.localPosition;
                hitgo.transform.RotateAround(pos,hitgo.transform.up,90);
            }
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.black, 30);
        }
    }
}
