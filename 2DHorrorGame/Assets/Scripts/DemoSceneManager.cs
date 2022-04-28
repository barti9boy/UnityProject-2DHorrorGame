using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSceneManager : MonoBehaviour
{
    public List<GameObject> Switches = new List<GameObject>();
    public List<GameObject> Monsters = new List<GameObject>();

    private List<Collider2D> Colliders = new List<Collider2D>();


    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject mSwitch in Switches)
        {
            Collider2D cld = mSwitch.GetComponent<Collider2D>();
            Colliders.Add(cld);
        }
        foreach (GameObject monster in Monsters)
        {
            monster.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Collider2D cld in Colliders)
        {
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
