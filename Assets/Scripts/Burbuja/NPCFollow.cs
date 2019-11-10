using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow : MonoBehaviour
{
    public GameObject Player;
    public float TargetDistance;
    public float AllowedDistance;
    public GameObject NPC;
    public float FollowSpeed;
    public RaycastHit Shot;

    void Update() {
        transform.LookAt(Player.transform);
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot))
        {
            TargetDistance = Shot.distance;
            if(TargetDistance >= AllowedDistance)
            {
                FollowSpeed = 0.1f;
                transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, FollowSpeed);
            }else{
                FollowSpeed = 0;
            }
        }
    }

}
