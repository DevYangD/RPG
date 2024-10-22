using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private float hpValue;
    private float curhpValue;
    private int atkValue;
    private int defValue;
    private float spdValue;
    private float spValue;


    private int skillPoint;

    private static PlayerStatus instance = null;
    private static readonly object padlock = new object();

    public float HpValue { get=> hpValue; set => hpValue = value; }
    public float CurHpValue { get => curhpValue; set => curhpValue = value; }
    public int AtkValue { get=> atkValue; set => atkValue = value; }
    public int DefValue { get=>defValue; set => defValue = value; }
    public float SpdValue { get=>spdValue; set => spdValue = value; }
    public float SpValue { get => spValue; set => spValue = value; }
    public int SkillPoint { get=>skillPoint; set => skillPoint = value; }


    private PlayerStatus() { }

    public static PlayerStatus Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<PlayerStatus>();

                if(instance != null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<PlayerStatus>();
                    singletonObject.name = typeof(PlayerStatus).ToString();
                }

                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private static void InitialSettings()
    {
        instance.hpValue = 200;
        instance.curhpValue = 200;
        instance.atkValue = 10;
        instance.defValue = 5;
        instance.spdValue = 2;
        instance.spValue = 5;
        instance.skillPoint = 5;
    }

    void Awake()
    {

        if(instance ==null)
        {
            instance = this;

            InitialSettings();

            DontDestroyOnLoad(this.gameObject);
        }
        else if( instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"{skillPoint} , {SkillPoint}");
    }
}
