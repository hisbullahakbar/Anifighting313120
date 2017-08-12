using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    private IEnemyState currentState;
    public IEnemyState CurrentState
    {
        get
        {
            return currentState;
        }
    }

    private GameObject target;
    public GameObject Target
    {
        get
        {
            if (target == null)
            {
                target = GameObject.FindObjectOfType<Player>().gameObject;
            }
            return target;
        }
    }

    [SerializeField]
    private float nearRange;
    public float NearRange
    {
        get { return nearRange; }
    }
    [SerializeField]
    private float farRange;
    public float FarRange
    {
        get { return farRange; }
    }

    public bool InNearRange
    {
        get
        {
            if (target != null)
            {
                return Vector2.Distance(transform.position, target.transform.position) <= nearRange;
            }
            return false;
        }
    }


    public bool InFarRange
    {
        get
        {
            if (target != null)
            {
                return Vector2.Distance(transform.position, target.transform.position) <= farRange;
            }
            return false;
        }
    }

    public override void Start()
    {
        base.Start();
		target = Player.Instance.gameObject;
		IDCharacter = CharacterChoosenManager.statSelectedCharacter2;

        ChangeState(new IdleState());

		if (LayerMask.LayerToName(gameObject.layer) == "Erza")
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Lyon"), gameObject.layer);
		else if (LayerMask.LayerToName(gameObject.layer) == "Lyon")
			Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Erza"), gameObject.layer);
    }

    //use this method in class player for change flip function jobs..
    //after it make move backward in this class and used it in StateClass WalkBackward
    //and try it in idle state
    //after all of step try to access jump attack, jump ranged attack, and crouch attack...
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

    void Update()
    {
        if (BattleSceneManager.Instance.State == BattleSceneManager.BattleSceneState.battle)
        {
            if (!IsDead)
            {
                if (!takingDamage)
                {
                    currentState.Execute();
                    LookAtTarget();
                }
            }
            //Debug.Log(Player.Instance.Target.GetComponent<Enemy>().CurrentState.getStateName());
        }
        //else if (BattleSceneManager.Instance.State == BattleSceneManager.BattleSceneState.beginingPose)
        //{

        //}

		Vector2 S = gameObject.GetComponent<SpriteRenderer> ().sprite.bounds.size;
		Vector2 P = gameObject.GetComponent<SpriteRenderer> ().sprite.bounds.center;

		gameObject.GetComponent<BoxCollider2D> ().size = S;
		gameObject.GetComponent<BoxCollider2D> ().offset = P;
    }

    public void ChangeState(IEnemyState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = newState;
        currentState.Enter(this);
    }

    public void Move()
    {
        if (!attack)
        {
            CharaAnimator.SetFloat("speed", 1f);
            transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime));
        }
    }

    public Vector2 GetDirection()
    {
        return facingRight ? Vector2.right : Vector2.left;
    }

    public void MoveBackward()
    {
        if (!attack)
        {
            CharaAnimator.SetFloat("speed", 1f);
            transform.Translate(GetDirection() * (movementSpeed * Time.deltaTime) * -1);
        }
    }

    /*public Vector2 GetDirectionBackward()
    {
        return facingRight ? Vector2.left : Vector2.right;
    }*/

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        currentState.OnTriggerEnter(other);
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
            CharaAnimator.SetTrigger("die");
            WinLoseManager.Instance.setWinLoseState(WinLoseManager.WinloseState.player1Win);
            BattleSceneManager.Instance.State = BattleSceneManager.BattleSceneState.winLosePose;
            yield return null;
        }
    }

    public override bool IsDead
    {
        get { return health <= 0; }
    }
}