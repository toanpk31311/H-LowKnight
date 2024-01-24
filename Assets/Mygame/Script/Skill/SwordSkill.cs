using UnityEngine;

public class SwordSkill : Skill
{
    public SwordType swordType = SwordType.Regular;
    [Header("Bounce info")]
    //[SerializeField] private UI_SkillTreeSlot bounceUnlockButton;
    [SerializeField] private int   bounceAmount;
    [SerializeField] private float bounceGravity;
    [SerializeField] private float bounceSpeed;
    [Header("Peirce info")]
    //[SerializeField] private UI_SkillTreeSlot pierceUnlockButton;
    [SerializeField] private int   pierceAmount;
    [SerializeField] private float pierceGravity;
    [Header("Spin info")]
    //[SerializeField] private UI_SkillTreeSlot spinUnlockButton;
    [SerializeField] private float hitCooldown = .35f;
    [SerializeField] private float maxTravelDistance = 7;
    [SerializeField] private float spinDuration = 2;
    [SerializeField] private float spinGravity = 1;


    [Header("Skill info")]

    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2    launchForce;
    [SerializeField] private float      swordGravity;
    [SerializeField] private float      freezeTimeDuration;
    [SerializeField] private float      returnSpeed;
    private Vector2 finalDr;

    [Header("Aim dots")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBeetwenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;
    private GameObject[] dots;
    public bool swordUnlocked { get; private set; }
    public enum SwordType
    {
        Regular,
        Bounce,
        Pierce,
        Spin
    }
    
    protected override void Start()
    {
        base.Start();

        GenerateDots();
    }

    public void CreateSword()
    { 
        
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        SwordSkillController newSwordScript = newSword.GetComponent<SwordSkillController>();

        if (swordType == SwordType.Bounce)
        {           
            newSwordScript.SetupBounce(true, bounceAmount,bounceSpeed);   
        }
        else if (swordType == SwordType.Pierce)
        {
            newSwordScript.SetupPierce(pierceAmount);
        }

        else if (swordType == SwordType.Spin)
            newSwordScript.SetupSpin(true, maxTravelDistance, spinDuration, hitCooldown);

        newSwordScript.SetUpSword(finalDr, swordGravity, player,freezeTimeDuration,returnSpeed);

        player.AssignNewSword(newSword);
        DotsActive(false);
    }
    private void SetupGraivty()
    {
        if (swordType == SwordType.Bounce)
            swordGravity = bounceGravity;
        else if (swordType == SwordType.Pierce)
            swordGravity = pierceGravity;
        else if (swordType == SwordType.Spin)
            swordGravity = spinGravity;
    }

    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
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
    #region Aimming
    public Vector2 AimDr()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 mosePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mosePos - playerPos;
        return direction;
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
    #endregion
}