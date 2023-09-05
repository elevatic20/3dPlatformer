using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //varijable protivnika (brzina i brzina okretanja)
    [SerializeField] float speed;                           
    public float rotationSpeed = 5f;


    [SerializeField] private WaypointPath waypointPath;     //objekt Parent u kojem se nalaze Child objekti waypointova
    private int nextWaypoint;                               //sledeci waypoint
    private Transform preWaypoint;                          //pozicija prijasnjeg waypointa
    private Transform targWaypoint;                         //pozicija trenutnog
    private float timeToWaypoint;                           //vrijeme do waypointa --> udaljenost izmedu (trenutnog i prijasnjeg)/brzina
    private float elapsedTime;                              //proteklo vrijeme

    void Start() {
        TargetWaypoint();
    }

    void FixedUpdate() {
        //proteklo vrijeme
        elapsedTime += Time.deltaTime;                  

        //postotak proteklog vremena
        float elpPer = elapsedTime / timeToWaypoint;    

        //kretnja protivnika
        transform.position = Vector3.Lerp(preWaypoint.position, targWaypoint.position, elpPer); 

        //dohvacanje sljedeceg waypointa
        if (elpPer >= 1) 
        {
            TargetWaypoint();
        }
        //rotacija prema trenutnom waypointu
        Quaternion targetRotation = Quaternion.LookRotation(targWaypoint.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    //dohvacanje sljedeceg waypointa i izracunavanje vrijeme potrebno do njega
    private void TargetWaypoint()                                   
    {
        preWaypoint = waypointPath.GetWaypoint(nextWaypoint);
        nextWaypoint = waypointPath.GetNextWaypoint(nextWaypoint);
        targWaypoint = waypointPath.GetWaypoint(nextWaypoint);

        elapsedTime = 0;
        float distance = Vector3.Distance(preWaypoint.position, targWaypoint.position);
        timeToWaypoint = distance / speed;
    }
}
