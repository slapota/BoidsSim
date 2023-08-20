using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public List<Boid> PRBoids = new List<Boid>();
    public List<Boid> VRBoids = new List<Boid>();
    public Mother mom;
    public float pRange;
    public float vRange;
    public float xV;
    public float yV;
    public float avoidFactor;
    public float matchingFactor;
    public float centeringFactor;
    public float turnFactor;
    public float minV;
    public float maxV;
    public float overAllSpeed;
    public float edgeMargin;

    private void Start()
    {
        //StartCoroutine(Edge());
        StartCoroutine(FindBoids());
    }
    private void Update()
    {
        Edge();

        if (Mathf.Abs(xV) + Mathf.Abs(yV) < minV)
        {
            xV *= 1.1f;
            yV *= 1.1f;
        }

        transform.position += new Vector3(xV, yV, 0) * Time.deltaTime * overAllSpeed;
    }
    /*IEnumerator Edge()
    {
        yield return new WaitUntil(()=>Mathf.Abs(transform.localPosition.y) > 560 || Mathf.Abs(transform.localPosition.x) > 980);
        transform.localPosition *= -1;
        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(Edge());
    }*/
    void Edge()
    {
        if(transform.localPosition.x < -960 + edgeMargin)
        {
            xV += turnFactor;
        }else if(transform.localPosition.x > 960 - edgeMargin)
        {
            xV -= turnFactor;
        }
        if (transform.localPosition.y < -540 + edgeMargin)
        {
            yV += turnFactor;
        }
        else if (transform.localPosition.y > 540 - edgeMargin)
        {
            yV -= turnFactor;
        }
    }
    void MakeFriends()
    {
        float xpos_avg = 0;
        float ypos_avg = 0;

        foreach(Boid b in VRBoids)
        {
            xpos_avg += b.transform.position.x;
            ypos_avg += b.transform.position.y;
        }

        if(VRBoids.Count > 0)
        {
            xpos_avg /= VRBoids.Count;
            ypos_avg /= VRBoids.Count;

            xV += (xpos_avg - transform.position.x) * centeringFactor;
            yV += (ypos_avg - transform.position.y) * centeringFactor;
        }
    }
    void ChangeDir()
    {
        float xvel_avg = 0;
        float yvel_avg = 0;

        foreach(Boid b in VRBoids)
        {
            xvel_avg += b.xV;
            yvel_avg += b.yV;
        }

        if(VRBoids.Count > 0)
        {
            xvel_avg /= VRBoids.Count;
            yvel_avg /= VRBoids.Count;

            xV += (xvel_avg - xV) * matchingFactor;
            yV += (yvel_avg - yV) * matchingFactor;
        }
    }
    void ChangeV()
    {
        float close_dx = 0;
        float close_dy = 0;

        foreach(Boid b in PRBoids)
        {
            close_dx += transform.position.x - b.transform.position.x;
            close_dy += transform.position.y - b.transform.position.y;

            xV += close_dx * avoidFactor;
            yV += close_dy * avoidFactor;
        }

        xV = Mathf.Clamp(xV, -maxV, maxV);
        yV = Mathf.Clamp(yV, -maxV, maxV);
    }
    IEnumerator FindBoids()
    {
        yield return new WaitForSeconds(0.1f);
        PRBoids.Clear();
        VRBoids.Clear();
        foreach(Boid b in mom.boids)
        {
            if(Vector2.Distance(b.transform.position, transform.position) <= vRange)
            {
                VRBoids.Add(b);
            }
        }
        VRBoids.Remove(this);
        for(int i = VRBoids.Count - 1; i >= 0; i--)
        {
            if (Vector2.Distance(VRBoids[i].transform.position, transform.position) <= pRange)
            {
                PRBoids.Add(VRBoids[i]);
          
            }
        }
        PRBoids.Remove(this);
        ChangeV();
        ChangeDir();
        MakeFriends();
        StartCoroutine(FindBoids());
    }
}
