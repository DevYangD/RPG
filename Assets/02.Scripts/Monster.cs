using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{

    public Transform target;
    NavMeshAgent nav;
    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    void MonsterAI()
    {
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (distance > 1.3f)
        {
            nav.SetDestination(target.transform.position);
        }
        else if (distance <= 1.3f)
        {
            Stat targetStats = target.GetComponent<Stat>();
            if (targetStats != null)
            {
                Attack(targetStats);
            }
        }

        else
        {
            nav.SetDestination(transform.position);
        }
    }

    public void Attack(Stat target)
    {

    }
}
