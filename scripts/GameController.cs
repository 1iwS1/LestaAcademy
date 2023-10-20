using System.Collections;

using UnityEngine;

namespace lesta_academy
{
  public class GameController : MonoBehaviour
  {
    public PauseMenuC pauseC;
    public DefeatMenuC defeatC;
    public VictoryMenuC victoryC;
    public HealthTextC healthC;

    private GameState gameState_;
    private int counter_;
    private string timePassing_;

    public static bool isStartedCount;
    public static bool toStart;

    private void Start()
    {
      SetGameState(GameState.IN_PROCESS);

      counter_ = 0;
      timePassing_ = "";
      isStartedCount = false;
      toStart = false;
    }

    private void Update()
    {
      GameStateDetermination();

      StartCoroutine(GameCounter());
    }

    private void SetGameState(GameState new_state)
    {
      gameState_ = new_state;
    }

    private GameState GetGameState()
    {
      return gameState_;
    }

    private void GameStateDetermination()
    {
      if (Input.GetButtonDown("Pause") && GetGameState() != GameState.PAUSE) // вынести в отдельный GameInput
      {
        SetGameState(GameState.PAUSE);
        pauseC.EnablePauseMenu();
        TimeFreezingFunc.EnableFreezing();

        healthC.DisableHealthText();
      }

      else if (Player.isFinished) // победа, если пересек финиш
      {
        SetGameState(GameState.VICTORY);

        toStart = false;
        timePassing_ = CalcTime(counter_ - 1); // -1 потому что незаконченная секунда увеличивает момент пересечения финиша на 1 секунду
        print(timePassing_);

        victoryC.SetTimeResult(timePassing_);
        victoryC.EnableVictoryMenu();
        TimeFreezingFunc.EnableFreezing();

        healthC.DisableHealthText();
      }

      else if (Player.isFell || Player.healthState == LifeState.DEAD) // поражение, если упал или потерял все здоровье
      {
        SetGameState(GameState.DEFEAT);

        defeatC.EnableDefeatMenu();
        TimeFreezingFunc.EnableFreezing();

        healthC.DisableHealthText();
      }

      else if ((GetGameState() != GameState.VICTORY &&
                GetGameState() != GameState.DEFEAT &&
                GetGameState() != GameState.PAUSE) || (Input.GetButtonDown("Pause") && GetGameState() == GameState.PAUSE))
      {
        pauseC.DisablePauseMenu();
        defeatC.DisableDefeatMenu();
        victoryC.DisableVictoryMenu();

        TimeFreezingFunc.DisableFreezing();

        SetGameState(GameState.IN_PROCESS);

        healthC.EnableHealthText();
      }
    }

    private IEnumerator GameCounter()
    {
      if (!isStartedCount && toStart)
      {
        isStartedCount = true;
        
        while (toStart)
        {
          yield return new WaitForSeconds(1f);

          ++counter_;
        }
      }
    }

    private string CalcTime(int counter)
    {
      int seconds, minutes, hours;
      string result;

      hours = counter / 3600;
      minutes = hours == 0 ? counter / 60 : counter % 3600 / 60;
      seconds = counter % 60;

      result = hours.ToString() + ":" +
                minutes.ToString() + ":" +
                seconds.ToString();

      return result;
    }
  }
}
