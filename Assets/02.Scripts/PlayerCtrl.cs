using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class PlayerCtrl : MonoBehaviour
{
    private float hpValue;
    private float curhpValue;
    private int atkValue;
    private int defValue;
    private float spdValue;
    public float spValue;

    private int skillPoint;

    private static PlayerCtrl instance = null;
    private static readonly object padlock = new object();

    public Slider hpSlider;
    public Slider spSlider;
    public GameObject boss;
    public GameObject melee_Hitted;
    public GameObject Fireball_Hit;
    public GameObject ShieldEffect;
    public GameObject fireSkillEffect;
    public GameObject bossClear;
    public GameObject finishE;
    public Transform finishEffectPos;

    private Vector3 knockbackVelocity;
    private bool isKnockback;
    public float knockbackForce = 100f; // 넉백 강도
    private float knockbackDuration = 0.2f; // 넉백 지속 시간
    private float knockbackTimer = 0f;


    [SerializeField]
    private string sword1;
    [SerializeField]
    private string sword2;
    [SerializeField]
    private string sword3;
    [SerializeField]
    private string msword1;
    [SerializeField]
    private string msword2;
    [SerializeField]
    private string msword3;
    [SerializeField]
    private string rollsound;
    [SerializeField]
    private string hitted;
    [SerializeField]
    private string BGM;
    [SerializeField]
    private string BossBGM;

    public bool isDamage;
    SkinnedMeshRenderer mesh;
    public CharacterController characterController;
    private CinemachineVirtualCamera _cam;

    public float defaultFOV = 40f;
    public float zoomFOV = 30f;
    public float zoomSpeed = 5f;

    private Coroutine zoomCoroutine;

    ThirdPersonController plctrl;
    CameraShake cameraShake;
    public float HpValue { get => hpValue; set => hpValue = value; }
    public float CurHpValue { get => curhpValue; set => curhpValue = value; }
    public int AtkValue { get => atkValue; set => atkValue = value; }
    public int DefValue { get => defValue; set => defValue = value; }
    public float SpdValue { get => spdValue; set => spdValue = value; }
    public float SpValue { get => spValue; set => spValue = value; }
    public int SkillPoint { get => skillPoint; set => skillPoint = value; }

    private PlayerCtrl() { }

    public static PlayerCtrl Instance
    {
        get
        {
           
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerCtrl>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<PlayerCtrl>();
                    singletonObject.name = typeof(PlayerCtrl).ToString();
                }

                // singleTon instance scene moving, can't not destory
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    public static void InitialSettings()
    {
        instance.hpValue = 200;
        instance.curhpValue = 200;
        instance.atkValue = 10;
        instance.defValue = 5;
        instance.spdValue = 9;
        instance.spValue = 5;
        instance.skillPoint = 5;
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            InitialSettings();

            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        characterController = GetComponent<CharacterController>();
        plctrl = GetComponent<ThirdPersonController>();
        cameraShake = FindObjectOfType<CameraShake>();
        _cam = FindObjectOfType<CinemachineVirtualCamera>();

        instance.hpValue = 200;
        instance.curhpValue = 200;
        instance.atkValue = 10;
        instance.defValue = 5;
        instance.spdValue = 9;
        instance.spValue = 5;
        instance.skillPoint = 5;

        

    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {
            if (SpValue > 0)
            {
                SpValue -= (Time.deltaTime) * 2;
                CheckSp();
            }
            if(SpValue <=0)
            {
                SpValue = 0;
                CheckSp();
            }
        }
        else
        {
            SpValue += Time.deltaTime;
            if (SpValue >= 5)
                SpValue = 5;
            CheckSp();
        }

        if (plctrl.isShield) return;
        if (isKnockback)
        {
            if (knockbackTimer > 0)
            {
                // 넉백 방향으로 이동
                characterController.Move(knockbackVelocity * Time.deltaTime);
                knockbackTimer -= Time.deltaTime;
            }
            else
            {
                isKnockback = false; // 넉백 종료
            }
        }
    }

    public void CheckHp()
    {
        if (hpSlider != null)
        {
            hpSlider.value = PlayerCtrl.Instance.CurHpValue / PlayerCtrl.Instance.HpValue;
        }
    }

    public void HpPotion(int potion)
    {
        curhpValue += potion;
        if(curhpValue >= hpValue)
            curhpValue = hpValue;
    }

    public void CheckSp()
    {
        if (spSlider != null)
        {
            spSlider.value = SpValue / 5;
        }

        if(SpdValue <=0)
        {
            ThirdPersonController ctrl = FindObjectOfType<ThirdPersonController>();
            if(ctrl != null)
            {
                ctrl.DeactiveBlock();
            }

        }
    }
    void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "EnemyBullet")
        {
            if(plctrl.isShield)
            {
                StartCoroutine(HitDamage());
                GameObject shieldEffect = Instantiate(ShieldEffect, other.transform.position, other.transform.rotation);
                Destroy(shieldEffect, 1f);
            }
            
            if(!isDamage &&!plctrl.isShield)
            {
                GameObject fireball_HitEffect = Instantiate(Fireball_Hit, other.transform.position, other.transform.rotation);
                Destroy(fireball_HitEffect, 0.5f);
                FireBall enemybullet = other.GetComponent<FireBall>();
                curhpValue = curhpValue - enemybullet.damage + defValue;
                CheckHp();
                StartCoroutine(OnDamage());
                StartCoroutine(HitDamage());
                
            }
        }
        if (other.tag == "Breath")
        {
            if (!isDamage)
            {
                StartCoroutine(FireSkillHit());
                StartCoroutine(HitDamage());

            }
        }
        if (other.tag == "Boss_MeleeAtk")
        {
            if(!isDamage)
            {

                GameObject MeleeHit = Instantiate(melee_Hitted, transform.position, transform.rotation);
                Destroy(MeleeHit, 1f);
                ThirdPersonController ctrl = GetComponent<ThirdPersonController>();
                ctrl.Fall();
                
                curhpValue = curhpValue -30 + defValue;
                CheckHp();
                if (boss != null)
                {
                    knockbackVelocity = (transform.position - boss.transform.position).normalized * knockbackForce;
                    Debug.Log(knockbackVelocity);
                }

                // 넉백 발생
                isKnockback = true;
                knockbackTimer = knockbackDuration;
                StartCoroutine(OnDamage());
                cameraShake.MeleeDoShake();
            }
        }
        if (other.tag == "Boss_Field")
        {
            UIMgr ulmgr = FindObjectOfType<UIMgr>();
            Boss boss = FindObjectOfType<Boss>();
            UI_MiniMap minimap = FindObjectOfType<UI_MiniMap>();
            StartCoroutine(BossPopUp());
            minimap.textMapName.text = "BOSS_DRAGON";
            ulmgr.BossTag();
            boss.isGo = true;
            SoundMgr.instance.StopBGM(BGM);
            SoundMgr.instance.PlayBGM(BossBGM);
            Destroy(other.gameObject);
        }
        if (other.tag == "First")
        {
            UI_MiniMap minimap = FindObjectOfType<UI_MiniMap>();
            minimap.textMapName.text = "STARTING";
            SoundMgr.instance.PlayBGM(BGM);
            Debug.Log("닿았다");
            Destroy(other.gameObject);
        }
        if(other.tag == "Damping")
        {
            
            StartCoroutine(StartDamp());
            
        }

    }

    IEnumerator StartDamp()
    {
        
        cameraShake.StartDamping();
        yield return new WaitForSeconds(1f);
        cameraShake.EndDamping();
    }

    IEnumerator OnDamage()
    {
        SoundMgr.instance.PlaySE(hitted);
        isDamage = true;
        mesh.material.color = Color.yellow;
        yield return new WaitForSeconds(1f);

        isDamage = false;
        mesh.material.color = Color.white;
        
    }

    IEnumerator HitDamage()
    {
        cameraShake.HitShake();
        yield return new WaitForSeconds(0.5f);
        cameraShake.StopShake();
    }

    public void SwordSound1()
    {
        SoundMgr.instance.PlaySE(sword1);
    }
    public void SwordSound2()
    {
        SoundMgr.instance.PlaySE(sword2);
    }
    public void SwordSound3()
    {
        SoundMgr.instance.PlaySE(sword3);
    }
    public void mSound1()
    {
        SoundMgr.instance.PlaySE(msword1);
    }
    public void mSound2()
    {
        SoundMgr.instance.PlaySE(msword2);
    }
    public void mSound3()
    {
        SoundMgr.instance.PlaySE(msword3);
    }
    public void Rollsound()
    {
        SoundMgr.instance.PlaySE(rollsound);
    }

    public void meleeAtkShake()
    {
        cameraShake.DoShake();
    }

    public void meleeFinish()
    {
        cameraShake.FinishShake();
        GameObject finisheffect = Instantiate(finishE, finishEffectPos.position, finishEffectPos.rotation);
        Destroy(finisheffect, 1f);
    }
    public void DoStopShake()
    {
        cameraShake.StopShake();
    }

    public void FinishZoomStart()
    {
        // 기존 줌 코루틴이 있으면 중지
        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
        }

        // 줌 인 코루틴 시작
        zoomCoroutine = StartCoroutine(SmoothZoom(zoomFOV));
    }

    public void FinishZoomEnd()
    {
        // 기존 줌 코루틴이 있으면 중지
        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
        }

        // 줌 아웃 코루틴 시작
        zoomCoroutine = StartCoroutine(SmoothZoom(defaultFOV));
    }

    public IEnumerator SmoothZoom(float targetFOV)
    {
        float startFOV = _cam.m_Lens.FieldOfView;

        float elapT = 0f;
        while(elapT <1f)
        {
            _cam.m_Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, elapT);
            elapT += Time.deltaTime * zoomSpeed;
            yield return null;
        }
        _cam.m_Lens.FieldOfView = targetFOV;
    }

    IEnumerator FireSkillHit()
    {
        SoundMgr.instance.PlaySE(hitted);
        isDamage = true;
        fireSkillEffect.SetActive(true);
        curhpValue = curhpValue - 10 + defValue;
        CheckHp();
        yield return new WaitForSeconds(0.2f);
        curhpValue = curhpValue - 10 + defValue;
        CheckHp();
        yield return new WaitForSeconds(0.2f);
        curhpValue = curhpValue - 10 + defValue;
        CheckHp();
        yield return new WaitForSeconds(0.6f);
        isDamage = false;
        fireSkillEffect.SetActive(false);
    }

    IEnumerator BossPopUp()
    {
        bossClear.SetActive(true);
        yield return new WaitForSeconds(2f);
        bossClear.SetActive(false);
    }


}
