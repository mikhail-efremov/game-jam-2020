using GameFlow;
using UnityEngine;

namespace LevelLogic
{
  public class FinishTrigger : MonoBehaviour
  {
    private void OnCollisionEnter2D(Collision2D other)
    {
      if (other.transform.GetComponent<Car>() != null)
        GameFlowManager.Instance.OnFinishEnter();
    }
  }
}