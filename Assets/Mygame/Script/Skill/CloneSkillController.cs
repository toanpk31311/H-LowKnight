using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float colorLoseSpeed;
    private float cloneTimer;
    private SpriteRenderer sr;

    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = .8f;
    private Transform closestEnemy;
    private int facingDir = 1;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent <Animator>();
    }


    public void SetUpClone(Transform _newTranForm,float _cloneDuration,bool _canAttack,Vector3 _offset)
        
    {
        if(_canAttack)
        {
            anim.SetInteger("AttackNumber", Random.Range(1,3));
        }
        transform.position = _newTranForm.position+_offset;
        cloneTimer = _cloneDuration;
    }


    private void Update()
    {
        cloneTimer = -Time.deltaTime;
        if(cloneTimer < 0)
        {
            sr.color= new Color(1,1,1,sr.color.a-(Time.deltaTime*colorLoseSpeed));
            //cloneTimer = cloneDuration;
            if (sr.color.a <= 0)
                Destroy(gameObject);

        }
    }
    private void AnimationTrigger()
    {
        cloneTimer = -.1f;
    }
    private void AttackTrigger()
    {
        Collider2D[] coliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        foreach (var hit in coliders)
        {
            if (hit.GetComponent<GroundOnlyEnemy>() != null)
            {
                hit.GetComponent<GroundOnlyEnemy>().Damage();
            }
        }

    }
}
