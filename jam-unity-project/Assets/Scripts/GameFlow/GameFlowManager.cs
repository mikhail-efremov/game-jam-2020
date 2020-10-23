using System.Collections;
using System.Globalization;
using DG.Tweening;
using LevelLogic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameFlow
{
  public class GameFlowManager : MonoBehaviour
  {
    public Car CurrentCar;
    [SerializeField] private Image _timerIcon;
    [SerializeField] private TextMeshProUGUI _timerText;

    [SerializeField] private TextMeshProUGUI _tabToStartLabel;

    [SerializeField] private TextMeshProUGUI _firstPlayerFinished;
    [SerializeField] private TextMeshProUGUI _secondPlayerFinished;

    [SerializeField] private TextMeshProUGUI _firstPlayerLose;
    [SerializeField] private TextMeshProUGUI _secondPlayerLose;

    [SerializeField] private FinishTrigger[] _finishes;

    public static GameFlowStateId CurrentStateId;
    public static PlayerTypeId CurrentPlayer;

    public static GameFlowManager Instance = null;

    private float CurrentTurnTime;

    private bool _setTimerAlarm = false;

    public void OnFinishEnter()
    {
      SetToPlayerReachedFinish();
    }

    private void Start()
    {
      DOTween.SetTweensCapacity(99999, 99999);

      _timerText.text = GameController.turnDuration.ToString("N2").Replace(",", ".");;

      _firstPlayerFinished.gameObject.SetActive(false);
      _secondPlayerFinished.gameObject.SetActive(false);
      _firstPlayerLose.gameObject.SetActive(false);
      _secondPlayerLose.gameObject.SetActive(false);

      if (Instance == null)
        Instance = this;

      SetToStartScreen();
    }

    private void Update()
    {
      var spacePressed = Input.GetKeyDown(KeyCode.Space);

      if (CurrentStateId == GameFlowStateId.StartScreen && spacePressed)
      {
        SetToGameplay();
        GetComponent<GameController>().StartController();
      }

      if (CurrentStateId == GameFlowStateId.Gameplay)
      {
        CurrentTurnTime += Time.deltaTime;

        var timeLeft = GameController.turnDuration - CurrentTurnTime;

        _timerText.text = timeLeft > 0 ? timeLeft.ToString("N2").Replace(",", ".") : "0.00";

        if (timeLeft < 3f && !_setTimerAlarm)
        {
          _setTimerAlarm = true;
          _timerIcon.DOFade(0.0f, .2f).SetEase(Ease.Flash).SetLoops(-1, LoopType.Yoyo);
          _timerText.DOFade(0.0f, .2f).SetEase(Ease.Flash).SetLoops(-1, LoopType.Yoyo);
        }

        if (CurrentTurnTime > GameController.turnDuration)
        {
          StartCoroutine(SetPlayerLose());
        }
      }
    }

    private void SetToStartScreen()
    {
      CurrentStateId = GameFlowStateId.StartScreen;

      if (CurrentCar != null)
        CurrentCar.enabled = false;
      _tabToStartLabel.DOFade(0.0f, .5f).SetEase(Ease.InExpo).SetLoops(-1, LoopType.Yoyo);
    }

    private void SetToGameplay()
    {
      _setTimerAlarm = false;
      CurrentStateId = GameFlowStateId.Gameplay;

      if (CurrentCar != null)
        CurrentCar.enabled = true;
      _tabToStartLabel.gameObject.SetActive(false);

      GetComponent<GameController>().GetNextCar();
    }

    private void SetToPlayerReachedFinish()
    {
      CurrentStateId = GameFlowStateId.PlayerReachedFinish;
      CurrentTurnTime = 0;

      StartCoroutine(PlayerReachedFinish());
    }

    private IEnumerator SetPlayerLose()
    {
      if (CurrentPlayer == PlayerTypeId.First)
        _firstPlayerLose.gameObject.SetActive(true);
      if (CurrentPlayer == PlayerTypeId.Second)
        _secondPlayerLose.gameObject.SetActive(true);

      CurrentCar.EndTrails();
      Destroy(CurrentCar);
      yield return new WaitForSeconds(2);

      SceneManager.LoadScene(0);
    }

    private IEnumerator PlayerReachedFinish()
    {
      if (CurrentPlayer == PlayerTypeId.First)
        _firstPlayerFinished.gameObject.SetActive(true);
      if (CurrentPlayer == PlayerTypeId.Second)
        _secondPlayerFinished.gameObject.SetActive(true);

      CurrentCar.EndTrails();
      Destroy(CurrentCar);
      CurrentCar.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
      GetComponent<GameController>().EnableTimeLaps();

      var delayTime = 0f;
      while (true)
      {
        delayTime += Time.deltaTime;
        yield return null;
        if (!GetComponent<GameController>().TimeLapsIsActive)
        {
          if (delayTime >= 2)
            break;
        }
      }

      GetComponent<GameController>().DisableTimeLaps();

      _firstPlayerFinished.gameObject.SetActive(false);
      _secondPlayerFinished.gameObject.SetActive(false);

      if (CurrentPlayer == PlayerTypeId.First)
        CurrentPlayer = PlayerTypeId.Second;
      else if (CurrentPlayer == PlayerTypeId.Second)
        CurrentPlayer = PlayerTypeId.First;

      SetToGameplay();
    }
  }
}