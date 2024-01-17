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
}
