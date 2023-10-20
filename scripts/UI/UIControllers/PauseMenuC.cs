using UnityEngine;

namespace lesta_academy
{
  public class PauseMenuC : MonoBehaviour
  {
    public GameObject pause;

    public static void OnClickRestart()
    {
      RestartFunc.Restart();
    }

    public static void OnClickQuit()
    {
      Application.Quit();
    }

    public void EnablePauseMenu()
    {
      pause.SetActive(true);
    }

    public void DisablePauseMenu()
    {
      pause.SetActive(false);
    }
  }
}
