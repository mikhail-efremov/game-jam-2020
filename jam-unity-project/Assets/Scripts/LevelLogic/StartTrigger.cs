using UnityEditor;
using UnityEngine;

namespace LevelLogic
{
  public class StartTrigger : MonoBehaviour
  {
    public int Index => index;

    [SerializeField] private int index;


    public void OnDrawGizmos()
    {
      Gizmos.color = Color.red;
      Gizmos.DrawCube(transform.position + transform.up, new Vector3(1f,1f,1f));
      Gizmos.DrawCube(transform.position, new Vector3(2,2,2));
    }
  }
}