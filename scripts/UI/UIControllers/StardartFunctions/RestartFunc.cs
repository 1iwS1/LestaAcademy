using UnityEngine;
using UnityEngine.SceneManagement;

namespace lesta_academy
{
  public class RestartFunc : MonoBehaviour
  {
    public static void Restart()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
  }
}
