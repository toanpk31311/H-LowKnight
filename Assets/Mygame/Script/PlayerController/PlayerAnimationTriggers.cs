using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
  private Player player => gameObject.GetComponentInParent<Player>();

   private void AnimationTrigger() 
    {
        player.AniamationTrigger();
    }
    private void AttackTrigger()
    {
        Collider2D[] coliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach(var hit in coliders)
        {
            if (hit.GetComponent<GroundOnlyEnemy>()!= null)
            {
                hit.GetComponent<GroundOnlyEnemy>().Damage();
            }
        }
            
    }
    private void ThrowSword()
    {
        SkillManager.instance.Sword.CreateSword();
    }
}
