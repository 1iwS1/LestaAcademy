using System.Collections;
using UnityEngine;

namespace lesta_academy
{
  public class DamageTrapControl : MonoBehaviour
  {
    [SerializeField] private MeshRenderer graphics;

    public static int damage;
    public static bool doDamage;
    private readonly float reloadSeconds_ = 5f;
    private readonly float damageSeconds_ = 1f;

    public static bool trapLogic_1;
    public static bool trapLogic_2;
    public static bool toReset;
    private static bool alreadyStarted_;

    private void Start()
    {
      alreadyStarted_ = false;
    }

    private void FixedUpdate()
    {
      StartCoroutine(Step_1());
      Reset();
    }

    private IEnumerator Step_1()
    {
      if (trapLogic_1)
      {
        trapLogic_1 = false;
        graphics.material.color = new Color(255, 69, 0);
        yield return new WaitForSeconds(damageSeconds_);

        StartCoroutine(Step_2());
      }
    }

    private IEnumerator Step_2()
    {
      if (trapLogic_2)
      {
        trapLogic_2 = false;
        graphics.material.color = new Color(255, 0, 0);
        damage = Random.Range(20, 30);
        doDamage = true;

        yield return new WaitForSeconds(0.6f); // чутка времени, чтобы красный цвет был немного подольше
        graphics.material.color = new Color(255, 255, 255);
        yield return new WaitForSeconds(reloadSeconds_);

        alreadyStarted_ = false;
        toReset = true;
        StartCoroutine(Step_1());
      }
    }

    public static void StartLogic()
    {
      if (!alreadyStarted_)
      {
        alreadyStarted_ = true;

        trapLogic_1 = true;
        trapLogic_2 = true;

        damage = 0;
        doDamage = false;
      }
    }

    private void Reset()
    {
      if (toReset)
      {
        toReset = false;
        alreadyStarted_ = false;

        trapLogic_1 = false;
        trapLogic_2 = false;

        damage = 0;
        doDamage = false;
        graphics.material.color = new Color(255, 255, 255); // исходный цвет платформы
      }
    }
  }
}
