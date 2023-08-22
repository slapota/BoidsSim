using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mother : MonoBehaviour
{
    public Slider avoidSlider, matchingSlider, centeringSlider;
    public List<Boid> boids = new List<Boid>();
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
            newBoid.transform.eulerAngles += Vector3.forward * Random.Range(-180f, 180f);
            newBoid.mom = this;
            newBoid.avoidSlider = avoidSlider;
            newBoid.matchingSlider = matchingSlider;
            newBoid.centeringSlider = centeringSlider;
            boids.Add(newBoid);
        }
    }
}
