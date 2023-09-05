using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private WaypointPath waypointPath;     //objekt Parent u kojem se nalaze Child objekti waypointova
    [SerializeField] float platformSpeed;                   //brzina platforme
    private int nextWaypoint;                               //sledeci waypoint
    private Transform preWaypoint;                          //pozicija prijasnjeg waypointa
    private Transform targWaypoint;                         //pozicija trenutnog
    private float timeToWaypoint;                           //vrijeme do waypointa --> udaljenost izmedu (trenutnog i prijasnjeg)/brzina
    private float elapsedTime;                              //proteklo vrijeme
    
    //dohvacanje ciljanog waypointa
    void Start()
    {
        TargetWaypoint();
    }

    //micanje platforme od waypointa do waypointa
    void FixedUpdate()                                      
    {
        elapsedTime += Time.deltaTime;

        float elpPer = elapsedTime / timeToWaypoint;
        elpPer = Mathf.SmoothStep(0,1,elpPer);
        transform.position = Vector3.Lerp(preWaypoint.position, targWaypoint.position, elpPer);

        if(elpPer >=1){
            TargetWaypoint();
        }
    }

    //dohvacanje sljedeceg waypointa u nizu i izracunavanje vrijeme potrebno do njega
    private void TargetWaypoint(){                                                          
        preWaypoint = waypointPath.GetWaypoint(nextWaypoint);
        nextWaypoint = waypointPath.GetNextWaypoint(nextWaypoint);
        targWaypoint = waypointPath.GetWaypoint(nextWaypoint);

        elapsedTime = 0;
        float distance = Vector3.Distance(preWaypoint.position, targWaypoint.position);
        timeToWaypoint = distance/platformSpeed;
    }

    //kad igrac stane na platformu koja se mice postane njezino dijete kako bi se mogao kretati s njome
    private void OnTriggerEnter(Collider other) {       
        other.transform.SetParent(transform);
    }

    //kad igrac izadje iz prostora pokretne platforme, makne se iz hijerarhije platforme
    private void OnTriggerExit(Collider other) {        
        other.transform.SetParent(null);
    }
}
