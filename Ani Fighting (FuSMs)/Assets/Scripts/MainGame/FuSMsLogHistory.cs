using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuSMsLogHistory : MonoBehaviour {

    [System.Serializable]
    class TransitionData
    {
        [System.Serializable]
        struct TotalAttackType
        {
            public int light, heavy, ranged;
        }

        [System.Serializable]
        struct TotalAttackDirection
        {
            public int up, middle, down;
        }

        [System.Serializable]
        struct NextStatePersentage
        {
            public float idle, walk, walkBackward, lightAttack, heavyAttack, rangedAttack, jump, crouch;
        }

        public int transitionNumber;
		public string previousState;
        //MovementType.enemy previousState; sementara string
		public float range;
        [SerializeField]
        TotalAttackType totalAttackType;
        [SerializeField]
        TotalAttackDirection totalAttackDirection;
        [SerializeField]
        NextStatePersentage nextStatePersentage;
		public string choosenStage;
        //MovementType.enemy choosenStage; sementara string

        public TransitionData(int transitionNumber, string previousState, float range,
        int totalLightAttack, int totalHeavyAttack, int totalRangedAttack, int totalUpAttack, int totalMiddleAttack, int totalDownAttack,
        float[] nextStateValuePersentage, string choosenStage)
        {
            this.transitionNumber = transitionNumber;
            this.previousState = previousState;
            this.range = range;

            this.totalAttackType.light = totalLightAttack;
            this.totalAttackType.heavy = totalHeavyAttack;
            this.totalAttackType.ranged = totalRangedAttack;
            this.totalAttackDirection.up = totalUpAttack;
            this.totalAttackDirection.middle = totalMiddleAttack;
            this.totalAttackDirection.down = totalDownAttack;

            this.nextStatePersentage.idle = nextStateValuePersentage[0];
            this.nextStatePersentage.walk = nextStateValuePersentage[1];
            this.nextStatePersentage.walkBackward = nextStateValuePersentage[2];
            this.nextStatePersentage.lightAttack = nextStateValuePersentage[3];
            this.nextStatePersentage.heavyAttack = nextStateValuePersentage[4];
            this.nextStatePersentage.rangedAttack = nextStateValuePersentage[5];
            this.nextStatePersentage.jump = nextStateValuePersentage[6];
            this.nextStatePersentage.crouch = nextStateValuePersentage[7];

            this.choosenStage = choosenStage;
        }
    }

    [SerializeField]
    List<TransitionData> transitionData;
    int totalTransition;

	void Start () {
        transitionData.Clear();
        totalTransition = 0;
    }

    void Update()
    {
        totalTransition = transitionData.Count;
    }

    public void addTransition(string previousState, float range,
        int totalLightAttack, int totalHeavyAttack, int totalRangedAttack, int totalUpAttack, int totalMiddleAttack, int totalDownAttack,
        float[] nextStatePersentage, string choosenStage)
    {
        transitionData.Add(new TransitionData(totalTransition, previousState, range, totalLightAttack, totalHeavyAttack, totalRangedAttack,
            totalUpAttack, totalMiddleAttack, totalUpAttack, nextStatePersentage, choosenStage));
    }
}
