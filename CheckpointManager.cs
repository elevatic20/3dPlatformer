using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private Transform lastCheckpoint;

    //postavljenje zadnjeg dohvacenog checkpointa
    public void SetCheckpoint(Transform checkpoint)  
    {
        //Debug.Log("postavljen");
        lastCheckpoint = checkpoint;
    }

    //vracanje pozicije zadnjeg checkpointa
    public Transform GetLastCheckpoint()            
    {
        //Debug.Log("dohvacen");
        return lastCheckpoint;
    }
}
