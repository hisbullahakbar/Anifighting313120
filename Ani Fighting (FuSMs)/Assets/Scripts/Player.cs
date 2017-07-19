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
    
    private GameObject target;
    public GameObject Target
    {
        get
        {
            if (target == null)
            {
                target = GameObject.FindObjectOfType<Enemy>().gameObject;
            }
            return target;
        }
    }

	public override void Start () {
		base.Start();
		charaRigidbody2D = GetComponent<Rigidbody2D> (); 
	}

    void Update()
    {
        if (BattleSceneManager.Instance.State == BattleSceneManager.BattleSceneState.battle)
        {
            if (!takingDamage && !IsDead)
            {
                HandleInput();
            }
        }
        else if (BattleSceneManager.Instance.State == BattleSceneManager.BattleSceneState.beginingPose)
        {

        }
    }

    public override void FixedUpdate()
    {
        if (BattleSceneManager.Instance.State == BattleSceneManager.BattleSceneState.battle)
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
    }

	private void HandleInput(){
		if (Input.GetKeyDown (KeyCode.X)) {
			CharaAnimator.SetTrigger ("jump");

            //FuzzyStateMachines.Instance.initiateFuSMs();
            //FuzzyStateMachines.Instance.runFuSMs();
        }
        if (Input.GetKeyDown(KeyCode.Z))
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
        if (Input.GetKeyDown(KeyCode.C))
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
        if (Input.GetKeyDown(KeyCode.V))
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

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (onGround && !jump) {
				CharaAnimator.SetBool ("crouch", true);
			}

            //FuzzyStateMachines.Instance.initiateFuSMs();
            //FuzzyStateMachines.Instance.runFuSMs();
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