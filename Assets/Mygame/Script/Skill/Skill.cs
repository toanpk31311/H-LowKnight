using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float cooldown;
    public float cooldownTimer;

    protected Player player;


    protected virtual void Start()
    {
        player = PlayerManager.instance.player;

        CheckUnlock();

    }

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual void UseSkill()
    {
        // do some skill spesific things
    }
    protected virtual void CheckUnlock()
    {

    }
    public virtual bool CanUseSkill()
    {
        if (cooldownTimer < 0)
        {
            UseSkill();
            cooldownTimer = cooldown;
            return true;
        }
        return false;
    }
   

}
