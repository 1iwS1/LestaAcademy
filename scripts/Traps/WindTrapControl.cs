using System.Collections;

using UnityEngine;

namespace lesta_academy
{
  public class WindTrapControl : MonoBehaviour
  {
    public static Vector3 windDirection;
    private float seconds_ = 2f;

    private void Start()
    {
      StartCoroutine(Countdown());
    }

    // счетчик 2 секунд до смены направления
    IEnumerator Countdown()
    {
      while (true)
      {
        ChangeDirection();

        yield return new WaitForSeconds(seconds_);
      }
    }

    private void ChangeDirection()
    {
      windDirection.x = Random.Range(-25, 25);
      windDirection.z = Random.Range(-25, 25);
    }
  }
}
