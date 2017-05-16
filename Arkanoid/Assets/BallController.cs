using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

    public float Speed = 20f;

    private GameObject _playerObject;

    private float _halfBallHeight;
    private float _halfPlayerHeight;

    private bool _isActive = false;

	void Start() 
    {
        _playerObject = GameObject.Find("Player");

        Sprite ballSprite = GetComponent<SpriteRenderer>().sprite;
        _halfBallHeight = GetHalfHeight(ref ballSprite);

		Sprite playerSprite = _playerObject.GetComponent<SpriteRenderer>().sprite;
		_halfPlayerHeight = GetHalfHeight(ref playerSprite);
	}
	
	void FixedUpdate() 
    {
        if(!_isActive)
        {
            PlaceBallOnTopOfPlayer();    
        }
	}

    public void ActivateBall()
    {
        _isActive = true;
        GetComponent<Rigidbody2D>().velocity = Speed * Vector2.up;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player" && _isActive)
        {
            GetComponent<Rigidbody2D>().velocity = Speed * GetForceDirection(ref collision);
            Destroy(collision.gameObject);
        }
    }

    private void PlaceBallOnTopOfPlayer()
    {
        Vector3 playerPosition = _playerObject.transform.position;
        playerPosition.y += _halfBallHeight;
        playerPosition.y += _halfPlayerHeight;

        transform.position = playerPosition;
    }

    private float GetHalfHeight(ref Sprite sprite)
    {
        float halfHeight = 0f;

        halfHeight = (sprite.bounds.max.y - sprite.bounds.min.y) / 2;

        return halfHeight;
    }

    private Vector2 GetForceDirection(ref Collision2D playerCollision)
    {
        Vector2 direction = Vector2.up;

        Vector3 estimatedDirection = (transform.position - playerCollision.transform.position);
        estimatedDirection /= playerCollision.collider.bounds.size.x;

        direction.x = estimatedDirection.normalized.x;

        return direction;
    }
}
