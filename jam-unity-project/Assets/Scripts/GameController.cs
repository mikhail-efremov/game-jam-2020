using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using GameFlow;
using LevelLogic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  public const float turnDuration = 12f;
  public const float timelapseDuration = 4f;
  public GameObject car;

  public bool TimeLapsIsActive => _isTimeLapse && _gameTick > 0;

  private List<GameObject> _cars = new List<GameObject>();
  private Dictionary<int, List<CarPosition>> _carToPositions = new Dictionary<int, List<CarPosition>>();
  private int _currentCarIndex = -1;
  private int _gameTick;
  private bool _isTimeLapse;
  private bool _isActive;

  public void StartController()
  {
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
    // if(ISACTION) should track only if we play and not showing some numbers
    if (_isTimeLapse)
    {
      Timelapse();
    }
    else
    {
      MoveStraightforward();
    }
  }

  private void Timelapse()
  {
    for (var i = 0; i < _currentCarIndex + 1; i++)
      UpdatePos(i);

    _gameTick -= (int)(turnDuration/timelapseDuration);
  }

  private void MoveStraightforward()
  {
    var currentCar = _cars[_currentCarIndex];
    _carToPositions[_currentCarIndex].Add(new CarPosition(currentCar.transform.position, currentCar.transform.rotation));

    for (var i = 0; i < _currentCarIndex; i++)
    {
      UpdatePos(i);
    }

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
    _currentCarIndex++;

    var start = GameFlowManager.Instance.Starts
      .First(x => x.Index == _currentCarIndex + 1);
    var startTransform = start.transform;

    var go = Instantiate(car, startTransform.position, startTransform.rotation);
    var carComponent = go.GetComponent<Car>();
    carComponent.IsPlayerControlled = true;

    GameFlowManager.Instance.CurrentCar = carComponent;
    _cars.Add(go);
    _carToPositions.Add(_currentCarIndex, new List<CarPosition>());
    _gameTick = 0;
  }
}