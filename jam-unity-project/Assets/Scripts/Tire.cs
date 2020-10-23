using UnityEngine;

public class Tire : MonoBehaviour {

	public float RestingWeight { get; set; }
	public float ActiveWeight { get; set; }
	public float Grip { get; set; }
	public float FrictionForce { get; set; }
	public float AngularVelocity { get; set; }
	public float Torque { get; set; }

	public float Radius = 0.5f;

	float TrailDuration = 300;
	bool TrailActive;
	GameObject Skidmark;

	public void SetTrailActive(bool active) {
		if (active && !TrailActive) {
			// These should be pooled and re-used
			Skidmark = Instantiate (Resources.Load ("Skidmark") as GameObject, transform.position, transform.rotation);
						
			//Fix issue where skidmarks draw at 0,0,0 at slow speeds
			Skidmark.GetComponent<TrailRenderer>().Clear();
			
			Skidmark.GetComponent<TrailRenderer> ().sortingOrder = 0;
			Skidmark.transform.parent = transform;
		} else if (!active && TrailActive) {			
			Skidmark.transform.parent = null;
		}
		TrailActive = active;
	}

}
