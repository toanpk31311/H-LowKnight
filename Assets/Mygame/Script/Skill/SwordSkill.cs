using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SwordSkill : Skill
{
    [Header("Skill info")]

    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;
    [SerializeField] private float freezeTimeDuration;
    [SerializeField] private float returnSpeed;
    private Vector2 finalDr;

    [Header("Aim dots")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBeetwenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;
    private GameObject[] dots;
    public bool swordUnlocked { get; private set; }
    protected override void Start()
    {
        base.Start();

        GenerateDots();
    }
        public void CreateSword()
    {   

        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        SwordSkillController newSwordScript = newSword.GetComponent<SwordSkillController>();
        newSwordScript.SetUpSword(finalDr, swordGravity,player);
        player.AssignNewSword(newSword);
        DotsActive(false);
    }
    public Vector2 AimDr()
    {
        Vector2 playerPos= player.transform.position;
        Vector2 mosePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mosePos - playerPos;
        return direction;
    }
    protected override void Update()
    {
        if(Input.GetKeyUp(KeyCode.R)) {
            finalDr = new Vector2(AimDr().normalized.x * launchForce.x, AimDr().normalized.y * launchForce.y);
            
        }
        if (Input.GetKey(KeyCode.R))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBeetwenDots);
            }
        }
    }
    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }
    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }
    private Vector2 DotsPosition(float t)
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * t + .5f * (Physics2D.gravity * swordGravity) * (t * t);

        return position;
    }
 
    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

}