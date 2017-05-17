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

	public override void Start () {
		base.Start();
		charaRigidbody2D = GetComponent<Rigidbody2D> (); 
	}

	void Update(){
		HandleInput ();
	}
	
	public override void FixedUpdate () {
		float horizontal = Input.GetAxis ("Horizontal");
        
        base.horizontal = horizontal;
        base.FixedUpdate();

		Flip (horizontal);
	}

	private void HandleInput(){
		if (Input.GetKeyDown (KeyCode.X)) {
			CharaAnimator.SetTrigger ("jump");
		}
		if (Input.GetKeyDown (KeyCode.Z)) {
			if (!crouch)
				CharaAnimator.SetTrigger ("lightAttack");
			else
				CharaAnimator.SetTrigger ("crouchAttack");
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			CharaAnimator.SetTrigger ("rangedAttack");
		}
		if (Input.GetKeyDown (KeyCode.V)) {
			CharaAnimator.SetTrigger ("heavyAttack");
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

	private void Flip(float horizontal){
		if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight)) {
			ChangeDirection ();	
		}
	}

	public override void CastingMagic(int value){
		if (((!onGround && value == 1) || (onGround && value == 0)) && !crouch) {
			base.CastingMagic (value);
		}
	}

    public override bool IsDead
    {
        get { return health <= 0; }
    }

    public override IEnumerator TakeDamage()
    {
        yield return null;
    }
}