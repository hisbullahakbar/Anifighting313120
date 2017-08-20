using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character 
{
	private static Player instance;
	public static Player Instance {
		get {
			if (instance == null) {
				instance = GameObject.FindObjectOfType<Player> ();
			}
			return instance;
		}
	}

	private GameObject target;
	public GameObject Target
	{
		get {
			if (target == null) {
				target = GameObject.FindObjectOfType<Enemy> ().gameObject;
			}
			return target;
		}
	}

	public override void Start () {
		healthBar = GameObject.Find ("PlayerSpecialBars");
		base.Start();
		charaRigidbody2D = GetComponent<Rigidbody2D> (); 
		IDCharacter = CharacterChoosenManager.statSelectedCharacter1;

		if (LayerMask.LayerToName(gameObject.layer) == "Erza")
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Lyon"), gameObject.layer);
		else if (LayerMask.LayerToName(gameObject.layer) == "Lyon")
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Erza"), gameObject.layer);

		//Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("FootCollider"), gameObject.layer);
	}

    void Update()
	{
		if (BattleSceneManager.Instance.State == BattleSceneManager.BattleSceneState.battle) {
			if (!takingDamage && !IsDead) {
				HandleInput ();
			}
		}

		//TANDAI INI coba diedit dimasukan kedalam script player dan enemy

		//script ini berhasil ganti ukuran tapi offsetnya tidak bagus
		Vector2 S = gameObject.GetComponent<SpriteRenderer> ().sprite.bounds.size;
		Vector2 P = gameObject.GetComponent<SpriteRenderer> ().sprite.bounds.center;

		//Debug.Log (gameObject.GetComponent<SpriteRenderer> ().sprite.name);
		//Debug.Log ("size = " + S.x + "," + S.y);
		//Debug.Log ((0.5f * S.x * 100));
		//Debug.Log (P.x);
		//Debug.Log (((0.5f * S.x * 100) - P.x));
		//Debug.Log (((0.5f * S.x * 100) - P.x) / 100);
		//Debug.Log ("pivot = " + (((0.5f * S.x * 100) - P.x) / 100) + "," + (((0.5f * S.y * 100) - P.y) / 100));
		gameObject.GetComponent<BoxCollider2D> ().size = S;
		gameObject.GetComponent<BoxCollider2D> ().offset = P;
			//	new Vector2 (((0.5f * S.x * 100) - P.x) / 100, ((0.5f * S.y * 100) - P.y) / 100);

		//script ini berhasill tapi berat
		//Destroy(gameObject.GetComponent<BoxCollider2D>());
		//gameObject.AddComponent<BoxCollider2D>();
	}

    public override void FixedUpdate()
    {
        if (BattleSceneManager.Instance.State == BattleSceneManager.BattleSceneState.battle)
        {
			if ((!takingDamage || jump) && !IsDead)
			{
				float horizontal = 0;
				if (!crouch)
					horizontal = Input.GetAxis ("Horizontal");

                base.horizontal = horizontal;
                base.FixedUpdate();

                LookAtTarget();
                //Flip(horizontal);
			}
        }
    }

	private void HandleInput(){
		if (Input.GetKeyDown (KeyCode.X) || Input.GetKeyDown("joystick button 0")) {
			if (!CharaAnimator.GetBool ("crouch"))
				CharaAnimator.SetTrigger ("jump");
			
            //FuzzyStateMachines.Instance.initiateFuSMs();
            //FuzzyStateMachines.Instance.runFuSMs();
		}
		if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown("joystick button 3"))
        {
            CharaAnimator.SetTrigger("lightAttack");
            if (!jump)
            {
                PlayerInputManager.instance.getCountMovement(MovementType.playerCounter.lightAttack).addLaunchedMovement(1);
                PlayerInputManager.instance.getCountMovement(MovementType.playerCounter.middleDirection).addLaunchedMovement(1);
            }
            else
            {
                PlayerInputManager.instance.getCountMovement(MovementType.playerCounter.lightAttack).addLaunchedMovement(1);
                PlayerInputManager.instance.getCountMovement(MovementType.playerCounter.upDirection).addLaunchedMovement(1);
            }

            PlayerInputManager.instance.getTotalInput().addLaunchedMovement(1);

            //FuzzyStateMachines.Instance.initiateFuSMs();
            //FuzzyStateMachines.Instance.runFuSMs();
        }
		if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown("joystick button 4"))
        {
            CharaAnimator.SetTrigger("rangedAttack");
            if (!jump)
            {
                PlayerInputManager.instance.getCountMovement(MovementType.playerCounter.rangedAttack).addLaunchedMovement(1);
                PlayerInputManager.instance.getCountMovement(MovementType.playerCounter.middleDirection).addLaunchedMovement(1);
            }
            else
            {
                PlayerInputManager.instance.getCountMovement(MovementType.playerCounter.rangedAttack).addLaunchedMovement(1);
                PlayerInputManager.instance.getCountMovement(MovementType.playerCounter.upDirection).addLaunchedMovement(1);
            }

            PlayerInputManager.instance.getTotalInput().addLaunchedMovement(1);

            //FuzzyStateMachines.Instance.initiateFuSMs();
            //FuzzyStateMachines.Instance.runFuSMs();
        }
		if (Input.GetKeyDown(KeyCode.V) || Input.GetKeyDown("joystick button 1"))
        {
            CharaAnimator.SetTrigger("heavyAttack"); //it's become crouch attack, if character is already in crouch position
            if (!crouch)
            {
                PlayerInputManager.instance.getCountMovement(MovementType.playerCounter.heavyAttack).addLaunchedMovement(1);
                PlayerInputManager.instance.getCountMovement(MovementType.playerCounter.middleDirection).addLaunchedMovement(1);
            }
            else
            {
                PlayerInputManager.instance.getCountMovement(MovementType.playerCounter.heavyAttack).addLaunchedMovement(1);
                PlayerInputManager.instance.getCountMovement(MovementType.playerCounter.bottomDirection).addLaunchedMovement(1);
            }

            PlayerInputManager.instance.getTotalInput().addLaunchedMovement(1);

            //FuzzyStateMachines.Instance.initiateFuSMs();
            //FuzzyStateMachines.Instance.runFuSMs();
        }

        //---------------delete this because this fitur is unused-------------------
		if (Input.GetKeyDown (KeyCode.F)) {
			CharaAnimator.SetBool ("guard", true);
		} else if (Input.GetKeyUp (KeyCode.F)) {
			CharaAnimator.SetBool ("guard", false);
		}
		//--------------------------------------------------------------------------	

		if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetAxis("Vertical") < -0.75f) { //INI MASIH TES!!!!!!!!!!!!!!!!!!!!!!!!!!!
			if (onGround && !jump) {
				CharaAnimator.SetBool ("crouch", true);
			}

            //FuzzyStateMachines.Instance.initiateFuSMs();
            //FuzzyStateMachines.Instance.runFuSMs();
		} else if (Input.GetKeyUp (KeyCode.DownArrow) || Input.GetAxis("Vertical") == 0.0f) {
            CharaAnimator.SetBool("crouch", false);
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

	public override IEnumerator TakeDamage(float damagePoint)
	{
		if (!immortal) {
			health -= (int)damagePoint;
			healthBar.GetComponent<HealthBar> ().UpdateHealthBar (health);
			if (CharaAnimator.GetBool ("crouch"))
				CharaAnimator.SetBool ("crouch", false);

			if (!IsDead) {
				CharaAnimator.SetTrigger ("damage");
				if (damageCounter < 2) {
					damageCounter += 1;
					CharaAnimator.SetInteger ("damageCounter", damageCounter);
				} else {
					immortal = true;

					StartCoroutine (IndicateImmortal ());
					yield return new WaitForSeconds (immortalTime);
					immortal = false;

					damageCounter = 0;
					CharaAnimator.SetInteger ("damageCounter", damageCounter);
				}
			} else {
				CharaAnimator.SetLayerWeight (1, 0);
				CharaAnimator.SetTrigger ("die");
				WinLoseManager.Instance.setWinLoseState (WinLoseManager.WinloseState.player2Win);
				BattleSceneManager.Instance.State = BattleSceneManager.BattleSceneState.winLosePose;
			}

			yield return null;
		}
	}
}