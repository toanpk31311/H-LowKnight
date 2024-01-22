using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SwordSkillController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;

    private bool canRotate = true;
    private bool isReturning;


    private float freezeTimeDuration;
    private float returnSpeed = 12;

    [Header("Bounce info")]
    [SerializeField] public float bounceSpeed = 20;
    private bool isBouncing;
    private int bounceAmount = 4;
    private List<Transform> enemyTarget;
    private int targetIndex;

    [Header("Pierce info")]
    private float pierceAmount;


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
    public void SetUpSword(Vector2 _dir, float _gravityScale, Player _player)
    {
        player = _player;
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
        if (pierceAmount <= 0)
        {
            anim.SetBool("Rotation", true);
        }
       
    }
    private void Update()
    {
        if (canRotate)
            transform.right = rb.velocity;
        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < 1)
                player.CatchTheSword();
        }
        BounceLogic();

    }

    private void BounceLogic()
    {
        if (isBouncing && enemyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIndex].position, bounceSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < .1f)
            {
                targetIndex++;
                bounceAmount--;
                if (bounceAmount <= 0)
                {
                    isBouncing = false;
                    isReturning = true;
                }

                if (targetIndex >= enemyTarget.Count)
                    targetIndex = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (isReturning)
            return;
        SetupTargetsForBounce(collision);
        StuckInto(collision);

    }
    public void SetupBounce(bool _isBouncing, int _amountOfBounces)
    {
        isBouncing = _isBouncing;
        bounceAmount = _amountOfBounces;
        //bounceSpeed = _bounceSpeed;


        enemyTarget = new List<Transform>();
    }

    private void SetupTargetsForBounce(Collider2D collision)
    {
        if (collision.GetComponent<GroundOnlyEnemy>() != null)
        {

            if (isBouncing && enemyTarget.Count <= 0)
            {
                Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 10);
                foreach (var hit in collider)
                {
                    if (hit.GetComponent<GroundOnlyEnemy>() != null)
                    {
                        enemyTarget.Add(hit.transform);
                    }
                }
            }

        }
    }

    private void StuckInto(Collider2D collision)
    {
        if (pierceAmount > 0 && collision.GetComponent<GroundOnlyEnemy>() != null)
        {
            pierceAmount--;
            return;
        }

        cd.enabled = false;
        canRotate = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        if (isBouncing && enemyTarget.Count > 0) { return; }
        

        anim.SetBool("Rotation", false);
        transform.parent = collision.transform;
    }

    public void ReturnSword()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;


        // sword.skill.setcooldown;
    }
    public void SetupPierce(int _pierceAmount)
    {
        pierceAmount = _pierceAmount;
    }
}
