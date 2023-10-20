using UnityEngine;

namespace lesta_academy
{
  public class TimeFreezingFunc : MonoBehaviour
  {
    public static void EnableFreezing()
    {
      Time.timeScale = 0.0f;
    }

    public static void DisableFreezing()
    {
      Time.timeScale = 1.0f;
    }
  }
}
