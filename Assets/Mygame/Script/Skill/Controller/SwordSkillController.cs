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
    public  void SetUpSword(Vector2 _dir ,float _gravityScale,Player _player)
    {
        player = _player;
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
        anim.SetBool("Rotation", true);
    }
    private void Update()
    {
        if (canRotate)
            transform.right = rb.velocity;
        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) < 1)
                player.ClearTheSword();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetBool("Rotation", false);
        cd.enabled = false;
        canRotate = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent= collision.transform;
        if (isReturning)
            return;


    //    if (collision.GetComponent<GroundOnlyEnemy>() != null)
    //    {
    //        GroundOnlyEnemy enemy = collision.GetComponent<GroundOnlyEnemy>();
    //        //SwordSkillDamage(enemy);
    //    }


    //    //SetupTargetsForBounce(collision);

    //    //StuckInto(collision);
    }
    public void ReturnSword()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;


        // sword.skill.setcooldown;
    }
}
