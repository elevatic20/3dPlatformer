using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    //varijabla broj sireva(jedino oovdje se moze promijeniti vrijednost, no bilo gdje pristupiti vrijednosti)
    public int NumberOfCheese {get; private set;}
    //svaki put kad se pokupi sir
    public UnityEvent<Inventory> CheeseCollected; 

    public void CheeseCollector(){
        NumberOfCheese++;
        //šalje se broj sireva (DisplayUI skripti)
        CheeseCollected.Invoke(this); 
        AudioManagerScript.Instance.PlayCollectSound();
    }
}
