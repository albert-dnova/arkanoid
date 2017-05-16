using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public GameObject BallObject;
    public GameObject PlayerObject;

    public float PlayerMovementSpeed = 15f;

	void Start () {
	}
	
	void FixedUpdate () 
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            BallObject.GetComponent<BallController>().ActivateBall();
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        PlayerObject.GetComponent<Rigidbody2D>().velocity = horizontalInput * Vector2.right * PlayerMovementSpeed;
	}
}
