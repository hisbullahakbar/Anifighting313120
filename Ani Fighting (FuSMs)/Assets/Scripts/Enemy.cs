using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    private IEnemyState currentState;

    private GameObject target;

    [SerializeField]
    private float nearRange;
    [SerializeField]
    private float farRange;

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

	public override void Start () {
        base.Start();
        target = Player.Instance.gameObject;
        
        ChangeState(new IdleState());
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

    void Update()
    {
        currentState.Execute();
        LookAtTarget();
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

    void OnTriggerEnter2D(Collider2D other)
    {
        currentState.OnTriggerEnter(other);
    }

    public override IEnumerator TakeDamage()
    {
        health -= 10;

        if (!IsDead)
        {
            CharaAnimator.SetTrigger("damage");
        }
        else
        {
            CharaAnimator.SetTrigger("die");
            yield return null;
        }
    }

    public override bool IsDead
    {
        get { return health <= 0; }
    }
}