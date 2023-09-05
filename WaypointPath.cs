using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public Transform GetWaypoint(int waypointInd){
        //dohvacanje pozicije trenutnog -> poziva se u MovingPlatform skripti
        return transform.GetChild(waypointInd);                 
    }

    public int GetNextWaypoint(int currentWaypoint){
        //dohvacanje sljedeceg -> poziva se u MovingPlatform skripti
        int nextWaypoint = currentWaypoint + 1;

        if(nextWaypoint == transform.childCount){
            nextWaypoint = 0;
        }

        return nextWaypoint;
    }
}
