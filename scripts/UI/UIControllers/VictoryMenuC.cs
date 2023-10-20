using UnityEngine;

namespace lesta_academy
{
  public class VictoryMenuC : MonoBehaviour
  {
    public GameObject victory;
    public VictoryMenu vict;

    public static void OnClick()
    {
      RestartFunc.Restart();
    }

    public void SetTimeResult(string time)
    {
      vict.timeText.text = time;
    }

    public void EnableVictoryMenu()
    {
      victory.SetActive(true);
    }

    public void DisableVictoryMenu()
    {
      victory.SetActive(false);
    }
  }
}
