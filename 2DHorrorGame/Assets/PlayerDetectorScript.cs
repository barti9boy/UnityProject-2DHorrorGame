using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectorScript : MonoBehaviour
{
    [SerializeField] LayerMask playerMask;
    public bool playerDetected;
    // Start is called before the first frame update
    void Start()
    {
        playerDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitPlayer = Physics2D.Raycast(this.transform.position - new Vector3(1, 0, 0), -Vector2.right, 8.0f, playerMask);
        Debug.DrawRay(this.transform.position - new Vector3(1, 0, 0), -Vector2.right * 8.0f);
        if (hitPlayer.collider != null)
        {
            Debug.Log(LayerMask.NameToLayer("Player"));
            Debug.Log(hitPlayer.collider.tag);
            playerDetected = true;
        }
    }
}
