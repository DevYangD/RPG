using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Boss : MonoBehaviour
{
    public int MaxHp;
    public int curHp;
    public BoxCollider meleeArea;
    public Transform[] bulletPos;
    public bool isAttack;
    public bool isDead;
    public bool isGo = false;
    BoxCollider boxCollider;
    public GameObject flame;
    public GameObject fireSkill;
    public Transform target;
    public Slider bossHpSlider;
    public GameObject bloodE;
    public GameObject hitE;
    public GameObject clear;
    public float DeadT = 2f;

    [SerializeField]
    private string Fireball;
    [SerializeField]
    private string fly;
    [SerializeField]
    private string fireskill;
    [SerializeField]
    private string died;
    [SerializeField]
    private string claw;
    [SerializeField]
    private string mhit;


    Vector3 lookVec;    // 예측공격
    

    public bool isLook;
    public bool isHit;
    
    Rigidbody rigid;
    Material mat;
    NavMeshAgent nav;
    Animator anim;

    SkinnedMeshRenderer mesh;

    private void Awake()
    {
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        bulletPos = GameObject.FindGameObjectsWithTag("BulletPos").Select(obj => obj.transform).ToArray();
        StartCoroutine(WaitforGO());
        //StartCoroutine(Think());

    }
 

    // Update is called once per frame
    void Update()
    {
        
        if(isDead)
        {
            StopAllCoroutines();

            DeadT -=Time.deltaTime;
            if(DeadT <=0)
            {

                if(Input.anyKey)
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 플레이 모드 종료
#else
                Application.Quit(); // 빌드된 게임에서 종료
#endif
                }
            }
            return;
        }
        if (isLook)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h, 0, v) * 5f;
            transform.LookAt(target.position + lookVec);
            
        }

    }

    IEnumerator Think()
    {
        SoundMgr.instance.StopSE(fly);
        yield return new WaitForSeconds(0.5f);

        int ranAct = Random.Range(0, 5);
        switch (ranAct)
        {
            case 0:
            case 1:
                StartCoroutine(Claw());
                break;
            case 2:
            case 3:
                StartCoroutine(Flame());
                break;
            case 4:
                StartCoroutine(Missile());
                break;
        }
    }

    IEnumerator Claw()
    {
        
        isLook = false;
        anim.SetTrigger("doClaw");
        SoundMgr.instance.PlaySE(claw);
        yield return new WaitForSeconds(3f);
        isLook = true;
        StartCoroutine(Think());
    }

    IEnumerator Missile()
    {
        anim.SetTrigger("doFlyAtk");
        boxCollider.enabled = false;
        SoundMgr.instance.PlaySE(fly);
        yield return new WaitForSeconds(2.5f);
        SoundMgr.instance.StopSE(fly);
        for (int i = 0; i < bulletPos.Length; i++)
        {
            
            GameObject instanMissile = Instantiate(flame, bulletPos[i].position, bulletPos[i].rotation);
            FireBall fireball = instanMissile.GetComponent<FireBall>();
            fireball.target = target;
 
        }
        SoundMgr.instance.PlaySE(Fireball);
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < bulletPos.Length; i++)
        {
            
            GameObject instanMissile = Instantiate(flame, bulletPos[i].position, bulletPos[i].rotation);
            FireBall fireball = instanMissile.GetComponent<FireBall>();
            fireball.target = target;
  
        }
        SoundMgr.instance.PlaySE(Fireball);
        SoundMgr.instance.PlaySE(fly);
        yield return new WaitForSeconds(3f);
        boxCollider.enabled = true;
        StartCoroutine(Think());
    }

    IEnumerator Flame()
    {
        isLook = false;
        anim.SetTrigger("doFlame");
        SoundMgr.instance.PlaySE(fireskill);
        yield return new WaitForSeconds(2.4f);
        isLook = true;
        StartCoroutine(Think());
    }

    void OnClawAtk()
    {
        meleeArea.enabled = true;
    }

    void ExitClawAtk()
    {
        meleeArea.enabled = false;
    }

    void OnFlameAtk()
    {
        fireSkill.SetActive(true);
    }

    void ExitFlameAtk()
    {
        fireSkill.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Melee")
        {
            if(!isHit)
            {
                Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
                Damage_Ctrl.Instance.CreateDamageText(pos, PlayerCtrl.Instance.AtkValue);
                GameObject inIt = Instantiate(bloodE, other.transform.position, other.transform.rotation );
                GameObject inIt2 = Instantiate(hitE, other.transform.position, other.transform.rotation );
                Destroy( inIt ,1f);
                Destroy( inIt2 ,1f);

                StartCoroutine(OnHit()); 
            }
        }
    }

    IEnumerator OnHit()
    {
        
        if (!isDead &&curHp >0)
        {
            isHit = true;
            curHp -= PlayerCtrl.Instance.AtkValue;
            mesh.material.SetColor("_BaseColor", Color.gray);
            SoundMgr.instance.PlaySE(mhit);
            CheckBossHp();
            Time.timeScale = 0.7f;
            yield return new WaitForSeconds(0.5f);
            isHit = false;
            mesh.material.SetColor("_BaseColor", Color.white);
            Time.timeScale = 1.0f;
        }

        else if (!isDead && curHp <= 0)
        {
            CheckBossHp();
            StartCoroutine(Die());
        }

    }

    IEnumerator WaitforGO()
    {
        yield return new WaitWhile(() => isGo == false);
        StartCoroutine(Think());
    }
    public IEnumerator Die()
    {
        SoundMgr.instance.PlaySE(died);
        fireSkill.SetActive(false);
        anim.SetTrigger("doDie");
        Time.timeScale = 0.2f;
        yield return new WaitForSeconds(1f);
        isDead = true;
        Debug.Log("DIE!!");
        Time.timeScale = 1f;
        clear.SetActive(true);
        

    }

    public void CheckBossHp()
    {
        if (bossHpSlider != null)
        {
            bossHpSlider.value = curHp;
        }
    }
}
