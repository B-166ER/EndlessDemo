using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;

public class Stacker : MonoBehaviour
{
    [Range(0f,5f)] [SerializeField] float DistanceOfStackeds;
    public List<GameObject> stackeds = new List<GameObject>();
    public void DestroyOneFromStack()
    {
        if(stackeds.Count == 0)
        {
            GameManager.instance.GameLost();
            return;
        }
        GameObject tmp = stackeds[stackeds.Count - 1];
        stackeds.Remove(tmp);
        gameObject.GetComponent<PlayerBehaviour>().SetPlayerSpeed( (stackeds.Count+3f) / 3f );
        tmp.transform.parent = null;
        tmp.AddComponent<Rigidbody>();
        tmp.gameObject.GetComponent<Rigidbody>().AddForce(tmp.transform.forward * 200);
        tmp.gameObject.GetComponent<Rigidbody>().AddForce(tmp.transform.up * 400);
        tmp.GetComponent<CollectableBehaviour>().DestroySelf();
    }

    public void StackTheParameter(GameObject gObj)
    {
        stackeds.Add(gObj);
        gObj.transform.parent = gameObject.transform;
        gameObject.GetComponent<PlayerBehaviour>().SetPlayerSpeed( (stackeds.Count + 3f) / 3f );
    }
    private void Update()
    {
        for (int i = 0; i < stackeds.Count; i++)
        {
            Vector3 tmp = new Vector3();
            tmp.y += (DistanceOfStackeds *(i + 1f));
            stackeds[i].gameObject.transform.localPosition = tmp;
            stackeds[i].gameObject.transform.rotation = gameObject.transform.rotation;
        }
    }
    private void Start()
    {
        GameManager.instance.onEndGame += EndGameThrower;
    }
    public void ThrowLatestCube()
    {
        
    }
    public void EndGameThrower()
    {
        StartCoroutine(EndGameThrowerRoutine());
    }
    public IEnumerator EndGameThrowerRoutine()
    {


        float initialStackedCount = stackeds.Count;
        for (int i = stackeds.Count-1; i >= 0; i--)
        {
            Debug.Log("End Game Thrower Index " + i);
            GameObject tmp = stackeds[i];
            tmp.transform.parent = null;
            tmp.gameObject.AddComponent<Rigidbody>();
            tmp.gameObject.GetComponent<Rigidbody>().AddForce(stackeds[i].transform.forward * (50 + (Mathf.Abs(i - initialStackedCount) * 50)));
            tmp.gameObject.GetComponent<Rigidbody>().AddForce(stackeds[i].transform.up * (50 + (Mathf.Abs(i - initialStackedCount) * 50)));
            tmp.gameObject.GetComponent<BoxCollider>().enabled = true;
            tmp.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            stackeds.RemoveAt(i);

            //(120+Mathf.Abs( i-stackeds.Count )*50)

            yield return new WaitForSecondsRealtime(0.7f);
        }

        gameObject.GetComponent<Rigidbody>().mass = 1f;
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward *100 );
        gameObject.GetComponent<Rigidbody>().AddForce(transform.up *500);
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
    }
}
