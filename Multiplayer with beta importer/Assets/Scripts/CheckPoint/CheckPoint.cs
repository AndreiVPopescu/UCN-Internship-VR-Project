using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CheckPoint: NetworkBehaviour {

    public Transform cpObject;
    public Transform player;


    GameObject checkPoint;
    public bool spawned = false;

    void Update()
    {

        if (Input.GetKeyDown("u") == true)
        {

            RpcCreateCheckPoint();
        
        }

        if (Input.GetKeyDown("y") == true)
        {

            GoToCP();

        }

    }

   // [ClientRpc]
    public void RpcCreateCheckPoint() {

        CreateCheckPoint(player.transform);

    }

    public void CreateCheckPoint(Transform pos) {

        if (!spawned)
        {

            checkPoint = Instantiate(cpObject.gameObject, pos.position, Quaternion.identity) as GameObject;
            spawned = true;
            

        }
        else {

            GameObject.Destroy(checkPoint);
            checkPoint = null;
            spawned = false;
            
        }

       
    
    }

    public void GoToCP() {
        if (spawned)
        {
            player.position = checkPoint.transform.position;
        }
    
    }
}
