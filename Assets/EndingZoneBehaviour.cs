using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingZoneBehaviour : MonoBehaviour
{
    [SerializeField] Material GreenMat;
    [SerializeField] int zoneMultiplier;
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.other.gameObject.GetComponent<CollectableBehaviour>() != null)
        {
            gameObject.GetComponent<MeshRenderer>().material = GreenMat;
            ScoreManager.instance.setMultiplier(zoneMultiplier);
            ScoreManager.instance.UpdateScore();
        }
        
    }
}
