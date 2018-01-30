using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokerController : MonoBehaviour {

		
	public float speed;
	public GameObject energyBar; 


    private Direction dir; 
	private Rigidbody rb;
    private List<GameObject> inFrontOf;

    private float buttonDownTime;
    private bool trackingKeyPress;
    private SmokerAttack trackedEnum;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
        this.dir = Direction.UP;
        this.inFrontOf = new List<GameObject>();
        trackingKeyPress = false;
        this.trackedEnum = SmokerAttack.None;


	}

	// Update is called once per frame
	void Update () {


        // When you press a key:
        // 1) make sure it's maped to a skill
        //   If it is, then reccord the time,
        //                  stop caring about all other key presses
        //                  Remember what skill/ key was pressed
        //   Look only at key up events
        //   If the key up event is for the skill/key that we reccorded being pressed
        //         Record the time and find the difference
        //         Start caring about key down events
        //         Forget what key up / skill was just used

        if (this.trackedEnum == SmokerAttack.RELOAD) {
			if (Time.time - this.buttonDownTime > 3.0f) {
				var prog = this.energyBar.GetComponent<Progress> ();

				if (prog == null) {
					print ("NULL!");
				} else {
					prog.addFill (prog.getMaxFill());
				}

                //this.trackingKeyPress = false;
                //this.trackedEnum = SmokerAttack.None;
                //processKeys();
            }
        } else {
            processKeys();
        }



	}

    // The smoker uses wasd to move
    void FixedUpdate()
    {

        float vertMove = 0.0f;
        float horizMove = 0.0f;
        if (Input.GetKey("w"))
        {
            this.changeDirection(Direction.UP);
            vertMove = 1.0f;
        }
        else if (Input.GetKey("s"))
        {
            this.changeDirection(Direction.DOWN);
            vertMove = -1.0f;
        }
        else if (Input.GetKey("d"))
        {
            this.changeDirection(Direction.RIGHT);
            horizMove = 1.0f;
        }
        else if (Input.GetKey("a"))
        {
            this.changeDirection(Direction.LEFT);
            horizMove = -1.0f;
        }


        Vector3 movement = new Vector3(horizMove, vertMove, 0.0f);
        Vector3.Normalize(movement);
        rb.AddForce(movement * speed);
    }


    private void processKeys() {
        // If the user presses "1" and we're not tracking a key press
        if (!trackingKeyPress)
        {
            // IF we're not currently tracking a down key press

            if (Input.GetKeyDown("1"))
            {
                // Record when the button was pressed
                this.trackingKeyPress = true;
                this.trackedEnum = SmokerAttack.PUFF;
                // Puff smoke to all infront of you

            } // End Input.GetKeyDown("1");

            // Reload key
            if (Input.GetKeyDown("r"))
            {
                this.trackingKeyPress = true;
                this.trackedEnum = SmokerAttack.RELOAD;
            }


            // NEW SKILL/ KEY DOWN EVENTS GO HERE

            if(trackingKeyPress) {
                // If we started tracking a new key
                this.buttonDownTime = Time.time;
            }
        }





        if (Input.GetKeyUp("1") && this.trackedEnum == SmokerAttack.PUFF)
        {
            float buttonUpTime = Time.time;
            this.trackingKeyPress = false;
            this.trackedEnum = SmokerAttack.None;

            // Less than the number implies a quick attack
            if (buttonUpTime - this.buttonDownTime < 0.5f)
            {
                // Do a small puff attack;
                float puffCost = 10.0f;
                foreach (GameObject go in inFrontOf)
                {
					if (this.energyBar == null) {
						print ("NULL3");
					}
					var prog = 
						this.energyBar.GetComponent<Progress> ();

					if (prog == null) {
						print ("NULL2");
					} else {

						if (prog.getCurrentFill() <= puffCost)
	                    {
	                        break; // Out of energy
	                    }
	                    go.GetComponent<Renderer>().material.color = new Color(0, 0, 200);
						prog.reduceFill(puffCost);
					}
                }
            }
            else
            {
                // Do a large puff attack
            }
        } // End Input.GetKeyUp("1") 
    }



    private void OnTriggerEnter(Collider other)
    {
        this.inFrontOf.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        this.inFrontOf.Remove(other.gameObject);
    }




    private void changeDirection(Direction newDir) {

        BoxCollider c = this.GetComponents<Collider>()[1] as BoxCollider;

        if (this.dir == newDir) {
            return;
        }

        switch(newDir) {
            case Direction.UP:
                c.center = new Vector3(0, 1, 0);
                this.dir = newDir;
                break;
            
            case Direction.DOWN:
                c.center = new Vector3(0, -1, 0);
                this.dir = newDir;
                break;
            
            case Direction.LEFT:
                c.center = new Vector3(-1, 0, 0);
                this.dir = newDir;
                break;

            case Direction.RIGHT:
                c.center = new Vector3(1, 0, 0);
                this.dir = newDir;
                break;
        }
    }
}
