using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWaypointPath : MonoBehaviour
{
    //za svaki objekt nacrtaj kuglu
    private void OnDrawGizmos() {
        foreach(Transform t in transform){          
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(t.position, 1f);
        }

        Gizmos.color = Color.red;
        //crtanje linija izmedju objekata
        for (int i = 0; i< transform.childCount -1; i++){    
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i+1).position);  
        }

        // crtanje linije izmedju zadnjeg i prvog objekta
        Gizmos.DrawLine(transform.GetChild(transform.childCount -1).position, transform.GetChild(0).position); 
    }

}
