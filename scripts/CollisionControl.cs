using UnityEngine;

namespace lesta_academy
{
  public class CollisionControl : MonoBehaviour
  {
    public static bool IsGroundedCheck(Transform groundChecker, float groundDistance, LayerMask groundMask)
    {
      return Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);
    }

    public static bool IsOnWindtrapCheck(Collider other, bool isOnWindtrap, bool temp)
    {
      if (other.gameObject.tag == "WindTrap")
      {
        isOnWindtrap = temp;
      }

      return isOnWindtrap;
    }

    public static bool IsOnDamagetrapCheck(Collision collision, bool isOnDamagetrap, bool temp)
    {
      if (collision.gameObject.tag == "DamageTrap")
      {
        isOnDamagetrap = temp;
      }

      return isOnDamagetrap;
    }

    public static bool IsOnLavatrapCheck(Collision collision, bool isOnLavagetrap, bool temp)
    {
      if (collision.gameObject.tag == "LavaTrap")
      {
        isOnLavagetrap = temp;
      }

      return isOnLavagetrap;
    }

    public static bool IsFellCheck(Collider other, bool isFell, bool temp)
    {
      if (other.gameObject.tag == "FallTrigger")
      {
        isFell = temp;
      }

      return isFell;
    }

    public static bool IsFinishedCheck(Collider other, bool isFinished, bool temp)
    {
      if (other.gameObject.tag == "FinishZone")
      {
        isFinished = temp;
      }

      return isFinished;
    }

    public static bool IsStartedCheck(Collider other, bool isStarted, bool temp)
    {
      if (other.gameObject.tag == "StartZone")
      {
        isStarted = temp;
      }

      return isStarted;
    }

    public static bool IsItPlayer(Collision collision, bool trapLogic, bool temp)
    {
      if (collision.gameObject.tag == "Player")
      {
        trapLogic = temp;
      }

      return trapLogic;
    }
  }
}
