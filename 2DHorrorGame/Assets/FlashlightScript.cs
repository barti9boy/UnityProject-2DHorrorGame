using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    public enum FlashlightPosition : byte
    {
        StandingPosition = 0,
        WalkingPosition = 1,
        SitingPosition = 2,
    }
    [SerializeField ]private List<Transform> flashlightPositions = new List<Transform>();

    public void ChangeFlashlightPosition(FlashlightPosition position)
    {
        transform.position = flashlightPositions[((byte)position)].position;
    }

}
