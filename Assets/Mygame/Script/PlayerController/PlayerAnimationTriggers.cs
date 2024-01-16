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
}
