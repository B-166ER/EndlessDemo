using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehaviour : MonoBehaviour
{
    [SerializeField] int addScoreOnStacking;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hey");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Stacker>() != null)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.GetComponent<Stacker>().StackTheParameter(gameObject);
            ScoreManager.instance.AddScore(addScoreOnStacking);
        }
    }
    public void DestroySelf()
    {
        StartCoroutine(DestroySelfRoutine());
    }
    IEnumerator DestroySelfRoutine()
    {
        yield return new WaitForSecondsRealtime(4f);
        Debug.Log("I Destroyed Myself");
        Destroy(gameObject);
    }
}
