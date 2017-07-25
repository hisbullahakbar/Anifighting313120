using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaBattleInfoManager : MonoBehaviour {

    [SerializeField]
    GameObject gameModeIcon, cbiPlayerIcon, cbiEnemyIcon, cbiVSIcon;

    bool isGameModeIconMoving;
    bool isCBIPlayerShow, isCBIEnemyShow, isCBIVSIconShow;
    bool isEndTime;

	void Start () {
        isGameModeIconMoving = false;
        isCBIPlayerShow = false;
        isCBIEnemyShow = false;
        isCBIVSIconShow = false;
        isEndTime = false;
    }

    void Update()
    {
        if (BattleSceneManager.Instance.State == BattleSceneManager.BattleSceneState.characterInfo)
        {
            if (isGameModeIconMoving)
            {
                gameModeIcon.transform.position = Vector3.MoveTowards(gameModeIcon.transform.position,
                    new Vector3(gameModeIcon.transform.position.x, 3.9f, gameModeIcon.transform.position.z), 0.4f);
                gameModeIcon.transform.localScale = Vector3.Lerp(gameModeIcon.transform.localScale, Vector3.one * 1.1f, 0.4f);
                StopCoroutine(movingGameModeIcon());
            }
            if (isCBIPlayerShow)
            {
                cbiPlayerIcon.transform.position = Vector3.MoveTowards(cbiPlayerIcon.transform.position,
                    new Vector3(-4.3f, cbiPlayerIcon.transform.position.y, cbiPlayerIcon.transform.position.z), 0.5f);
                StopCoroutine(showingCBIPlayerIcon());
            }
            if (isCBIEnemyShow)
            {
                cbiEnemyIcon.transform.position = Vector3.MoveTowards(cbiEnemyIcon.transform.position,
                    new Vector3(4.3f, cbiEnemyIcon.transform.position.y, cbiEnemyIcon.transform.position.z), 0.5f);
                StopCoroutine(showingCBIEnemyIcon());
            }
            if (isCBIVSIconShow)
            {
                cbiVSIcon.transform.localScale = Vector3.Lerp(cbiVSIcon.transform.localScale, Vector3.one, 0.1f);
                StopCoroutine(showingCBIVSIcon());
            }
            if (isEndTime)
            {
                StopCoroutine(gotoEndTime());
                BattleSceneManager.Instance.State = BattleSceneManager.BattleSceneState.beginingPose;
                Destroy(gameObject, 0.25f);
            }

            StartCoroutine(movingGameModeIcon());
            StartCoroutine(showingCBIPlayerIcon());
            StartCoroutine(showingCBIEnemyIcon());
            StartCoroutine(showingCBIVSIcon());
            StartCoroutine(gotoEndTime());
        }
    }

    private IEnumerator movingGameModeIcon()
    {
        yield return new WaitForSeconds(1f);
        isGameModeIconMoving = true;
    }

    private IEnumerator showingCBIPlayerIcon()
    {
        yield return new WaitForSeconds(2f);
        isCBIPlayerShow = true;
    }

    private IEnumerator showingCBIEnemyIcon()
    {
        yield return new WaitForSeconds(2f);
        isCBIEnemyShow = true;
    }

    private IEnumerator showingCBIVSIcon()
    {
        yield return new WaitForSeconds(3f);
        isCBIVSIconShow = true;
    }

    private IEnumerator gotoEndTime()
    {
        yield return new WaitForSeconds(5f);
        cbiVSIcon.GetComponent<SpriteRenderer>().enabled = false;
        
        //yield return new WaitForSeconds(0.5f);
        cbiPlayerIcon.GetComponent<SpriteRenderer>().enabled = false;
        cbiEnemyIcon.GetComponent<SpriteRenderer>().enabled = false;
        
        //yield return new WaitForSeconds(1f);
        isEndTime = true;
    }
}
