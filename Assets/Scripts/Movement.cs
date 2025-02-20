using UnityEngine;
//using System.Mathf;

public class Movement : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
    	_animator = GetComponent<Animator>();   
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {

        	Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        	Vector3 direction = mousePosition - transform.position;
        	float angle = NormalizeAngle(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
        	Debug.Log(angle);
        	int animationIndex = (int)((angle + 22.5f) / 45f) % 8;
        	string[] animations = new string[]
        	{
        		"Back",
        		"BackRight",
        		"Right",
        		"FrontRight",
        		"Front",
        		"FrontLeft",
        		"Left",
        		"BackLeft",
        		"knd",
        		"jndf"
        	};
        	_animator.Play(animations[animationIndex]);
        	
        }
    }
    private float NormalizeAngle(float angle)
    {
    	while (angle > 180f)
    	{
    		angle -=360f;
    	}
    	while (angle < -180f)
    	{
    		angle += 360f;
    	}
    	return angle;
    }
}
