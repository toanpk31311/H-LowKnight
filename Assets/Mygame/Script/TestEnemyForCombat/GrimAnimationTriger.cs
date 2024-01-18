using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimAnimationTriger : MonoBehaviour
{
  
    private GrimEnermy enemy => GetComponentInParent<GrimEnermy>();
    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] coliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);
        foreach (var hit in coliders)
        {
            if (hit.GetComponent<PlayerControll>() != null)
            {
                hit.GetComponent<PlayerControll>().Damage();
            }
        }
    }
}
