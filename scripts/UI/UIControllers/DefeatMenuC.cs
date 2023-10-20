using UnityEngine;

namespace lesta_academy
{
  public class DefeatMenuC : MonoBehaviour
  {
    public GameObject defeat;

    public static void OnClick()
    {
      RestartFunc.Restart();
    }

    public void EnableDefeatMenu()
    {
      defeat.SetActive(true);
    }

    public void DisableDefeatMenu()
    {
      defeat.SetActive(false);
    }
  }
}
