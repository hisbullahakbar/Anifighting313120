using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{

    [SerializeField]
    protected float movementSpeed;
    [SerializeField]
    protected bool facingRight;

    [SerializeField]
    private GameObject[] castedMagicPrefab;
    [SerializeField]
    protected Transform magicPos;

    [SerializeField]
    private Transform[] groundPoints;
    [SerializeField]
    private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private bool airControl;
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    protected int health;
    public abstract bool IsDead { get; }

    [SerializeField]
    public EdgeCollider2D[] attackCollider;

    [SerializeField]
    private List<string> damageSources;

    public Animator CharaAnimator { get; private set; }
    public Rigidbody2D charaRigidbody2D { get; set; }
    public bool attack { get; set; }
    public bool jump { get; set; }
    public bool onGround { get; set; }
    public bool guard { get; set; }
    public bool crouch { get; set; }
    public float horizontal { get; set; }
    public bool takingDamage { get; set; }
    public int damageCounter { get; set; }

    [SerializeField]
    protected GameObject healthBar;

    [SerializeField]
    protected CharacterMugshoot mugshoot;

    public virtual void Start()
    {
        CharaAnimator = GetComponent<Animator>();
        charaRigidbody2D = GetComponent<Rigidbody2D>();
        mugshoot = GetComponent<CharacterMugshoot>();
        //facingRight = true;
        for (int i = 0; i < attackCollider.Length; i++)
        {
            setAttackColliderFalse(i);
        }
        damageCounter = 0;
        healthBar.GetComponent<HealthBar>().SetMaximum(gameObject);
        healthBar.GetComponent<HealthBar>().UpdateHealthBar(health);
    }

    void Update()
    {

    }

    public int getHealth()
    {
        return health;
    }

    public void setAttackColliderTrue(int i)
    {
        attackCollider[i].enabled = true;
    }

    public void setAttackColliderFalse(int i)
    {
        attackCollider[i].enabled = false;
    }

    public abstract IEnumerator TakeDamage();

    public virtual void FixedUpdate()
    {
        onGround = IsGrounded(charaRigidbody2D, groundPoints, groundRadius, whatIsGround);
        HandleMovement(horizontal);

        HandleLayers();
    }

    private void HandleMovement(float horizontal)
    {
        if (charaRigidbody2D.velocity.y < 0)
        {
            CharaAnimator.SetBool("land", true);
        }
        if (!attack && !guard && !crouch && (onGround || airControl))
        {
            charaRigidbody2D.velocity = new Vector2(horizontal * movementSpeed, charaRigidbody2D.velocity.y);
        }
        if (jump && charaRigidbody2D.velocity.y == 0)
        {
            charaRigidbody2D.AddForce(new Vector2(0, jumpForce));
        }
        CharaAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    public void ChangeDirection()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
    }

    public void beginingPoseExit()
    {
        CharaAnimator.SetBool("beginingPose", false);
    }

    public virtual void CastingMagic(int value)
    {
        if (facingRight)
        {
            GameObject temp = (GameObject)Instantiate(castedMagicPrefab[value], magicPos.position, Quaternion.Euler(0, 0, 0));
            temp.GetComponent<MagicAttack>().Initialize(Vector2.right);
        }
        else
        {
            GameObject temp = Instantiate(castedMagicPrefab[value], magicPos.position, Quaternion.Euler(0, 180, 0));
            temp.GetComponent<MagicAttack>().Initialize(Vector2.left);
        }
    }

    protected bool IsGrounded(Rigidbody2D charaRigidbody2D, Transform[] groundPoints, float groundRadius, int whatIsGround)
    {
        if (charaRigidbody2D.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        if (!onGround)
        {
            CharaAnimator.SetLayerWeight(1, 1);
            CharaAnimator.SetLayerWeight(2, 0);
        }
        else
        {
            CharaAnimator.SetLayerWeight(1, 0);
            CharaAnimator.SetLayerWeight(2, 1);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (damageSources.Contains(other.tag))
        {
            //MASIH ADA BUG DIBAGIAN GETDAMAGE!!!
            //1. get damage tidak keluar saat player memilih state aktif lain
            StartCoroutine(TakeDamage());
        }
    }
}