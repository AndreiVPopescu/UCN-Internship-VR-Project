using UnityEngine;
using System.Collections.Generic;

public class Minimap : MonoBehaviour
{
    public GameObject player;
    public GameObject[] players;

	// Use this for initialization
	void Start ()
    {
        
	}

    public void setPlayer(GameObject player)
    {
        this.player = player;
    }

	// Update is called once per frame
	void Update ()
    {
        if (!player.Equals(null))
        {
            transform.position = new Vector3(transform.position.x, player.transform.position.y + (float)1.9, transform.position.z);

        }
	}
}
