using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mother : MonoBehaviour
{
    public Boid boid;
    public float spread;
    public Transform canvas;
    public int startCount;

    private void Start()
    {
        Spawn(Vector2.zero, startCount);
    }
    void Spawn(Vector2 basePos, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Boid newBoid = Instantiate(boid, canvas);
            newBoid.transform.localPosition = basePos + new Vector2(Random.Range(-spread, spread), Random.Range(-spread, spread));
        }
    }
}
