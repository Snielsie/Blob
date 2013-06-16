 using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {
	// here are all the fields stored wich, are used by this and other classes
	private CharacterController charcontroller;
	private CollisionFlags collisionFlags; 
	private Animation animation;
	public GameObject target;
	public float moveSpeed = 20.0f;
	public float jumpSpeed = 50.0F;
    public float gravity = 1.0F;
	private bool paused = false;
	
	private Vector3 moveDirection = Vector3.zero;
	Vector3 offset;
	
	/*
	 * This function initializes everything that is needed for the other functions to function.
	 */
	void Start () {
		//enable saving
		Model.saveProgress = true;
		
		charcontroller = transform.GetComponent<CharacterController>();
		animation = transform.GetComponent<Animation>();
		Screen.showCursor = false; 
		Screen.lockCursor = true;
		
		Vector3 savedPos = Model.getPlayerPos();
		
		if(savedPos != Vector3.zero)
			transform.position = savedPos;
	}
	
	/* 
	 * This function updates everything based on the input of the player and/or functions. 
	 * Also refers/calls upon the functions for input and changes.
	 */
	void Update ()
	{
		if(this.paused)
			return;
		
		movement();
		
	}

	/*
	 * This function lets the character turn.
	 */
	public void CharRotate(float rotateTo) {
		Quaternion rotation = Quaternion.Euler(0, rotateTo, 0);
		transform.rotation = rotation;
	}
	/*
	 * this function is called upon by the update function.
	 * Here al the controls are stored wich the player can use, this returns the movement input given by the player.
	 */ 
	void movement () {
			/* If an object is grounded (charcontroller.isGrounded), the object can walk on the x & z axel.
			 * The object can also jump, when this happens it gets pulled back down by the gravit
		 	*/
		string curAnim = "idle";
		if (charcontroller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;
			
            //if (Input.GetButton("Jump"))
               // moveDirection.y = jumpSpeed;
		}
		else if((collisionFlags & CollisionFlags.CollidedBelow) == 0)
		{
			moveDirection.y -= gravity * Time.deltaTime;
		}

		/* Here the movement distance gets calculated on time (sec.) and not on frames,
		 * We give the function move the following commands: wich course and how much it can move until there is collision.
		 * When there is collision the object can't move further.
		 */
		
		if(Mathf.Abs(Input.GetAxisRaw("Vertical")) > .1 || Mathf.Abs(Input.GetAxisRaw("Horizontal")) > .1) {
			curAnim = "walk";
		
		}
	
        collisionFlags = charcontroller.Move(moveDirection * Time.deltaTime);
		
		animation.CrossFade(curAnim);
	}
	
	/*This function is called upon by the update function.
	 * This funtion hold the information and events about what should happen if it is called upon by the update function.
	 */
	public void OnPauseGame()
	{
		Screen.showCursor = true; 
		Screen.lockCursor = false;
		this.paused = true;
	}
	 
	public void OnResumeGame()
	{
		Screen.showCursor = false; 
		Screen.lockCursor = true;
		paused = false;
	}
	
	public void OnDestroy ()
	{
		Model.savePlayerPos(this.transform.position);
	}
}
