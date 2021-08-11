using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private Transform enemyPosition;
    public Transform playerPos;
    Vector3 playerDir;
    private float angleToPlayer;
    public float sightDistance;
    // Start is called before the first frame update
    void Start()
    {
        enemyPosition = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
       
        //Debug.DrawRay(enemyPosition.position, enemyPosition.forward * 5, Color.red);
        Debug.Log(Vector3.Distance(enemyPosition.position, playerPos.position));

        if (Vector3.Distance(enemyPosition.position, playerPos.position) <= sightDistance)
        {
            playerDir = playerPos.position - enemyPosition.position;
            angleToPlayer = Vector3.Angle(enemyPosition.forward , playerDir);

           // Debug.LogWarning(angleToPlayer);
            if (angleToPlayer < 55f)
            {
                

                RaycastHit hit;
                if (Physics.Raycast(enemyPosition.position, playerDir, out hit, Mathf.Infinity))
                {
                    //Debug.DrawRay(enemyPosition.position, playerDir, Color.red);
                    if (hit.collider.tag == "Player")
                    {
                        Debug.DrawRay(enemyPosition.position, playerDir, Color.red);
                        Debug.LogWarning ("I see the player!");
                    }
                    else
                    {
                        Debug.LogWarning("Player out of sight");
                    }
                }
            }
        }
        #region 
/*
        if (angleToPlayer < 55f)
        {
            Debug.DrawLine(enemyPosition.position, playerPos.position, Color.red);
            Debug.LogWarning("Can see player!");
        }
        else
        {
            Debug.Log("Player is safe");
        }
*/
#endregion
    }
}
