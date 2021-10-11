using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FinalCamPosBehaviour : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vcam;
    private void Start()
    {
        GameManager.instance.onAdjustCameraForEnding += AdjustCameraFollowTargetOnEnding;
    }
    void AdjustCameraFollowTargetOnEnding()
    {
        vcam.Follow = gameObject.transform;
    }
}
