using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int Hitpoints = 1;
    public ParticleSystem DestroyEffect;

    public static event Action<Brick> OnBrickDestruction;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        ApplyCollisionLogic(ball);
    }

    private void ApplyCollisionLogic(Ball ball)
    {
        this.Hitpoints--;

        if(this.Hitpoints <= 0)
        {
            OnBrickDestruction?.Invoke(this);
            //SpawnDestroyEffect();
            Destroy(this.gameObject);
        } 
        else 
        {
            //sprite change
        }
    }
}
