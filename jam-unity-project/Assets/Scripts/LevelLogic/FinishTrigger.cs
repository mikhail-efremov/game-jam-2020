using System.Collections;
using GameFlow;
using UnityEngine;

namespace LevelLogic
{
  public class FinishTrigger : MonoBehaviour
  {
    public int Index => index;
    [SerializeField] private int index;

    [SerializeField] private GameObject Mesh;
    [SerializeField] private GameObject Fx;

    private void OnTriggerStay2D(Collider2D other)
    {
      if (other.transform.GetComponent<Car>() != null)
      {
        GameFlowManager.Instance.OnFinishEnter();
        StartCoroutine(FxActions());
      }
    }

    private IEnumerator FxActions()
    {
      Fx.SetActive(true);
      yield return new WaitForSeconds(.1f);
      Mesh.SetActive(false);
    }
  }
}