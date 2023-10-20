using System.Collections;

using UnityEngine;

namespace lesta_academy
{
  public class LavaTrapControl : MonoBehaviour
  {
    public static int damage;
    public static bool doEffects;

    private bool trapLogic_;
    private bool alreadyStarted_;
    private float delay_ = 0.4f;

    private void Start()
    {
      alreadyStarted_ = false;
      doEffects = false;
    }

    private void FixedUpdate()
    {
      StartCoroutine(DoLogic());
    }

    private IEnumerator DoLogic()
    {
      if (trapLogic_ && !alreadyStarted_)
      {
        alreadyStarted_ = true;

        while (trapLogic_)
        {
          damage = RandomDamage(damage);
          doEffects = true;
          
          yield return new WaitForSeconds(delay_);
        }
      }
    }

    private int RandomDamage(int damage)
    {
      damage = Random.Range(1, 5); // от 1 до 4

      return damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
      trapLogic_ = CollisionControl.IsItPlayer(collision, trapLogic_, true);
    }

    private void OnCollisionExit(Collision collision)
    {
      trapLogic_ = CollisionControl.IsItPlayer(collision, trapLogic_, false);
      alreadyStarted_ = false;
    }
  }
}
