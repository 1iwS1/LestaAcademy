using UnityEngine;

namespace lesta_academy
{
  [CreateAssetMenu(menuName = "UI/PauseMenuSO", fileName = "PauseMenuSO")]
  public class PauseMenuSO : ScriptableObject
  {
    public string title;
    public string restartButtonText;
    public string quitButtonText;
  }
}
