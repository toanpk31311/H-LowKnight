using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHollController : MonoBehaviour
{
    private float maxSize;
    private float growSpeed;

    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodeList;
     private List<GameObject> createdHotKey = new List<GameObject>();

    private float shrinkSpeed;
    private float blackholeTimer;

    [SerializeField] private bool canGrow ;
    private bool canShrink;
    private bool canCreateHotKeys = true;
    private bool cloneAttackReleased;
    private bool playerCanDisapear = true;

    private bool CloneAttackRelease;
    private bool canCreateHotkey = true;
    private int amountOfAttacks = 4;
    private float cloneAttackCooldown = .3f;
    private float cloneAttackTimer;
    [SerializeField] private List<Transform> targets = new List<Transform>();
    public void SetupBlackhole(float _maxSize, float _growSpeed, float _shrinkSpeed, int _amountOfAttacks, float _cloneAttackCooldown/* ,float _blackholeDuration*/)
    {
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        shrinkSpeed = _shrinkSpeed;
        amountOfAttacks = _amountOfAttacks;
        cloneAttackCooldown = _cloneAttackCooldown;

        //blackholeTimer = _blackholeDuration;


        //if (SkillManager.instance.clone.crystalInseadOfClone)
        //    playerCanDisapear = false;
    }
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
            ReleaseCloneAttack();
        }

        CloneAttackLogic();

        if (canGrow && !canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);

        }
        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);
            if (transform.localScale.x < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void ReleaseCloneAttack()
    {
        DestroyHotKeys();
        CloneAttackRelease = true;
        canCreateHotkey = false;
        PlayerManager.instance.player.MakeTransprent(true);
    }

    private void CloneAttackLogic()
    {
        if (cloneAttackTimer < 0 && CloneAttackRelease)

        {
            cloneAttackTimer = cloneAttackCooldown;
            float xOffset;

            if (Random.Range(0, 100) > 50)
                xOffset = 2;
            else
                xOffset = -2;
            SkillManager.instance.clone.CreateClone(targets[Random.Range(0, targets.Count)], new Vector3(xOffset, 0));
            amountOfAttacks--;
            if (amountOfAttacks <= 0)
            {
               Invoke("FinishBlackHole",0.9f);
            }
        }
    }

    private void FinishBlackHole()
    {
        PlayerManager.instance.player.ExitBlackHoleState();
        canShrink = true;
        CloneAttackRelease = false;
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
        if (!canCreateHotkey)
        {
            return;
        }
        GameObject newHotKey = Instantiate(hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity);
        createdHotKey.Add(newHotKey);
        KeyCode chossenKey = keyCodeList[Random.Range(0, keyCodeList.Count)];

        keyCodeList.Remove(chossenKey);
        BlackHoleHotKeyController newHotKeyScript = newHotKey.GetComponent<BlackHoleHotKeyController>();
        newHotKeyScript.SetupHotKey(chossenKey, collision.transform, this);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<GroundOnlyEnemy>() != null)
            collision.GetComponent<GroundOnlyEnemy>().FreezeTime(false);
    }

    public void AddEnemyToList(Transform _enemyTransform) => targets.Add(_enemyTransform);
    private void DestroyHotKeys()
    {
        if (createdHotKey.Count <= 0)
            return;

        for (int i = 0; i < createdHotKey.Count; i++)
        {
            Destroy(createdHotKey[i]);
        }
    }
 }
