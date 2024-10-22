using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type
    {
        Mell,
        Range
    }
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    public TrailRenderer trailEffect2;
    public TrailRenderer trailEffect3;


    public void Use()
    {
        if(type == Type.Mell)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
    }

    public void Finishing()
    {
        if(type == Type.Mell)
        {
            StopCoroutine("Finish");
            StartCoroutine("Finish");
        }
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
        
    }
    IEnumerator Finish()
    {
        yield return new WaitForSeconds(1f);
        meleeArea.enabled = true;
        trailEffect2.enabled = true;
        trailEffect3.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect2.enabled = false;
        trailEffect3.enabled= false;

    }

}
