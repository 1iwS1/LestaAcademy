using UnityEngine;
using UnityEngine.UI;

namespace lesta_academy
{
  public class VictoryMenu : UIComponent
  {
    [SerializeField] private VictoryMenuSO victoryMenuData_;

    [SerializeField] private Image panel_;
    [SerializeField] private Text text_;
    [SerializeField] private Button restartButton_;
    private Text restartButtonText_;
    
    public Text timeText;

    public override void Setup()
    {
      restartButtonText_ = restartButton_.GetComponentInChildren<Text>();
      restartButton_.onClick.AddListener(DefeatMenuC.OnClick);
    }

    public override void Configure()
    {
      text_.text = victoryMenuData_.title;
      restartButtonText_.text = victoryMenuData_.buttonText;
    }
  }
}
