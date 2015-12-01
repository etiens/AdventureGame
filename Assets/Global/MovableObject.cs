using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MovableObject : MonoBehaviour {

	// Animation Variables  
	protected Animator H_animator;

		
	// Speed, direction and movement variables
	protected Vector2 H_objectDirection {get {return _objectDirection;}	set{_objectDirection = Utils.Get8Dir(value);}}
	Vector2 _objectDirection;
	protected float H_speed;
	protected float H_maxSpeed;
	protected float H_minSpeed;
	public float P_speedBonusPercent;
	protected float H_acceleration;

	// Movement variables	
	protected bool H_moving;
	protected bool H_noClipping;
	protected float H_movementAngle;
	protected float H_movementSpeed;
	protected float H_movementTimer;
	protected float H_movementDuration;

	// Rotation variables
	
	Quaternion _initialRotation = new Quaternion ();
	protected bool H_rotating;	
	protected float H_rotationTimer;
	protected float H_rotationDuration;
	// rotation speed is in degree per second
	protected float H_rotationSpeed;
	
	// Use this for initialization
	public virtual void Awake () {
		_initialRotation = transform.localRotation;		
		H_objectDirection = new Vector2 (0.0F, -1.0F);
		H_moving = false;
		H_noClipping = false;
		H_movementAngle = 0.0F;
		H_movementSpeed = 0.0F;
		H_movementTimer = 0.0F;
		H_movementDuration = 0.0F;
		H_animator = this.GetComponent<Animator>();
		H_rotating = false;	
		H_rotationTimer = 0.0F;
		H_rotationDuration = 0.0F;
		H_rotationSpeed = 0.0F;
		P_speedBonusPercent = 0.0F;
		H_moving = false;
		H_noClipping = false;
	}

	public virtual void Move(Vector2 direction, float speed, float duration){
		if (duration <= 0.0F) {
			Vector2 movement = new Vector2 (0.0F, 0.0F);
			float angle = 0.0F;
			angle = Mathf.Deg2Rad * Utils.Vec2Ang (direction);
			movement.x = Mathf.Sin (angle) * speed;
			movement.y = Mathf.Cos (angle) * speed;
			rigidbody2D.velocity = (Vector3)movement;	
		} else {
			H_movementDuration = duration;
			H_movementAngle = Mathf.Deg2Rad * Utils.Vec2Ang (direction);
			H_movementSpeed = speed;
			H_movementTimer = 0.0F;
			H_moving = true;
		}

	}

	// This function takes in the start angle (degree), the end angle of the rotation, the duration of the rotation in seconds.
	// If the duration is 0, then the attack goes into its final position right away. Duration should be positive.
	// The direction represents the "0" degree direction.
	public virtual void RotateAroundParent(float angleStart, float angleEnd, float duration, Vector2 direction, float distance = 0.2F){
		if (duration < Utils.eps) {
			transform.rotation = _initialRotation;
			transform.position = transform.parent.position + new Vector3(0,distance,0);
			transform.RotateAround(transform.parent.position, new Vector3(0,0,-1), (Utils.Vec2Ang(direction) + angleEnd));
		} else {
			// Position the attack at the desired "start" angle.
			transform.rotation = _initialRotation;
			transform.position = transform.parent.position + new Vector3(0,distance,0);
			transform.RotateAround(transform.parent.position, new Vector3(0,0,-1), (Utils.Vec2Ang(direction) + angleStart));
			H_rotating = true;
			H_rotationSpeed = (angleEnd - angleStart)/duration;
			H_rotationDuration = duration;
			H_rotationTimer = 0.0F;
		}
	}
		
	// This function allows the rescaling of the object.
	public virtual void Scale(float xScale, float yScale){
		if (transform.parent != null){
			xScale /= transform.parent.localScale.x;
			yScale /= transform.parent.localScale.y;
		}
		transform.localScale = new Vector2 (xScale, yScale);
	}
	// This function rotates the object. Rotation is in degree;
	public virtual void Rotate(float rotation){
		float newAngle = 0;
		newAngle = Utils.Vec2Ang (H_objectDirection) + rotation;
		newAngle *= Mathf.Deg2Rad;
		_objectDirection.x = Mathf.Sin (newAngle);
		_objectDirection.y = Mathf.Cos (newAngle);
	}

	public virtual void PerformMove(){
		if (H_movementTimer <= H_movementDuration) {
			Vector2 movement = new Vector2 (0.0F, 0.0F);
			movement.x = Mathf.Sin (H_movementAngle) * H_movementSpeed;
			movement.y = Mathf.Cos (H_movementAngle) * H_movementSpeed;
			rigidbody2D.velocity = movement;	
			H_movementTimer += Time.deltaTime;
		} else {
			H_movementTimer = 0.0F;
			H_moving = false;
		}
	}

	public void UpdateDirection(){
		transform.eulerAngles = new Vector3(0F, 0F, -Utils.Vec2Ang(H_objectDirection));
	}

	// Update is called once per frame
	public virtual void Update () {
		rigidbody2D.velocity = new Vector2(0,0);

		if (H_moving) {
			PerformMove();
		}
		if (H_rotating) {
			if (H_rotationTimer<=H_rotationDuration){
				transform.RotateAround(transform.parent.position, new Vector3(0,0,-1), H_rotationSpeed * Time.deltaTime);
				H_rotationTimer += Time.deltaTime;		
			} else {
				H_rotationTimer = 0.0F;
				H_rotating = false;
			}
		}
		
	}

}
