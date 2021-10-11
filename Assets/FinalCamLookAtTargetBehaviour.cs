using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FinalCamLookAtTargetBehaviour : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera vcam;
    private void Start()
    {
        GameManager.instance.onAdjustCameraForEnding += AdjustCamLookAtTargetOnEnding;
    }
    void AdjustCamLookAtTargetOnEnding()
    {
        vcam.LookAt = gameObject.transform;
    }
}
