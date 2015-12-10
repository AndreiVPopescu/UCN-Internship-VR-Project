using UnityEngine;
using System.Collections;

public class Adjuster : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (transform.childCount > 0)
        {
            transform.position = new Vector3(0, (float)-23.1, 57);
            this.enabled = false;
        }
	}
}
