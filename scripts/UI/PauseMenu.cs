using UnityEngine;
using UnityEngine.UI;

namespace lesta_academy
{
  public class PauseMenu : UIComponent
  {
    [SerializeField] private PauseMenuSO pauseMenuData_;

    [SerializeField] private Image panel_;
    [SerializeField] private Text text_;

    [SerializeField] private Button restartButton_;
    private Text restartButtonText_;

    [SerializeField] private Button quitButton_;
    private Text quitButtonText_;

    public override void Setup()
    {
      restartButtonText_ = restartButton_.GetComponentInChildren<Text>();
      restartButton_.onClick.AddListener(PauseMenuC.OnClickRestart);

      quitButtonText_ = quitButton_.GetComponentInChildren<Text>();
      quitButton_.onClick.AddListener(PauseMenuC.OnClickQuit);
    }

    public override void Configure()
    {
      text_.text = pauseMenuData_.title;
      restartButtonText_.text = pauseMenuData_.restartButtonText;
      quitButtonText_.text = pauseMenuData_.quitButtonText;
    }
  }
}
