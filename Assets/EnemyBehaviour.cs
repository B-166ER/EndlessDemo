using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] int deductScoreOnStacking;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Stacker>().DestroyOneFromStack();
        ScoreManager.instance.AddScore(-deductScoreOnStacking);
        Destroy(gameObject);
    }
}
