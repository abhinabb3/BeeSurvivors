using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathtimer : MonoBehaviour
{

    [SerializeField] private float timer;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,timer);
    }

}
