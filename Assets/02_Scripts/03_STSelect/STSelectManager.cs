using UnityEngine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using Photon.Pun;
using Photon.Realtime;

public class STSelectManager : MonoBehaviour
{
    [FoldoutGroup("Pannels")] public GameObject ShirokoPannel;
    [FoldoutGroup("Pannels")] public GameObject AL1SPannel;

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

    public static string currentCharacter;

    private void Awake()
    {
        // 모두 비활성화
        ShirokoPannel.SetActive(false);
        AL1SPannel.SetActive(false);
        currentCharacter = "";
    }

    public void OnClickShiroko()
    {
        currentCharacter = "Shiroko";

        ShirokoPannel.SetActive(true);
        shirkoDescription.SetActive(true);
        shirkoSkillPannelC.SetActive(false);
        shirkoSkillPannelQ.SetActive(false);
        shirkoSkillPannelE.SetActive(false);
        shirkoSkillPannelX.SetActive(false);

        AL1SPannel.SetActive(false);
        AL1S_Description.SetActive(false);
        AL1S_SkillPannelC.SetActive(false);
        AL1S_SkillPannelQ.SetActive(false);
        AL1S_SkillPannelE.SetActive(false);
        AL1S_SkillPannelX.SetActive(false);
    }

    public void OnClickAL1S()
    {
        currentCharacter = "AL1S";

        AL1SPannel.SetActive(true);
        AL1S_Description.SetActive(true);
        AL1S_SkillPannelC.SetActive(false);
        AL1S_SkillPannelQ.SetActive(false);
        AL1S_SkillPannelE.SetActive(false);
        AL1S_SkillPannelX.SetActive(false);

        ShirokoPannel.SetActive(false);
        shirkoDescription.SetActive(false);
        shirkoSkillPannelC.SetActive(false);
        shirkoSkillPannelQ.SetActive(false);
        shirkoSkillPannelE.SetActive(false);
        shirkoSkillPannelX.SetActive(false);
    }

    public void OnClickShiroko_C()
    {
        shirkoDescription.SetActive(false);
        shirkoSkillPannelC.SetActive(true);
        shirkoSkillPannelE.SetActive(false);
        shirkoSkillPannelQ.SetActive(false);
        shirkoSkillPannelX.SetActive(false);
    }

    public void OnClickShiroko_Q()
    {
        shirkoDescription.SetActive(false);
        shirkoSkillPannelC.SetActive(false);
        shirkoSkillPannelQ.SetActive(true);
        shirkoSkillPannelE.SetActive(false);
        shirkoSkillPannelX.SetActive(false);
    }

    public void OnClickShiroko_E()
    {
        shirkoDescription.SetActive(false);
        shirkoSkillPannelC.SetActive(false);
        shirkoSkillPannelQ.SetActive(false);
        shirkoSkillPannelE.SetActive(true);
        shirkoSkillPannelX.SetActive(false);
    }

    public void OnClickShiroko_X()
    {
        shirkoDescription.SetActive(false);
        shirkoSkillPannelC.SetActive(false);
        shirkoSkillPannelQ.SetActive(false);
        shirkoSkillPannelE.SetActive(false);
        shirkoSkillPannelX.SetActive(true);
    }

    public void OnClickAL1S_C()
    {
        AL1S_Description.SetActive(false);
        AL1S_SkillPannelC.SetActive(true);
        AL1S_SkillPannelE.SetActive(false);
        AL1S_SkillPannelQ.SetActive(false);
        AL1S_SkillPannelX.SetActive(false);
    }

    public void OnClickAL1S_Q()
    {
        AL1S_Description.SetActive(false);
        AL1S_SkillPannelC.SetActive(false);
        AL1S_SkillPannelQ.SetActive(true);
        AL1S_SkillPannelE.SetActive(false);
        AL1S_SkillPannelX.SetActive(false);
    }

    public void OnClickAL1S_E()
    {
        AL1S_Description.SetActive(false);
        AL1S_SkillPannelC.SetActive(false);
        AL1S_SkillPannelQ.SetActive(false);
        AL1S_SkillPannelE.SetActive(true);
        AL1S_SkillPannelX.SetActive(false);
    }

    public void OnClickAL1S_X()
    {
        AL1S_Description.SetActive(false);
        AL1S_SkillPannelC.SetActive(false);
        AL1S_SkillPannelQ.SetActive(false);
        AL1S_SkillPannelE.SetActive(false);
        AL1S_SkillPannelX.SetActive(true);
    }
}
