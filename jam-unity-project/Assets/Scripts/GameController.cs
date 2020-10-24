using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using GameFlow;
using UnityEngine;

public class GameController : MonoBehaviour
{
  public const float turnDuration = 12f;
  public const float timelapseDuration = 4f;
  public int CurrentCarIndex = -1;

  [SerializeField] private Car[] Cars;

  public bool TimeLapsIsActive => _isTimeLapse && _gameTick > 0;

  private List<GameObject> _cars = new List<GameObject>();
  private Dictionary<int, List<CarPosition>> _carToPositions = new Dictionary<int, List<CarPosition>>();
  private int _gameTick;
  private bool _isTimeLapse;
  private bool _isActive;

  public void StartController()
  {
    if (_isActive)
      return;

    _isActive = true;
  }

  public void GetNextCar()
  {
    NextCar();
  }

  public void EnableTimeLaps()
  {
    _isTimeLapse = true;
  }

  public void DisableTimeLaps()
  {
    _isTimeLapse = false;
  }

  private void FixedUpdate()
  {
    if(!_isActive)
      return;

    if (_isTimeLapse)
      Timelapse();
    else
      MoveStraightforward();
  }

  private void Timelapse()
  {
    for (var i = 0; i < CurrentCarIndex + 1; i++)
      UpdatePos(i);

    _gameTick -= (int)(turnDuration/timelapseDuration);
  }

  private void MoveStraightforward()
  {
    var currentCar = _cars[CurrentCarIndex];
    _carToPositions[CurrentCarIndex].Add(new CarPosition(currentCar.transform.position, currentCar.transform.rotation));

    for (var i = 0; i < CurrentCarIndex; i++)
      UpdatePos(i);

    _gameTick++;
  }

  private void UpdatePos(int i)
  {
    var currentTick = Mathf.Clamp(_gameTick, 0, _carToPositions[i].Count-1);
    _cars[i].transform.position = _carToPositions[i][currentTick].position;
    _cars[i].transform.rotation = _carToPositions[i][currentTick].rotation;
  }

  public void NextCar()
  {
    CurrentCarIndex++;

    var start = GameFlowManager.Instance.Starts
      .First(x => x.Index == CurrentCarIndex + 1);
    var startTransform = start.transform;

    var go = Instantiate(Cars[CurrentCarIndex], startTransform.position, startTransform.rotation);
    var carComponent = go.GetComponent<Car>();
    carComponent.IsPlayerControlled = true;

    GameFlowManager.Instance.CurrentCar = carComponent;
    _cars.Add(go.gameObject);
    _carToPositions.Add(CurrentCarIndex, new List<CarPosition>());
    _gameTick = 0;
  }
}