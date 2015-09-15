using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ButtonStare : MonoBehaviour
{
    public GameObject crosshairNear;
    public GameObject crosshairFar;
    public GameObject obj;
    private RaycastHit hit;
    private Ray ray;
    // Use this for initialization
    void Start ()
    {
        ray = new Ray();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("joystick 1 button 0") || Input.GetKeyDown(KeyCode.P))
        {
            ray.origin = crosshairNear.transform.position;
            ray.direction = (crosshairFar.transform.position- crosshairNear.transform.position);
            Physics.Raycast(ray,out hit,100);
            Debug.DrawRay(ray.origin, ray.direction*100, Color.black,30);
            obj = hit.transform.gameObject;
            if (obj.GetComponent<Button>() != null)
                {
                obj.GetComponent<Button>().onClick.Invoke(); }
        }
        
    }
}
