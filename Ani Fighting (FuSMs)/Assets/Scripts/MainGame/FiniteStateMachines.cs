using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachines : MonoBehaviour {
/*
    private static FiniteStateMachines instance;
    public static FiniteStateMachines Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<FiniteStateMachines>();
            }
            return instance;
        }
    }

    float totalRange;
    int totalLightAttack, totalHeavyAttack, totalRangedAttack,
        totalUpAttack, totalMiddleAttack, totalDownAttack;

    float totalInput, minRange, maxRange;

    float[] miuRange = new float[2]; //miuRangeMin, miuRangeMax
    float[] miuAttackType = new float[3]; //miuLightAttack, miuHeavyAttack, miuRangedAttack
    float[] miuAttackDirection = new float[3]; //miuUpDirection, miuMiddleDirection, miuBottomDirection

    float[] alphaRuleValue = new float[18];
    float totalRuleValue;

    float[] ruleValues = new float[18];

    [SerializeField]
    float[] finalRuleValues = new float[8];
    [SerializeField]
    MovementType.enemy[] movementType = new MovementType.enemy[8];

    [SerializeField]
    float choosenRuleIndex;
    public float ChoosenRuleIndex
    {
        get
        {
            return choosenRuleIndex;
        }
    }

    [SerializeField]
    FSMLogHistory FSMLogHistory;

    void Start()
    {
        FSMLogHistory = FindObjectOfType<FSMLogHistory>();
    }

    public void initiateFSM()
    {
        for (int i = 0; i < finalRuleValues.Length; i++)
        {
            finalRuleValues[i] = 0;
            movementType[i] = (MovementType.enemy)i;
        }
    }

    public void runFSM()
    {
        totalInput = PlayerInputManager.instance.getTotalInput().getTotalMovement();
        minRange = Player.Instance.Target.GetComponent<Enemy>().NearRange;
        maxRange = Player.Instance.Target.GetComponent<Enemy>().FarRange;

        totalRange = Mathf.Abs(Player.Instance.gameObject.transform.position.x - Player.Instance.Target.transform.position.x);
        totalLightAttack = PlayerInputManager.instance.getCountMovement(0).getTotalMovement() + 1;
        totalHeavyAttack = PlayerInputManager.instance.getCountMovement(1).getTotalMovement() + 1;
        totalRangedAttack = PlayerInputManager.instance.getCountMovement(2).getTotalMovement() + 1;
        totalUpAttack = PlayerInputManager.instance.getCountMovement(3).getTotalMovement() + 1;
        totalMiddleAttack = PlayerInputManager.instance.getCountMovement(4).getTotalMovement() + 1;
        totalDownAttack = PlayerInputManager.instance.getCountMovement(5).getTotalMovement() + 1;

        miuRange[0] = calculateMiuValue(totalRange, false, true);
        miuRange[1] = calculateMiuValue(totalRange, true, true);
        miuAttackType[0] = calculateMiuValue(totalLightAttack, true, false);
        miuAttackType[1] = calculateMiuValue(totalHeavyAttack, true, false);
        miuAttackType[2] = calculateMiuValue(totalRangedAttack, true, false);
        miuAttackDirection[0] = calculateMiuValue(totalUpAttack, true, false);
        miuAttackDirection[1] = calculateMiuValue(totalMiddleAttack, true, false);
        miuAttackDirection[2] = calculateMiuValue(totalDownAttack, true, false);

        totalRuleValue = 0;
        for (int i = 0; i < miuRange.Length; i++)
        {
            for (int j = 0; j < miuAttackDirection.Length; j++)
            {
                for (int k = 0; k < miuAttackType.Length; k++)
                {
                    alphaRuleValue[i * 9 + j * 3 + k] = calculateRuleAND(new float[] { miuRange[i], miuAttackDirection[j], miuAttackType[k] });
                    totalRuleValue += alphaRuleValue[i * 9 + j * 3 + k];
                }
            }
        }

        for (int i = 0; i < ruleValues.Length; i++)
        {
            ruleValues[i] = alphaRuleValue[i] / totalRuleValue;

            /*for (int j = 0; j < movementType.Length; j++)
            {
                if (Rule.Instance.ruleAfterIdle[i] == movementType[j])
                {
                    finalRuleValues[j] += ruleValues[i];
                    continue;
                }
            } * /

            //Debug.Log(Player.Instance.Target.GetComponent<Enemy>().CurrentState.getStateName());
            switch (Player.Instance.Target.GetComponent<Enemy>().CurrentState.getStateName())
            {
                //masih bug sedikit disini
                case "idle":
                    for (int j = 0; j < movementType.Length; j++)
                    {
                        if (Rule.Instance.ruleAfterIdle[i] == movementType[j])
                        {
                            finalRuleValues[j] += ruleValues[i];
                            continue;
                        }
                    }
                    break;
                case "walk":
                    for (int j = 0; j < movementType.Length; j++)
                    {
                        if (Rule.Instance.ruleAfterWalk[i] == movementType[j])
                        {
                            finalRuleValues[j] += ruleValues[i];
                            continue;
                        }
                    }
                    break;
                case "walkBackward":
                    for (int j = 0; j < movementType.Length; j++)
                    {
                        if (Rule.Instance.ruleAfterWalkBackward[i] == movementType[j])
                        {
                            finalRuleValues[j] += ruleValues[i];
                            continue;
                        }
                    }
                    break;
                case "lightAttack":
                    for (int j = 0; j < movementType.Length; j++)
                    {
                        if (Rule.Instance.ruleAfterLightAttack[i] == movementType[j])
                        {
                            finalRuleValues[j] += ruleValues[i];
                            continue;
                        }
                    }
                    break;

                case "heavyAttack":
                    for (int j = 0; j < movementType.Length; j++)
                    {
                        if (Rule.Instance.ruleAfterHeavyAttack[i] == movementType[j])
                        {
                            finalRuleValues[j] += ruleValues[i];
                            continue;
                        }
                    }
                    break;
                case "rangedAttack":
                    for (int j = 0; j < movementType.Length; j++)
                    {
                        if (Rule.Instance.ruleAfterRangedAttack[i] == movementType[j])
                        {
                            finalRuleValues[j] += ruleValues[i];
                            continue;
                        }
                    }
                    break;
                case "jump":
                    for (int j = 0; j < movementType.Length; j++)
                    {
                        if (Rule.Instance.ruleAfterJump[i] == movementType[j])
                        {
                            finalRuleValues[j] += ruleValues[i];
                            continue;
                        }
                    }
                    break;
                case "crouch":
                    for (int j = 0; j < movementType.Length; j++)
                    {
                        if (Rule.Instance.ruleAfterCrouch[i] == movementType[j])
                        {
                            finalRuleValues[j] += ruleValues[i];
                            continue;
                        }
                    }
                    break;
            }
        }

        float randomValue = (float)Random.Range(1, 100);
        float tempTotal = 0;
        for (int i = 0; i < finalRuleValues.Length; i++)
        {
            if (tempTotal < randomValue)
            {
                tempTotal += (finalRuleValues[i] * 100);
            }

            if (tempTotal >= randomValue)
            {
                choosenRuleIndex = i;
                break;
            }
        }
        //----------------------------------------------------------------

        FSMLogHistory.addTransition(Player.Instance.Target.GetComponent<Enemy>().CurrentState.getStateName(),
            totalRange, totalLightAttack - 1, totalHeavyAttack - 1, totalRangedAttack - 1,
            totalUpAttack - 1, totalMiddleAttack - 1, totalDownAttack - 1,
            finalRuleValues, ((MovementType.enemy)ChoosenRuleIndex).ToString());
    }

    float calculateMiuValue(float value, bool isUpLinier, bool isRangeType)
    {
        float miuValue, maxValue, minValue;
        maxValue = totalInput;
        minValue = 0;

        if (isRangeType)
        {
            maxValue = maxRange;
            minValue = minRange;
        }

        if (isUpLinier)
        {
            if (value <= minValue)
            {
                miuValue = 0;
            }
            else if (value > minValue && value < maxValue)
            {
                miuValue = (value - minValue) / (maxValue - minValue);
            }
            else
            {
                miuValue = 1;
            }
        }
        else
        {
            if (value <= minValue)
            {

                miuValue = 1;
            }
            else if (value > minValue && value < maxValue)
            {
                miuValue = (maxValue - value) / (maxValue - minValue);
            }
            else //(value >= maxValue)
            {
                miuValue = 0;
            }
        }

        return miuValue;
    }

    float calculateRuleAND(float[] values)
    {
        float result = 0;

        if (values.Length > 0)
        {
            result = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] < result)
                {
                    result = values[i];
                }
            }
        }

        return result;
    }

    float calculateRuleOR(float[] values)
    {
        float result = 0;

        if (values.Length > 0)
        {
            result = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] > result)
                {
                    result = values[i];
                }
            }
        }

        return result;
    }

    float[] getRuleValues()
    {
        return ruleValues;
    }*/

    private static FiniteStateMachines instance;
    public static FiniteStateMachines Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<FiniteStateMachines>();
            }
            return instance;
        }
    }

    float totalRange;
    int totalLightAttack, totalHeavyAttack, totalRangedAttack,
        totalUpAttack, totalMiddleAttack, totalDownAttack;

    float totalInput, minRange, maxRange;

    float[] ruleValues = new float[18];

    [SerializeField]
    float[] finalRuleValues = new float[8];
    [SerializeField]
    MovementType.enemy[] movementType = new MovementType.enemy[8];

    [SerializeField]
    float choosenRuleIndex;
    public float ChoosenRuleIndex
    {
        get
        {
            return choosenRuleIndex;
        }
    }

    [SerializeField]
    FSMLogHistory FSMLogHistory;

    enum RangeBetweenChara
    {
        near,
        far
    };

    enum AttackTypeHabitude
    {
        lightAttack,
        heavyAttack,
        rangedAttack
    };

    enum AttackDirectionHabitude
    {
        up,
        middle,
        down
    };

    RangeBetweenChara range;
    AttackTypeHabitude attackTypeHabitude;
    AttackDirectionHabitude attackDirectionHabitude;

    void Start()
    {
        FSMLogHistory = FindObjectOfType<FSMLogHistory>();
    }

    public void initiateFSM()
    {
        for (int i = 0; i < finalRuleValues.Length; i++)
        {
            finalRuleValues[i] = 0;
            movementType[i] = (MovementType.enemy)i;
        }
        for (int i = 0; i < ruleValues.Length; i++)
        {
            ruleValues[i] = 0;
        }
    }

    public void runFSM()
    {
        totalInput = PlayerInputManager.instance.getTotalInput().getTotalMovement();
        minRange = Player.Instance.Target.GetComponent<Enemy>().NearRange;
        maxRange = Player.Instance.Target.GetComponent<Enemy>().FarRange;

        totalRange = Mathf.Abs(Player.Instance.gameObject.transform.position.x - Player.Instance.Target.transform.position.x);
        totalLightAttack = PlayerInputManager.instance.getCountMovement(0).getTotalMovement() + 1;
        totalHeavyAttack = PlayerInputManager.instance.getCountMovement(1).getTotalMovement() + 1;
        totalRangedAttack = PlayerInputManager.instance.getCountMovement(2).getTotalMovement() + 1;
        totalUpAttack = PlayerInputManager.instance.getCountMovement(3).getTotalMovement() + 1;
        totalMiddleAttack = PlayerInputManager.instance.getCountMovement(4).getTotalMovement() + 1;
        totalDownAttack = PlayerInputManager.instance.getCountMovement(5).getTotalMovement() + 1;

        range = getRangeBetweenChara_now();
        attackTypeHabitude = getAttackTypeHabitude_now();
        attackDirectionHabitude = getAttackDirection_now();

        switch (range)
        {
            case RangeBetweenChara.near:
                switch (attackDirectionHabitude)
                {
                    case AttackDirectionHabitude.up:
                        switch (attackTypeHabitude)
                        {
                            case AttackTypeHabitude.lightAttack:
                                ruleValues[0] = 1;
                                break;
                            case AttackTypeHabitude.heavyAttack:
                                ruleValues[1] = 1;
                                break;
                            case AttackTypeHabitude.rangedAttack:
                                ruleValues[2] = 1;
                                break;
                        }
                        break;
                    case AttackDirectionHabitude.middle:
                        switch (attackTypeHabitude)
                        {
                            case AttackTypeHabitude.lightAttack:
                                ruleValues[3] = 1;
                                break;
                            case AttackTypeHabitude.heavyAttack:
                                ruleValues[4] = 1;
                                break;
                            case AttackTypeHabitude.rangedAttack:
                                ruleValues[5] = 1;
                                break;
                        }
                        break;
                    case AttackDirectionHabitude.down:
                        switch (attackTypeHabitude)
                        {
                            case AttackTypeHabitude.lightAttack:
                                ruleValues[6] = 1;
                                break;
                            case AttackTypeHabitude.heavyAttack:
                                ruleValues[7] = 1;
                                break;
                            case AttackTypeHabitude.rangedAttack:
                                ruleValues[8] = 1;
                                break;
                        }
                        break;
                }
                break;
            case RangeBetweenChara.far:
                switch (attackDirectionHabitude)
                {
                    case AttackDirectionHabitude.up:
                        switch (attackTypeHabitude)
                        {
                            case AttackTypeHabitude.lightAttack:
                                ruleValues[9] = 1;
                                break;
                            case AttackTypeHabitude.heavyAttack:
                                ruleValues[10] = 1;
                                break;
                            case AttackTypeHabitude.rangedAttack:
                                ruleValues[11] = 1;
                                break;
                        }
                        break;
                    case AttackDirectionHabitude.middle:
                        switch (attackTypeHabitude)
                        {
                            case AttackTypeHabitude.lightAttack:
                                ruleValues[12] = 1;
                                break;
                            case AttackTypeHabitude.heavyAttack:
                                ruleValues[13] = 1;
                                break;
                            case AttackTypeHabitude.rangedAttack:
                                ruleValues[14] = 1;
                                break;
                        }
                        break;
                    case AttackDirectionHabitude.down:
                        switch (attackTypeHabitude)
                        {
                            case AttackTypeHabitude.lightAttack:
                                ruleValues[15] = 1;
                                break;
                            case AttackTypeHabitude.heavyAttack:
                                ruleValues[16] = 1;
                                break;
                            case AttackTypeHabitude.rangedAttack:
                                ruleValues[17] = 1;
                                break;
                        }
                        break;
                }
                break;
        }

        for (int i = 0; i < ruleValues.Length; i++)
        {
            switch (Player.Instance.Target.GetComponent<Enemy>().CurrentState.getStateName())
            {
                case "idle":
                    for (int j = 0; j < movementType.Length; j++)
                    {
                        if (Rule.Instance.ruleAfterIdle[i] == movementType[j])
                        {
                            finalRuleValues[j] += ruleValues[i];
                            continue;
                        }
                    }
                    break;
                case "walk":
                    for (int j = 0; j < movementType.Length; j++)
                    {
                        if (Rule.Instance.ruleAfterWalk[i] == movementType[j])
                        {
                            finalRuleValues[j] += ruleValues[i];
                            continue;
                        }
                    }
                    break;
                case "walkBackward":
                    for (int j = 0; j < movementType.Length; j++)
                    {
                        if (Rule.Instance.ruleAfterWalkBackward[i] == movementType[j])
                        {
                            finalRuleValues[j] += ruleValues[i];
                            continue;
                        }
                    }
                    break;
                case "lightAttack":
                    for (int j = 0; j < movementType.Length; j++)
                    {
                        if (Rule.Instance.ruleAfterLightAttack[i] == movementType[j])
                        {
                            finalRuleValues[j] += ruleValues[i];
                            continue;
                        }
                    }
                    break;

                case "heavyAttack":
                    for (int j = 0; j < movementType.Length; j++)
                    {
                        if (Rule.Instance.ruleAfterHeavyAttack[i] == movementType[j])
                        {
                            finalRuleValues[j] += ruleValues[i];
                            continue;
                        }
                    }
                    break;
                case "rangedAttack":
                    for (int j = 0; j < movementType.Length; j++)
                    {
                        if (Rule.Instance.ruleAfterRangedAttack[i] == movementType[j])
                        {
                            finalRuleValues[j] += ruleValues[i];
                            continue;
                        }
                    }
                    break;
                case "jump":
                    for (int j = 0; j < movementType.Length; j++)
                    {
                        if (Rule.Instance.ruleAfterJump[i] == movementType[j])
                        {
                            finalRuleValues[j] += ruleValues[i];
                            continue;
                        }
                    }
                    break;
                case "crouch":
                    for (int j = 0; j < movementType.Length; j++)
                    {
                        if (Rule.Instance.ruleAfterCrouch[i] == movementType[j])
                        {
                            finalRuleValues[j] += ruleValues[i];
                            continue;
                        }
                    }
                    break;
            }
        }


        float randomValue = (float)Random.Range(1, 100);
        float tempTotal = 0;
        for (int i = 0; i < finalRuleValues.Length; i++)
        {
            if (tempTotal < randomValue)
            {
                tempTotal += (finalRuleValues[i] * 100);
            }

            if (tempTotal >= randomValue)
            {
                choosenRuleIndex = i;
                break;
            }
        }

        FSMLogHistory.addTransition(Player.Instance.Target.GetComponent<Enemy>().CurrentState.getStateName(),
            totalRange, totalLightAttack - 1, totalHeavyAttack - 1, totalRangedAttack - 1,
            totalUpAttack - 1, totalMiddleAttack - 1, totalDownAttack - 1,
            finalRuleValues, ((MovementType.enemy)ChoosenRuleIndex).ToString());
    }

    RangeBetweenChara getRangeBetweenChara_now()
    {
        if (totalRange > minRange)
            return RangeBetweenChara.far;
        return RangeBetweenChara.near;
    }

    AttackTypeHabitude getAttackTypeHabitude_now()
    {
        if (totalLightAttack >= totalHeavyAttack && totalLightAttack >= totalRangedAttack)
            return AttackTypeHabitude.lightAttack;
        if (totalHeavyAttack >= totalLightAttack && totalHeavyAttack >= totalRangedAttack)
            return AttackTypeHabitude.heavyAttack;
        return AttackTypeHabitude.rangedAttack;
    }

    AttackDirectionHabitude getAttackDirection_now()
    {
        if (totalUpAttack >= totalMiddleAttack && totalUpAttack >= totalDownAttack)
            return AttackDirectionHabitude.up;
        if (totalDownAttack >= totalMiddleAttack && totalDownAttack >= totalUpAttack)
            return AttackDirectionHabitude.down;
        return AttackDirectionHabitude.middle;
    }
}
