using UnityEngine;

public class Tire : MonoBehaviour
{
	[SerializeField] private ParticleSystem _smoke;

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

	private void Start()
	{
		if (_smoke != null)
			_smoke.Stop();
	}

	public void SetTrailActive(bool active) {
		if (active && !TrailActive) {
			// These should be pooled and re-used
			Skidmark = Instantiate (Resources.Load ("Skidmark") as GameObject, transform.position, transform.rotation);
						
			//Fix issue where skidmarks draw at 0,0,0 at slow speeds
			Skidmark.GetComponent<TrailRenderer>().Clear();
			
			Skidmark.GetComponent<TrailRenderer> ().sortingOrder = 0;
			Skidmark.transform.parent = transform;
			
			if (_smoke != null)
				_smoke.Play();
		} else if (!active && TrailActive) {			
			Skidmark.transform.parent = null;
			
			if (_smoke != null)
				_smoke.Stop();
		}
		TrailActive = active;
	}

}
