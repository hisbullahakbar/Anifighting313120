using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {

    [System.Serializable]
    public class CountMovement
    {
        [SerializeField]
        string name;

        [SerializeField]
        int totalMovementLaunched;

        public void addLaunchedMovement(int launchedTimes)
        {
            totalMovementLaunched += launchedTimes;
        }

        public void resetTotal()
        {
            totalMovementLaunched = 0;
        }

        public float getTotalMovement()
        {
            return totalMovementLaunched;
        }
    }

    [SerializeField]
    private CountMovement[] countMovements;
    [SerializeField]
    private CountMovement totalInput;

    public static PlayerInputManager instance;

	void Start () {
        instance = this;
        for (int i = 0; i < countMovements.Length; i++)
        {
            countMovements[i].resetTotal();
        }
        totalInput.resetTotal();
	}

    public CountMovement getCountMovement(int i) {
        return countMovements[i];
    }
    
    public CountMovement getCountMovement(MovementType.playerCounter type)
    {
        return countMovements[(int)type];
    }

    public CountMovement getTotalInput()
    {
        return totalInput;
    }
}