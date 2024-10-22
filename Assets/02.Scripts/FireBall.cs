using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireBall : MonoBehaviour
{
    Rigidbody rb;
    public int damage;
    public Transform target;

    [SerializeField]
    float m_spd = 0f;
    float m_curSpd = 0f;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Terrain")
        {
            Destroy(gameObject) ;
        }

        if(other.gameObject.tag == "Player")
        {
            Debug.Log("¸Â¾Òµû");
            Destroy(gameObject) ;
        }
    }
    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            if(m_curSpd <= m_spd)
            {
                m_curSpd += m_spd * Time.deltaTime;
            }
            transform.position += transform.forward * m_curSpd * Time.deltaTime;

            Vector3 dir = (target.position - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, dir, 0.25f);

        }

        Destroy(gameObject, 5f);
    }
}
