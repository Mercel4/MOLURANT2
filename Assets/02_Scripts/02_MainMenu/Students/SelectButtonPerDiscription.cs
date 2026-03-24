using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;
using System;
using Unity.VisualScripting;

public class SelectButtonPerDiscription : MonoBehaviour
{
    [InfoBox("시로코, AL1S 캐릭터 선택 및 스킬 설명 패널 제어")]
    [FoldoutGroup("Pannels")] public GameObject ShirokoPannel;
    [FoldoutGroup("Pannels")] public GameObject AL1SPannel;
    [FoldoutGroup("ST Background")] public GameObject Shiroko_Background;
    [FoldoutGroup("ST Background")] public GameObject AL1S_Background;
    [FoldoutGroup("SkillButtons_Shiroko")] public GameObject shirkoSkillButtonC;
    [FoldoutGroup("SkillButtons_Shiroko")] public GameObject shirkoSkillButtonQ;
    [FoldoutGroup("SkillButtons_Shiroko")] public GameObject shirkoSkillButtonE;
    [FoldoutGroup("SkillButtons_Shiroko")] public GameObject shirkoSkillButtonX;
    [FoldoutGroup("SkillButtons_AL1S")] public GameObject AL1S_SkillButtonC;
    [FoldoutGroup("SkillButtons_AL1S")] public GameObject AL1S_SkillButtonQ;
    [FoldoutGroup("SkillButtons_AL1S")] public GameObject AL1S_SkillButtonE;
    [FoldoutGroup("SkillButtons_AL1S")] public GameObject AL1S_SkillButtonX;

    [FoldoutGroup("Descriptions_Shiroko")] public GameObject shirkoDescription;
    [FoldoutGroup("Descriptions_Shiroko")] public GameObject shirkoSkillPannelC;
    [FoldoutGroup("Descriptions_Shiroko")] public GameObject shirkoSkillPannelQ;
    [FoldoutGroup("Descriptions_Shiroko")] public GameObject shirkoSkillPannelE;
    [FoldoutGroup("Descriptions_Shiroko")] public GameObject shirkoSkillPannelX;

    [FoldoutGroup("Descriptions_AL1S")] public GameObject AL1S_Description;
    [FoldoutGroup("Descriptions_AL1S")] public GameObject AL1S_SkillPannelC;
    [FoldoutGroup("Descriptions_AL1S")] public GameObject AL1S_SkillPannelQ;
    [FoldoutGroup("Descriptions_AL1S")] public GameObject AL1S_SkillPannelE;
    [FoldoutGroup("Descriptions_AL1S")] public GameObject AL1S_SkillPannelX;

    private string currentCharacter;
    private Vector3 AL1S_OriginalPos;
    private Vector3 Shiroko_OriginalPos;

    private void Awake()
    {
        // 시로코 기본
        currentCharacter = "Shiroko";

        ShirokoPannel.SetActive(true);
        Shiroko_Background.SetActive(false);

        shirkoSkillButtonC.SetActive(true);
        shirkoSkillButtonQ.SetActive(true);
        shirkoSkillButtonE.SetActive(true);
        shirkoSkillButtonX.SetActive(true);

        shirkoDescription.SetActive(true);
        shirkoSkillPannelC.SetActive(false);
        shirkoSkillPannelQ.SetActive(false);
        shirkoSkillPannelE.SetActive(false);
        shirkoSkillPannelX.SetActive(false);
    }

    private void Start()
    {
        AL1S_OriginalPos = AL1S_Background.transform.position;
        Shiroko_OriginalPos = Shiroko_Background.transform.position;
    }

    public void OnClick_ShirokoButton()
    {
        if (currentCharacter == "Shiroko") return;
        currentCharacter = "Shiroko";

        Shiroko_Background.SetActive(true);
        ShirokoPannel.SetActive(true);

        if (AL1S_Background != null)
        {
            StartCoroutine(MoveToPosition(Shiroko_Background, "right"));
            StartCoroutine(MoveToPosition(AL1S_Background, "reset"));
        }

        shirkoSkillButtonC.SetActive(true);
        shirkoSkillButtonQ.SetActive(true);
        shirkoSkillButtonE.SetActive(true);
        shirkoSkillButtonX.SetActive(true);

        shirkoDescription.SetActive(true);
        shirkoSkillPannelC.SetActive(false);
        shirkoSkillPannelQ.SetActive(false);
        shirkoSkillPannelE.SetActive(false);
        shirkoSkillPannelX.SetActive(false);

        AL1SPannel.SetActive(false);

        AL1S_SkillButtonC.SetActive(false);
        AL1S_SkillButtonQ.SetActive(false);
        AL1S_SkillButtonE.SetActive(false);
        AL1S_SkillButtonX.SetActive(false);
        AL1S_Description.SetActive(false);
        AL1S_SkillPannelC.SetActive(false);
        AL1S_SkillPannelQ.SetActive(false);
        AL1S_SkillPannelE.SetActive(false);
        AL1S_SkillPannelX.SetActive(false);
    }

    public void OnClick_AL1SButton()
    {
        if (currentCharacter == "AL1S") return;
        currentCharacter = "AL1S";

        AL1SPannel.SetActive(true);

        if (Shiroko_Background != null)
        {
            StartCoroutine(MoveToPosition(AL1S_Background, "left"));
            StartCoroutine(MoveToPosition(Shiroko_Background, "reset"));
        }

        AL1S_SkillButtonC.SetActive(true);
        AL1S_SkillButtonQ.SetActive(true);
        AL1S_SkillButtonE.SetActive(true);
        AL1S_SkillButtonX.SetActive(true);

        AL1S_Description.SetActive(true);
        AL1S_SkillPannelC.SetActive(false);
        AL1S_SkillPannelQ.SetActive(false);
        AL1S_SkillPannelE.SetActive(false);
        AL1S_SkillPannelX.SetActive(false);

        ShirokoPannel.SetActive(false);

        shirkoSkillButtonC.SetActive(false);
        shirkoSkillButtonQ.SetActive(false);
        shirkoSkillButtonE.SetActive(false);
        shirkoSkillButtonX.SetActive(false);
        shirkoDescription.SetActive(false);
        shirkoSkillPannelC.SetActive(false);
        shirkoSkillPannelQ.SetActive(false);
        shirkoSkillPannelE.SetActive(false);
        shirkoSkillPannelX.SetActive(false);
    }

    // 스킬 버튼 클릭 시로코
    public void OnClick_ShirokoSkillButtonC()
    {
        shirkoDescription.SetActive(false);
        shirkoSkillPannelC.SetActive(true);
        shirkoSkillPannelQ.SetActive(false);
        shirkoSkillPannelE.SetActive(false);
        shirkoSkillPannelX.SetActive(false);
    }
    public void OnClick_ShirokoSkillButtonQ()
    {
        shirkoDescription.SetActive(false);
        shirkoSkillPannelC.SetActive(false);
        shirkoSkillPannelQ.SetActive(true);
        shirkoSkillPannelE.SetActive(false);
        shirkoSkillPannelX.SetActive(false);
    }
    public void OnClick_ShirokoSkillButtonE()
    {
        shirkoDescription.SetActive(false);
        shirkoSkillPannelC.SetActive(false);
        shirkoSkillPannelQ.SetActive(false);
        shirkoSkillPannelE.SetActive(true);
        shirkoSkillPannelX.SetActive(false);
    }
    public void OnClick_ShirokoSkillButtonX()
    {
        shirkoDescription.SetActive(false);
        shirkoSkillPannelC.SetActive(false);
        shirkoSkillPannelQ.SetActive(false);
        shirkoSkillPannelE.SetActive(false);
        shirkoSkillPannelX.SetActive(true);
    }

    // 스킬 버튼 클릭 AL1S
    public void OnClick_AL1SSkillButtonC()
    {
        AL1S_Description.SetActive(false);
        AL1S_SkillPannelC.SetActive(true);
        AL1S_SkillPannelQ.SetActive(false);
        AL1S_SkillPannelE.SetActive(false);
        AL1S_SkillPannelX.SetActive(false);
    }
    public void OnClick_AL1SSkillButtonQ()
    {
        AL1S_Description.SetActive(false);
        AL1S_SkillPannelC.SetActive(false);
        AL1S_SkillPannelQ.SetActive(true);
        AL1S_SkillPannelE.SetActive(false);
        AL1S_SkillPannelX.SetActive(false);
    }
    public void OnClick_AL1SSkillButtonE()
    {
        AL1S_Description.SetActive(false);
        AL1S_SkillPannelC.SetActive(false);
        AL1S_SkillPannelQ.SetActive(false);
        AL1S_SkillPannelE.SetActive(true);
        AL1S_SkillPannelX.SetActive(false);
    }
    public void OnClick_AL1SSkillButtonX()
    {
        AL1S_Description.SetActive(false);
        AL1S_SkillPannelC.SetActive(false);
        AL1S_SkillPannelQ.SetActive(false);
        AL1S_SkillPannelE.SetActive(false);
        AL1S_SkillPannelX.SetActive(true);
    }

    IEnumerator MoveToPosition(GameObject target, string arrow)
    {
        Vector3 startPos = target.transform.position;
        Vector3 endPos = startPos;

        float duration = 0.25f;
        float elapsed = 0f;

        if (arrow == "left")
        {
            endPos.x = startPos.x - 50f; // 왼쪽으로 이동
        }
        else if (arrow == "right")
        {
            endPos.x = startPos.x + 50f; // 오른쪽으로 이동
        }
        else if (arrow == "reset")
        {
            // 즉시 원위치 복귀 (부드럽게 하고 싶다면 아래 코드와 동일하게 처리 가능)
            if (target == AL1S_Background)
                target.transform.position = AL1S_OriginalPos;
            else if (target == Shiroko_Background)
                target.transform.position = Shiroko_OriginalPos;
            yield break;
        }

        // 부드럽게 이동 (Ease In-Out)
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            t = t * t * (3f - 2f * t);
            target.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        target.transform.position = endPos;
    }
}
