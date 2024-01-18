using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
  private PlayerControll player => gameObject.GetComponentInParent<PlayerControll>();

   private void AnimationTrigger() 
    {
        player.AniamationTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] coliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach(var hit in coliders)
        {
            if (hit.GetComponent<Enemy>()!= null)
            {
                hit.GetComponent<Enemy>().Damage();
            }
        }
            
    }
}
