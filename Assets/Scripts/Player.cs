using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public float runSpeed = 20.0f;

    [Header("Components")]
    public Animator topTorso;

    [SerializeField] private Animator botTorso;    
    [HideInInspector] public Health health;

    [Header("Debug")]

    public bool disablePlayer;
    [SerializeField] private bool isMoving = false;

    private float horizontal;
    private float vertical;

    private Rigidbody2D body;
    private Transform muzzle;

    // Start is called before the first frame update
    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        muzzle = GetComponent<Weapon>().muzzle;
    }

    // Update is called once per frame
    private void Update()
    {
        if (disablePlayer)
            return;

        MovePlayer(); // move player using H & V axis (WASD)
        RotateBody(); // rotate player body towards where the player is aiming
        RotateFeet(); // rotate player feet towards where the player is moving

    }

    private void FixedUpdate()
    {
        //body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        body.AddForce(new Vector2(horizontal * runSpeed, vertical * runSpeed));
    }

    private void MovePlayer() {

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

    }

    private void RotateBody() {

        Vector3 difference;
        float distance = Vector3.Distance(gameObject.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        // aim at cursor, if target is to close aim straight
        if (distance > 11f)
        {
            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - muzzle.position;
        }
        else
        {
            difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }
        
        difference.Normalize();     // normalizing the vector. Meaning that all the sum of the vector will be equal to 1

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;   // find the angle in degrees
        topTorso.transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

    }

    private void RotateFeet() {

        if (isMoving)
        {

            botTorso.SetBool("isMoving", isMoving);

            Vector2 v = body.velocity;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            botTorso.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (botTorso.transform.localEulerAngles.z > 90 && botTorso.transform.localEulerAngles.z < 270)
            {
                botTorso.transform.localScale = new Vector3(-1, -1, -1);
            }
            else
            {
                botTorso.transform.localScale = new Vector3(1, 1, 1);
            }

        }
        else
        {
            botTorso.SetBool("isMoving", isMoving);
        }

    }


}
