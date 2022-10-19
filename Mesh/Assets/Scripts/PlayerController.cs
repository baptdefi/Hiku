using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	// OLD
	//private float moveSpeed = 10;
	//private float baseMoveSpeed = 10;
	//private float dashMoveSpeed = 75;
	//private float bonusMoveSpeed = 50;

	private Rigidbody rb;

	// GUI
	public Sprite SpriteSpeed;
	public Sprite SpriteStrength;
	public Sprite SpriteLightness;
	public Sprite SpriteTrap;
	public Sprite SpriteSardine;
	public Sprite SpriteBomb;
	public Sprite SpriteJump;
	public Sprite SpriteSnowBall;
	public Sprite Vide;
	public GameObject P1Bonus;
	public GameObject P2Bonus;
	public GameObject P3Bonus;
	public GameObject P4Bonus;
	private Image IMP1;
	private Image IMP2;
	private Image IMP3;
	private Image IMP4;
	public GameObject P3B;
	public GameObject P4B;

	// CHARACTERISTIC
	private float moveSpeed = 5f;
	private float moveSpeedThreshold = 15f;
	private float baseMoveSpeed = 5f;
	private float baseMoveSpeedThreshold = 15f;

	// ITEMS
	private PickUp item = null;
	private PickUp bonus = null;
	private GameObject trap;
	public GameObject trapModel;
	public GameObject sardineModel;
	public GameObject bombModel;
	public GameObject snowBallModel;
	private bool bonusActive = false;
	private bool itemHold = false;
	private float bonusTimer;
	private float speedBonusDuration = 75;
	private float strengthBonusDuration = 250;
	private float lightnessBonusDuration = 200;
	private float strengthBonus = 10;
	private bool isLight = false;
	private bool isTrapped = false;
	private float trapTimer;
	private int trapMode = 0;
	private float trapDuration = 125;
	private float snowBallDuration = 70;

	// MOVEMENT
	private Vector3 moveInput;
	private Vector3 moveVelocity;
	private Vector3 playerDirection;
	private Vector3 lastPlayerDirection;

	// DASH
	private DashState dashState;
	private float dashTimer = 0;
	private float dashDuration = 15;
	private float dashCooldown = 50;
	private float dashMoveSpeed = 30f;
	private float dashMoveSpeedThreshold = 20f;
	private bool forceChanged = false;

	// CONTROLS
	private string AButtonName;
	private string BButtonName;
	private string XAxisName;
	private string YAxisName;

    private string AButtonKeyboardName;
	private string BButtonKeyboardName;
    private string UpKeyboardName;
    private string DownKeyboardName;
    private string LeftKeyboardName;
    private string RightKeyboardName;

    // DEBUG
    private bool bBlockTileDestruction = false;


    private string playerName;
	int nbjoueur=2;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody>();
		
		//affiche les bonus de p3 et p4 si present
		if (PlayerPrefs.GetInt ("PreferedModel3") > -1) {
			P3B.SetActive (true);
			nbjoueur++;
		}
		
		if (PlayerPrefs.GetInt ("PreferedModel4") > -1) {
			P4B.SetActive (true);
			nbjoueur++;
		}
		PlayerPrefs.SetInt ("nbjoueur", nbjoueur);
		
		playerName = transform.parent.gameObject.name;

        if (playerName.Equals("Player1")){
			AButtonName = "J1_BA";
			BButtonName = "J1_BB";
			XAxisName = "J1_LStick_X";
			YAxisName = "J1_LStick_Y";
            AButtonKeyboardName = "J1_Keyboard_BA";
            BButtonKeyboardName = "J1_Keyboard_BB";
            UpKeyboardName = "J1_Keyboard_Up";
            DownKeyboardName = "J1_Keyboard_Down";
            LeftKeyboardName = "J1_Keyboard_Left";
            RightKeyboardName = "J1_Keyboard_Right";
}
		else if (playerName.Equals("Player2")){
			AButtonName = "J2_BA";
			BButtonName = "J2_BB";
			XAxisName = "J2_LStick_X";
			YAxisName = "J2_LStick_Y";
            AButtonKeyboardName = "J2_Keyboard_BA";
            BButtonKeyboardName = "J2_Keyboard_BB";
            UpKeyboardName = "J2_Keyboard_Up";
            DownKeyboardName = "J2_Keyboard_Down";
            LeftKeyboardName = "J2_Keyboard_Left";
            RightKeyboardName = "J2_Keyboard_Right";
        }
		else if (playerName.Equals("Player3")){
			AButtonName = "J3_BA";
			BButtonName = "J3_BB";
			XAxisName = "J3_LStick_X";
			YAxisName = "J3_LStick_Y";
		}
		else if (playerName.Equals("Player4")){
			AButtonName = "J4_BA";
			BButtonName = "J4_BB";
			XAxisName = "J4_LStick_X";
			YAxisName = "J4_LStick_Y";
		}

		// To get collision event even if the player is not moving
		rb.sleepThreshold = 0;

		// Bonus Sprite
		IMP1 = P1Bonus.GetComponent<Image> ();
		IMP2 = P2Bonus.GetComponent<Image> ();
		IMP3 = P3Bonus.GetComponent<Image> ();
		IMP4 = P4Bonus.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            bBlockTileDestruction = !bBlockTileDestruction;
        }

        // debug
        //test modele au dessus du perso
        if (Input.GetKeyDown (KeyCode.A)) {
		Vector3 bombPosition = this.gameObject.transform.GetChild (0).transform.position;
		GameObject newbomb = bombModel;
		newbomb.GetComponent<Rigidbody> ().isKinematic = true;
		Instantiate (newbomb, bombPosition, Quaternion.LookRotation (playerDirection, Vector3.up));
		}

		//Get axis inputs
		float hAxis = Input.GetAxis (XAxisName);
		float vAxis = Input.GetAxis (YAxisName);

		//Don't consider too small inputs
		if (-0.2f <= hAxis && hAxis <= 0.2f) {
			hAxis = 0;
		}
		if (-0.2f <= vAxis && vAxis <= 0.2f) {
			vAxis = 0;
		}

        float moveInputVplus = 0;
        float moveInputVminus = 0;
        float moveInputHplus = 0;
        float moveInputHminus = 0;
        float moveInputV = 0;
        float moveInputH = 0;

        if (Input.GetButton(UpKeyboardName))
            moveInputVplus = 1;
        if (Input.GetButton(DownKeyboardName))
            moveInputVminus = -1;
        if (Input.GetButton(LeftKeyboardName))
            moveInputHminus = -1;
        if (Input.GetButton(RightKeyboardName))
            moveInputHplus = 1;

        moveInputV = moveInputVplus + moveInputVminus;
        moveInputH = moveInputHplus + moveInputHminus;

        // Controller and Keyboard should be detected and separated
        // Move input
        moveInput = new Vector3 (hAxis + moveInputH, 0, vAxis + moveInputV);

        moveVelocity = moveInput * moveSpeed;
		playerDirection = Vector3.right * (hAxis + moveInputH) + Vector3.forward * (vAxis + moveInputV);

		// Rotation toward direction
		if (playerDirection.sqrMagnitude > 0.0f) {
			transform.rotation = Quaternion.LookRotation (playerDirection, Vector3.up);
		}
		if (playerDirection.x != 0 || playerDirection.z != 0) {
			lastPlayerDirection = playerDirection;
		}


		Debug.DrawRay (transform.position, playerDirection * 100, Color.red);


		//Dash
		switch (dashState) 
		{
		case DashState.Ready:
			if(Input.GetButtonDown (BButtonName) || Input.GetButtonDown(BButtonKeyboardName))
			{
				//print ("b button");
				moveSpeed = dashMoveSpeed;
				moveSpeedThreshold = dashMoveSpeedThreshold;
					rb.mass += strengthBonus;
					forceChanged = true;
					Debug.Log (rb.mass);
				dashState = DashState.Dashing;
			}
			break;
		case DashState.Dashing:
			dashTimer ++;
			if(dashTimer >= dashDuration)
			{
				dashTimer = dashCooldown;
				moveSpeed = baseMoveSpeed;
				moveSpeedThreshold = baseMoveSpeedThreshold;
				if (forceChanged) {
					rb.mass -= strengthBonus;
					forceChanged = false;
					Debug.Log (rb.mass);
				}
				dashState = DashState.Cooldown;
			}
			break;
		case DashState.Cooldown:
			dashTimer--;
			if(dashTimer <= 0)
			{
				dashTimer = 0;
				dashState = DashState.Ready;
			}
			break;
		}

		// Item utilisation
		if (itemHold) {

			if (Input.GetButtonDown (AButtonName) || Input.GetButtonDown(AButtonKeyboardName)) {

				//Debug.Log ("a button");

				// Item
				if (item.getType () == PickUp.PickUpType.Trap || item.getType () == PickUp.PickUpType.Jump || item.getType () == PickUp.PickUpType.Bomb || item.getType () == PickUp.PickUpType.Sardine || item.getType() == PickUp.PickUpType.SnowBall) {

					//Debug.Log ("item");

					// TRAP
					if (item.getType () == PickUp.PickUpType.Trap) {
						Vector3 trapPosition = new Vector3 (transform.position.x, 0.5f, transform.position.z);
						// Drop the trap a bit behind the player
						if (playerDirection.x != 0 || playerDirection.z != 0) {
							trapPosition.x -= 1.8f * playerDirection.normalized.x;
							trapPosition.z -= 1.8f * playerDirection.normalized.z;
						} else {
							trapPosition.x -= 1.8f * lastPlayerDirection.normalized.x;
							trapPosition.z -= 1.8f * lastPlayerDirection.normalized.z;
						}
						Instantiate (trapModel, trapPosition, Quaternion.identity);
					}

					// SNOWBALL
					else if (item.getType () == PickUp.PickUpType.SnowBall) {
						Vector3 snowballPosition = new Vector3 (transform.position.x, 2.0f, transform.position.z);
						// Drop the snowball a bit in front of the player
						if (playerDirection.x != 0 || playerDirection.z != 0) {
							snowballPosition.x += 3f * playerDirection.normalized.x;
							snowballPosition.z += 3f * playerDirection.normalized.z;

							if (playerDirection.x == 0) {
								playerDirection = new Vector3 (0.001f, playerDirection.y, playerDirection.z);
							}

							Instantiate (snowBallModel, snowballPosition, Quaternion.LookRotation (playerDirection, Vector3.up));
						} else {
							snowballPosition.x += 3f * lastPlayerDirection.normalized.x;
							snowballPosition.z += 3f * lastPlayerDirection.normalized.z;

							if (lastPlayerDirection.x == 0) {
								lastPlayerDirection = new Vector3 (0.001f, lastPlayerDirection.y, lastPlayerDirection.z);
							}

							Instantiate (snowBallModel, snowballPosition, Quaternion.LookRotation (lastPlayerDirection, Vector3.up));
						}
					}

					// SARDINE
					else if (item.getType () == PickUp.PickUpType.Sardine) {
						Vector3 sardinePosition = new Vector3 (transform.position.x, 2.0f, transform.position.z);
						// Drop the sardine a bit in front of the player
						if (playerDirection.x != 0 || playerDirection.z != 0) {
							sardinePosition.x += 1.8f * playerDirection.normalized.x;
							sardinePosition.z += 1.8f * playerDirection.normalized.z;

							if (playerDirection.x == 0) {
								playerDirection = new Vector3 (0.001f, playerDirection.y, playerDirection.z);
							}

							Instantiate (sardineModel, sardinePosition, Quaternion.LookRotation (playerDirection, Vector3.forward));
						} else {
							sardinePosition.x += 1.8f * lastPlayerDirection.normalized.x;
							sardinePosition.z += 1.8f * lastPlayerDirection.normalized.z;

							if (lastPlayerDirection.x == 0) {
								lastPlayerDirection = new Vector3 (0.001f, lastPlayerDirection.y, lastPlayerDirection.z);
							}

							Instantiate (sardineModel, sardinePosition, Quaternion.LookRotation (lastPlayerDirection, Vector3.forward));
						}
					}
						
					// BOMB lancée
					else if (item.getType () == PickUp.PickUpType.Bomb) {
						Vector3 bombPosition = new Vector3 (transform.position.x, 2.0f, transform.position.z);
						// Drop the bomb a bit in front of the player
						if (playerDirection.x != 0 || playerDirection.z != 0) {
							bombPosition.x += 1.8f * playerDirection.normalized.x;
							bombPosition.z += 1.8f * playerDirection.normalized.z;

							if (playerDirection.x == 0) {
								playerDirection = new Vector3 (0.001f, playerDirection.y, playerDirection.z);
							}

							GameObject bomb = Instantiate (bombModel, bombPosition, Quaternion.identity) as GameObject;
							bomb.GetComponent<Rigidbody> ().AddForce (transform.forward * 800);
						} else {
							bombPosition.x += 1.8f * lastPlayerDirection.normalized.x;
							bombPosition.z += 1.8f * lastPlayerDirection.normalized.z;

							if (lastPlayerDirection.x == 0) {
								lastPlayerDirection = new Vector3 (0.001f, lastPlayerDirection.y, lastPlayerDirection.z);
							}

							GameObject bomb = Instantiate (bombModel, bombPosition, Quaternion.identity) as GameObject;
							bomb.GetComponent<Rigidbody> ().AddForce (transform.forward * 800);
						}
					}
						
					/*// BOMB posée
					else if (item.getType () == PickUp.PickUpType.Bomb) {
						Vector3 bombPosition = new Vector3 (transform.position.x, 0.2f, transform.position.z);
						// Drop the bomb a bit behind the player
						if (playerDirection.x != 0 || playerDirection.z != 0) {
							bombPosition.x -= 1.8f * playerDirection.normalized.x;
							bombPosition.z -= 1.8f * playerDirection.normalized.z;
						} else {
							bombPosition.x -= 1.8f * lastPlayerDirection.normalized.x;
							bombPosition.z -= 1.8f * lastPlayerDirection.normalized.z;
						}
						Instantiate (bombModel, bombPosition, Quaternion.identity);
					}*/

					// JUMP
					else if (item.getType () == PickUp.PickUpType.Jump) {
						if (!isTrapped) {
							rb.AddForce (Vector3.up * item.getBonusJump (), ForceMode.VelocityChange);
						}
					}

					// Remise à zéro du sprite
					if (playerName.Equals ("Player1")) {
						IMP1.sprite = Vide;
					}
					else if (playerName.Equals ("Player2")) {
						IMP2.sprite = Vide;
					}
					else if (playerName.Equals ("Player3")) {
						IMP3.sprite = Vide;
					}
					else if (playerName.Equals ("Player4")) {
						IMP4.sprite = Vide;
					}

					// Item laché
					itemHold = false;


				}

				// Bonus
				else if (item.getType () == PickUp.PickUpType.Speed || item.getType () == PickUp.PickUpType.Strength || item.getType () == PickUp.PickUpType.Lightness) {
				
					//Debug.Log ("bonus");

					// S'il n'y en pas deja un en cours
					if (!bonusActive) {

						//Debug.Log ("pas de bonus en cours, lancement");

						// SPEED
						if (item.getType () == PickUp.PickUpType.Speed) {
							bonusActive = true;
							moveSpeed = item.getBonusMoveSpeed();
							moveSpeedThreshold = item.getBonusMoveSpeedThreshold ();
							bonusTimer = 0;
							bonus = item;
						}

						// STRENGTH
						else if (item.getType () == PickUp.PickUpType.Strength) {
							bonusActive = true;
							rb.mass += strengthBonus;
							Debug.Log (rb.mass);
							bonusTimer = 0;
							bonus = item;

							// Grossissement
							//transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
							transform.localScale = new Vector3(2, 2, 2);
						}

						// LIGHTNESS
						else if (item.getType () == PickUp.PickUpType.Lightness) {
							bonusActive = true;
							isLight = true;
							bonusTimer = 0;
							bonus = item;

							// Rapetissement
							//transform.GetChild(0).transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
							transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
						}

						// Remise à zéro du sprite
						if (playerName.Equals ("Player1")) {
							IMP1.sprite = Vide;
						}
						else if (playerName.Equals ("Player2")) {
							IMP2.sprite = Vide;
						}
						else if (playerName.Equals ("Player3")) {
							IMP3.sprite = Vide;
						}
						else if (playerName.Equals ("Player4")) {
							IMP4.sprite = Vide;
						}

						// Item laché
						itemHold = false;
					}
				}
			}
		}
	}

	void FixedUpdate()
	{
		// OLD rb.velocity = moveVelocity;

		if (!isTrapped) {
			// Movement
			if (rb.velocity.magnitude < moveSpeedThreshold) {
				rb.AddForce (moveVelocity, ForceMode.VelocityChange);
				rb.angularVelocity = Vector3.zero;
			}
		} else {
			// Trap management
			if (trapMode == 1 && trapTimer < trapDuration) {
				trapTimer++;
			}
			else if (trapMode == 2 && trapTimer < snowBallDuration) {
				trapTimer++;
			}
			else {
				trapTimer = 0;
				isTrapped = false;
				trapMode = 0;
				if (trap != null) {
					trap.GetComponent<Trap> ().destroyTrap ();
				}
			}
		}

		// Characteristic bonus
		if (bonusActive) {
			if (bonus.getType () == PickUp.PickUpType.Speed) {
				if (bonusTimer < speedBonusDuration) {
					bonusTimer++;
					//Debug.Log (bonusTimer);
				} else {
					moveSpeed = baseMoveSpeed;
					moveSpeedThreshold = baseMoveSpeedThreshold;
					bonusTimer = 0;
					bonusActive = false;
					bonus = null;

				}
			}
			else if (bonus.getType () == PickUp.PickUpType.Strength) {
				if (bonusTimer < strengthBonusDuration) {
					bonusTimer++;
				} else {
					rb.mass -= strengthBonus;
					Debug.Log (rb.mass);
					bonusTimer = 0;
					bonusActive = false;



					// Taille normale
					//transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
					transform.localScale = new Vector3(1, 1, 1);

					bonus = null;

				}
			}
			else if (bonus.getType () == PickUp.PickUpType.Lightness) {
				if (bonusTimer < lightnessBonusDuration) {
					bonusTimer++;
				} else {
					isLight = false;
					bonusTimer = 0;
					bonusActive = false;

					// Taille normale
					//transform.GetChild(0).transform.localScale = new Vector3(1, 1, 1);
					transform.localScale = new Vector3(1, 1, 1);

					bonus = null;
				}
			}
		}
			
	}

	void OnTriggerEnter(Collider other)
	{
		// Triggers a pickup
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			if (!itemHold) {
				item = other.gameObject.GetComponent<PickUp> ();
				//Debug.Log (gameObject.name + " ITEM RAMASSE : " + item.getType ());

				itemHold = true;

				// Script Management
				// P1
				if (playerName.Equals("Player1")){
					if (item.getType() == PickUp.PickUpType.Speed){
						IMP1.sprite = SpriteSpeed;
					}
					if (item.getType() == PickUp.PickUpType.Strength){
						IMP1.sprite = SpriteStrength;
					}
					if (item.getType() == PickUp.PickUpType.Lightness){
						IMP1.sprite = SpriteLightness;
					}
					if (item.getType() == PickUp.PickUpType.Trap){
						IMP1.sprite = SpriteTrap;
					}
					if (item.getType() == PickUp.PickUpType.Bomb){
						IMP1.sprite = SpriteBomb;
					}
					if (item.getType() == PickUp.PickUpType.Sardine){
						IMP1.sprite = SpriteSardine;
					}
					if (item.getType() == PickUp.PickUpType.Jump){
						IMP1.sprite = SpriteJump;
					}
					if (item.getType() == PickUp.PickUpType.SnowBall){
						IMP1.sprite = SpriteSnowBall;
					}
				}
				// P2
				else if (playerName.Equals("Player2")){
					if (item.getType() == PickUp.PickUpType.Speed){
						IMP2.sprite = SpriteSpeed;
					}
					if (item.getType() == PickUp.PickUpType.Strength){
						IMP2.sprite = SpriteStrength;
					}
					if (item.getType() == PickUp.PickUpType.Lightness){
						IMP2.sprite = SpriteLightness;
					}
					if (item.getType() == PickUp.PickUpType.Trap){
						IMP2.sprite = SpriteTrap;
					}
					if (item.getType() == PickUp.PickUpType.Bomb){
						IMP2.sprite = SpriteBomb;
					}
					if (item.getType() == PickUp.PickUpType.Sardine){
						IMP2.sprite = SpriteSardine;
					}
					if (item.getType() == PickUp.PickUpType.Jump){
						IMP2.sprite = SpriteJump;
					}
					if (item.getType() == PickUp.PickUpType.SnowBall){
						IMP2.sprite = SpriteSnowBall;
					}
				}
				// P3
				else if (playerName.Equals("Player3")){
					if (item.getType() == PickUp.PickUpType.Speed){
						IMP3.sprite = SpriteSpeed;
					}
					if (item.getType() == PickUp.PickUpType.Strength){
						IMP3.sprite = SpriteStrength;
					}
					if (item.getType() == PickUp.PickUpType.Lightness){
						IMP3.sprite = SpriteLightness;
					}
					if (item.getType() == PickUp.PickUpType.Trap){
						IMP3.sprite = SpriteTrap;
					}
					if (item.getType() == PickUp.PickUpType.Bomb){
						IMP3.sprite = SpriteBomb;
					}
					if (item.getType() == PickUp.PickUpType.Sardine){
						IMP3.sprite = SpriteSardine;
					}
					if (item.getType() == PickUp.PickUpType.Jump){
						IMP3.sprite = SpriteJump;
					}
					if (item.getType() == PickUp.PickUpType.SnowBall){
						IMP3.sprite = SpriteSnowBall;
					}
				}
				// P4
				else if (playerName.Equals("Player4")){
					if (item.getType() == PickUp.PickUpType.Speed){
						IMP4.sprite = SpriteSpeed;
					}
					if (item.getType() == PickUp.PickUpType.Strength){
						IMP4.sprite = SpriteStrength;
					}
					if (item.getType() == PickUp.PickUpType.Lightness){
						IMP4.sprite = SpriteLightness;
					}
					if (item.getType() == PickUp.PickUpType.Trap){
						IMP4.sprite = SpriteTrap;
					}
					if (item.getType() == PickUp.PickUpType.Bomb){
						IMP4.sprite = SpriteBomb;
					}
					if (item.getType() == PickUp.PickUpType.Sardine){
						IMP4.sprite = SpriteSardine;
					}
					if (item.getType() == PickUp.PickUpType.Jump){
						IMP4.sprite = SpriteJump;
					}
					if (item.getType() == PickUp.PickUpType.SnowBall){
						IMP4.sprite = SpriteSnowBall;
					}
				}
				
				// Détruit le gameObject
				other.gameObject.GetComponent<PickUp> ().destroyPickUp ();
			}
		}
	}

	public void becomeTrapped(GameObject p_trap, int p_mode){

		if (!isTrapped) {
			Debug.Log ("bloqué");

			isTrapped = true;
			trap = p_trap;
			trapTimer = 0;

			// 1 = trap, 2 = snowball
			trapMode = p_mode;

			if (trapMode == 1) {
				transform.position = new Vector3 (trap.transform.position.x, transform.position.y, trap.transform.position.z);
			}

			// Blocked
			rb.angularVelocity = Vector3.zero;
			rb.velocity = Vector3.zero;
		}

	}


    void OnCollisionStay(Collision collisionInfo)
    {
		
		if (collisionInfo.gameObject.CompareTag ("Tile")) {

			if (!isLight) {

				Tuile t = collisionInfo.collider.gameObject.GetComponent<Tuile> ();

                if (!bBlockTileDestruction)
				    t.weakenTile();
			}
		}

		if (collisionInfo.gameObject.CompareTag ("Sardine")) {

			if (bonus == null || bonus.getType () != PickUp.PickUpType.Strength) {
				moveSpeed = 10;
				moveSpeedThreshold = 30;
			}
			else if (bonus.getType () == PickUp.PickUpType.Strength) {
				moveSpeed = baseMoveSpeed;
				moveSpeedThreshold = baseMoveSpeedThreshold;
			}
		}
    }

	void OnCollisionEnter(Collision collisionInfo)
	{
		if (collisionInfo.gameObject.CompareTag ("Sardine")) {

			if (bonus == null || bonus.getType () != PickUp.PickUpType.Strength) {
				moveSpeed = 10;
				moveSpeedThreshold = 30;
			}
			else if (bonus.getType () == PickUp.PickUpType.Strength) {
				moveSpeed = baseMoveSpeed;
				moveSpeedThreshold = baseMoveSpeedThreshold;
			}
		}
	}
		
	void OnCollisionExit(Collision collisionInfo)
	{
		if (collisionInfo.gameObject.CompareTag ("Sardine")) {

			moveSpeed = baseMoveSpeed;
			moveSpeedThreshold = baseMoveSpeedThreshold;
		}
	}

    public enum DashState 
	{
		Ready,
		Dashing,
		Cooldown
	}

	public DashState getDashState(){
		return dashState;
	}

	public float getDashTimer(){
		return dashTimer;
	}

	public float getDashCooldown(){
		return dashCooldown;
	}
}
