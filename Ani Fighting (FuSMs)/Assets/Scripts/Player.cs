using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

	private static Player instance;
	public static Player Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindObjectOfType<Player> ();
			}
			return instance;
		}
	}
    
    [SerializeField]
    private GameObject target;

	public override void Start () {
		base.Start();
		charaRigidbody2D = GetComponent<Rigidbody2D> (); 
	}

    void Update()
    {
        //taking damage belum ditambahkan disini
        if (!takingDamage && !IsDead)
        {
            HandleInput();
        }
    }

    public override void FixedUpdate()
    {
        if (!takingDamage && !IsDead)
        {
            float horizontal = Input.GetAxis("Horizontal");

            base.horizontal = horizontal;
            base.FixedUpdate();

            LookAtTarget();
            //Flip(horizontal);
        }
    }

	private void HandleInput(){
		if (Input.GetKeyDown (KeyCode.X)) {
			CharaAnimator.SetTrigger ("jump");
		}
		if (Input.GetKeyDown (KeyCode.Z)) {
				CharaAnimator.SetTrigger ("lightAttack");
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			CharaAnimator.SetTrigger ("rangedAttack");
		}
		if (Input.GetKeyDown (KeyCode.V)) {
			CharaAnimator.SetTrigger ("heavyAttack"); //it's become crouch attack, if character is already in crouch position
		}

		if (Input.GetKeyDown (KeyCode.F)) {
			CharaAnimator.SetBool ("guard", true);
		} else if (Input.GetKeyUp (KeyCode.F)) {
			CharaAnimator.SetBool ("guard", false);
		}
			
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (onGround && !jump) {
				CharaAnimator.SetBool ("crouch", true);
			}
		} else if (Input.GetKeyUp (KeyCode.DownArrow)) {
			CharaAnimator.SetBool ("crouch", false);
		}
	}

    private void LookAtTarget()
    {
        if (target != null)
        {
            float xDir = target.transform.position.x - transform.position.x;

            if ((xDir < 0 && facingRight) || (xDir > 0 && !facingRight))
            {
                ChangeDirection();
            }
        }
    }

	/*private void Flip(float horizontal){
		if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight)) {
			ChangeDirection ();	
		}
	}*/

	public override void CastingMagic(int value){
		if (((!onGround && value == 1) || (onGround && value == 0)) && !crouch) {
			base.CastingMagic (value);
		}
	}

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    public override bool IsDead
    {
        get { return health <= 0; }
    }

    public override IEnumerator TakeDamage()
    {
        health -= 10;
        healthBar.GetComponent<HealthBar>().UpdateHealthBar(health);

        if (!IsDead)
        {
            CharaAnimator.SetTrigger("damage");
            if (damageCounter < 2)
            {
                damageCounter += 1;
            }
            else
            {
                damageCounter = 0;
            }
            CharaAnimator.SetInteger("damageCounter", damageCounter);
        }
        else
        {
            CharaAnimator.SetLayerWeight(1, 0);
            CharaAnimator.SetTrigger("die");
        }

        yield return null;
    }
}