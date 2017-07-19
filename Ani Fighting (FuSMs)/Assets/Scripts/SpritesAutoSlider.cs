using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SpritesAutoSlider : MonoBehaviour {
    
    [SerializeField]
    Vector3 startPosition;
    [SerializeField]
    Vector3 respawnPosition;
    [SerializeField]
    float speed;
    [SerializeField]
    bool isHorizontal;

    private Vector3 direction;
    private Rigidbody2D sasRigidbody2D;

	void Start () {
        startPosition = transform.position;
        sasRigidbody2D = GetComponent<Rigidbody2D>();

        if (isHorizontal)
        {
            if (startPosition.x <= respawnPosition.x)
            {
                direction = new Vector2(-1, 0);
            }
            else
            {
                direction = new Vector2(1, 0);
            }
        }
        else
        {
            if (startPosition.y <= respawnPosition.y)
            {
                direction = new Vector2(0, -1);
            }
            else
            {
                direction = new Vector2(0, 1);
            }
        }
	}
	
	void FixedUpdate () {
        sasRigidbody2D.velocity = direction * speed;
	}

    void OnBecameInvisible()
    {
        sasRigidbody2D.transform.position = respawnPosition;
    }
}
