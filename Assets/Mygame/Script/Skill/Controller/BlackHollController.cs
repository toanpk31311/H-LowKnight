using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHollController : MonoBehaviour
{
    [SerializeField]private float maxSize;
    [SerializeField] private float growSpeed;

    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodeList;

    private float shrinkSpeed;
    private float blackholeTimer;

    private bool canGrow = true;
    private bool canShrink;
    private bool canCreateHotKeys = true;
    private bool cloneAttackReleased;
    private bool playerCanDisapear = true;

    private bool canAttack;

    private int amountOfAttacks = 4;
    private float cloneAttackCooldown = .3f;
    private float cloneAttackTimer;
    [SerializeField] private List<Transform> targets = new List<Transform>();
    private void Update()
    {

        cloneAttackTimer -= Time.deltaTime;
        blackholeTimer -= Time.deltaTime;
        //if (blackholeTimer < 0)
        //{
        //    //blackholeTimer = Mathf.Infinity;

        //    //if (targets.Count > 0)
        //    //    ReleaseCloneAttack();
        //    //else
        //    //    FinishBlackHoleAbility();
       // }
        if (Input.GetKeyDown(KeyCode.F))
        {
           canAttack = true;
        }
        float xOffset;

        if (Random.Range(0, 100) > 50)
            xOffset = 2;
        else
            xOffset = -2;

        if (cloneAttackTimer < 0 && canAttack)
           
        {   
            cloneAttackTimer = cloneAttackCooldown;
            SkillManager.instance.clone.CreateClone(targets[Random.Range(0, targets.Count)],new Vector3( xOffset,0));
            amountOfAttacks--;
            if(amountOfAttacks<=0)
            {
                canAttack=false;
            }
        }

        if (canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize,maxSize),growSpeed*Time.deltaTime);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<GroundOnlyEnemy>()!=null)
        {
            collision.GetComponent<GroundOnlyEnemy>().FreezeTime(true);

            CreateHotKey(collision);
        }
    }

    private void CreateHotKey(Collider2D collision)

    {
        if (keyCodeList.Count <= 0)
        {
            Debug.Log("BlackhollCOntroller");
            return;
        }
        GameObject newHotKey = Instantiate(hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);

        KeyCode chossenKey = keyCodeList[Random.Range(0, keyCodeList.Count)];

        keyCodeList.Remove(chossenKey);
        BlackHoleHotKeyController newHotKeyScript = newHotKey.GetComponent<BlackHoleHotKeyController>();
        newHotKeyScript.SetupHotKey(chossenKey, collision.transform, this);
    }
    public void AddEnemyToList(Transform _enemyTransform) => targets.Add(_enemyTransform);
}
