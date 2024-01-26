using UnityEngine;

public class Entities : MonoBehaviour
{


    public float facingDirection { get; private set; } = 1;
    public Animator animator { get; private set; }
    public Rigidbody2D rgbody { get; private set; }
    [SerializeField] private GameObject deathSprite;


    protected bool rightFlip = true;


    [Header("Collision")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask groundLayerMask;

    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask wallLayerMask;

    [SerializeField] protected Transform attackCheck;
    [SerializeField] protected float attackCheckRadius;


    [Header("Attribute")]
    public float moveSpeed;
    public int dame;
    public int health;


    protected virtual void Awake()
    {
        rgbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    public void SetAnimator(bool isEnable)
    {
        animator.enabled = isEnable;
    }

    public AnimationClip FindAnimation(string name)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }

        return null;
    }

    public void SetDeathSprite(bool isEnable)
    {
        deathSprite.SetActive(isEnable);
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    public virtual bool IsGroundDetected()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayerMask);
    }

    public virtual bool IsWallDetected()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, wallLayerMask);
    }

    protected virtual void OnDrawGizmos()
    {
        if (groundCheck != null) Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        if (wallCheck != null) Gizmos.DrawLine(wallCheck.position, new Vector3((wallCheck.position.x + wallCheckDistance) * facingDirection, wallCheck.position.y));
        if (attackCheck != null) Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    public void Flip()
    {
        facingDirection *= -1;
        rightFlip = !rightFlip;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float _x)
    {
        if (_x > 0 && !rightFlip)
        {
            Flip();
        }
        else if (_x < 0 && rightFlip)
        {
            Flip();
        }
    }

    public void SetZeroVelocity()
    {
        rgbody.velocity = new Vector2(0, 0);
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rgbody.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    public virtual void Damaged()
    {
        health--;
    }


}
