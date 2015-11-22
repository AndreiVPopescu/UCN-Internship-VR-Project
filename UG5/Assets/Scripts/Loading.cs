using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour
{
    public GameObject camera;
    private GameObject[] listOfPlayers;
    public GameObject canvas;
    public GameObject minimap;
    private int n = 0;
    // Use this for initialization
    void Start ()
    {
        listOfPlayers = new GameObject[20];
        minimap.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameObject.transform.childCount != 0)
        {
            GameObject.Destroy(camera);
            GameObject.Destroy(canvas);
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            for (int i = 0; i < n; i++)
            {
                listOfPlayers[i].SetActive(true);
                if (listOfPlayers[i].GetComponent<Player_NetworkSetup>().isLocalPlayer)
                {
                    minimap.GetComponent<Minimap>().setPlayer(listOfPlayers[i]);
                }
            }
            minimap.SetActive(true);
            this.enabled = false;
        }
	}

    public void addPlayer(GameObject player)
    {
        listOfPlayers[n] = player;
        n++;
        player.SetActive(false);
    }

}
