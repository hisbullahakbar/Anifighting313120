using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MagicAttack : MonoBehaviour {

	[SerializeField]
	private float speed;

	private Rigidbody2D maRigidbody2D;

	private Vector2 direction;

    void Start()
    {
        maRigidbody2D = GetComponent<Rigidbody2D>();

        if (LayerMask.LayerToName(gameObject.layer) == "IceEagle")
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Lyon"), gameObject.layer);
        else if (LayerMask.LayerToName(gameObject.layer) == "ThunderLance")
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Erza"), gameObject.layer);
    }
	
	void Update () {
		
	}

	void FixedUpdate(){
		maRigidbody2D.velocity = direction * speed;
	}

	public void Initialize(Vector2 direction){
		this.direction = direction;
	}

	void OnBecameInvisible(){
		Destroy (gameObject);
	}
}
