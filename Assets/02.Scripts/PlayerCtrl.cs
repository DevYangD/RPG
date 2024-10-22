using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    private float hpValue;
    private float curhpValue;
    private int atkValue;
    private int defValue;
    private float spdValue;
    private float spValue;

    private int skillPoint;

    private static PlayerCtrl instance = null;
    private static readonly object padlock = new object();

    public Slider hpSlider;
    public Slider spSlider;
    public GameObject boss;
    

    private Vector3 knockbackVelocity;
    private bool isKnockback;
    public float knockbackForce = 100f; // ³Ë¹é °­µµ
    private float knockbackDuration = 0.2f; // ³Ë¹é Áö¼Ó ½Ã°£
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

    ThirdPersonController plctrl;
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
                // ³Ë¹é ¹æÇâÀ¸·Î ÀÌµ¿
                characterController.Move(knockbackVelocity * Time.deltaTime);
                knockbackTimer -= Time.deltaTime;
            }
            else
            {
                isKnockback = false; // ³Ë¹é Á¾·á
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
            spSlider.value = PlayerCtrl.Instance.SpValue / 5;
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
        if (plctrl.isShield) return;
        if(other.tag == "EnemyBullet")
        {
            if(!isDamage)
            {
                FireBall enemybullet = other.GetComponent<FireBall>();
                curhpValue = curhpValue - enemybullet.damage + defValue;
                CheckHp();
                StartCoroutine(OnDamage());
            }
        }
        if (other.tag == "Breath")
        {
            if (!isDamage)
            {
                curhpValue = curhpValue - 25 + defValue;
                CheckHp();
                StartCoroutine(OnDamage());
            }
        }
        if (other.tag == "Boss_MeleeAtk")
        {
            if(!isDamage)
            {
                ThirdPersonController ctrl = GetComponent<ThirdPersonController>();
                ctrl.Fall();
                curhpValue = curhpValue -30 + defValue;
                CheckHp();
                if (boss != null)
                {
                    knockbackVelocity = (transform.position - boss.transform.position).normalized * knockbackForce;
                    Debug.Log(knockbackVelocity);
                }

                // ³Ë¹é ¹ß»ý
                isKnockback = true;
                knockbackTimer = knockbackDuration;
                StartCoroutine(OnDamage());
            }
        }
        if (other.tag == "Boss_Field")
        {
            UIMgr ulmgr = FindObjectOfType<UIMgr>();
            Boss boss = FindObjectOfType<Boss>();
            ulmgr.BossTag();
            boss.isGo = true;
            SoundMgr.instance.StopBGM(BGM);
            SoundMgr.instance.PlayBGM(BossBGM);
            Destroy(other.gameObject);
        }
        if (other.tag == "First")
        {
            SoundMgr.instance.PlayBGM(BGM);
            Debug.Log("´ê¾Ò´Ù");
            Destroy(other.gameObject);
        }

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


}
