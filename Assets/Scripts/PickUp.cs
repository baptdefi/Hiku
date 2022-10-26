using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {

	public enum PickUpType 
	{
		Speed,
		Strength,
		Lightness,
		Trap,
		Bomb,
		Sardine,
		Jump,
		SnowBall
	}

	private PickUpType type;

	private float bonusMoveSpeed;
	private float bonusMoveSpeedThreshold;
	private float bonusStrenght;
	private float bonusJump;

	// Use this for initialization
	void Start () {

        int number = Random.Range (0, 10);
        
        // TEST
        //int number = 3;

		if (number == 0) {
			Init (PickUp.PickUpType.Speed);
		}
		if (number == 1) {
			Init (PickUp.PickUpType.Strength);
		}
		if (number == 2) {
			Init (PickUp.PickUpType.Lightness);
		}
		if (number == 3) {
			Init (PickUp.PickUpType.Trap);
		}
		if (number == 4) {
			Init (PickUp.PickUpType.Bomb);
		}
		if (number == 5) {
			Init (PickUp.PickUpType.Jump);
		}
		if (number == 6) {
			Init (PickUp.PickUpType.SnowBall);
		}
		if (number == 7) {
			Init (PickUp.PickUpType.SnowBall);
		}
		if (number == 8) {
			Init (PickUp.PickUpType.Sardine);
		}
		if (number == 9) {
			Init (PickUp.PickUpType.Sardine);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -2.5f) {
            // pickup is out of play area
            destroyPickUp();
		}
	}

	public void Init(PickUpType p_type){

		type = p_type;

		if (p_type == PickUp.PickUpType.Speed) {
			bonusMoveSpeed = 10f;
			bonusMoveSpeedThreshold = 30f;
		}
		else if (p_type == PickUp.PickUpType.Strength) {
			bonusStrenght = 10f;
		}
		else if (p_type == PickUp.PickUpType.Jump) {
			bonusJump = 25f;
		}
	}

    // Destry pickup and update GameManager bonusCount
	public void destroyPickUp(){
		GameManagerScript.removeBonus ();
		Destroy (transform.parent.gameObject);
	}

	public void setType(PickUpType p_type){
		type = p_type;
	}

	public PickUpType getType(){
		return type;
	}

	public void setBonusMoveSpeed(float p_bonusMoveSpeed){
		bonusMoveSpeed = p_bonusMoveSpeed;
	}

	public float getBonusMoveSpeed(){
		return bonusMoveSpeed;
	}

	public void setBonusStrenght(float p_bonusStrenght){
		bonusStrenght = p_bonusStrenght;
	}

	public float getBonusStrenght(){
		return bonusStrenght;
	}

	public void setBonusMoveSpeedThreshold(float p_bonusMoveSpeedThreshold){
		bonusMoveSpeedThreshold = p_bonusMoveSpeedThreshold;
	}

	public float getBonusMoveSpeedThreshold(){
		return bonusMoveSpeedThreshold;
	}

	public float getBonusJump(){
		return bonusJump;
	}

}
