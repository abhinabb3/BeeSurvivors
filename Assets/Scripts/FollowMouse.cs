using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    Vector3 targetPos;

    private void Start()
    {
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        //targetPos = Camera.main.ScreenToWorldPoint(targetPos);
        transform.position = targetPos;
    }
}
