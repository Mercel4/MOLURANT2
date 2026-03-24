using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class SkillHoverDesciption : MonoBehaviour, IPointerEnterHandler
{
    public Text skillNameText;
    public Text skillDescriptionText;
    public bool isCommon;
    [FoldoutGroup("Common")] public string commonSkillName;
    [FoldoutGroup("Common"), TextArea(2, 10)] public string commonSkillDescription;
    [FoldoutGroup("Shiroko")] public string skillNameShiroko;
    [FoldoutGroup("Shiroko"), TextArea(2, 10)] public string skillDescriptionShiroko;
    [FoldoutGroup("AL1S")] public string skillNameAL1S;
    [FoldoutGroup("AL1S"), TextArea(2, 10)] public string skillDescriptionAL1S;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isCommon)
        {
            skillNameText.text = commonSkillName;
            skillDescriptionText.text = commonSkillDescription;
            return;
        }

        if (STSelectManager.currentCharacter == "AL1S")
        {
            skillNameText.text = skillNameAL1S;
            skillDescriptionText.text = skillDescriptionAL1S;
            return;
        }
        if (STSelectManager.currentCharacter == "Shiroko" || STSelectManager.currentCharacter == null)
        {
            skillNameText.text = skillNameShiroko;
            skillDescriptionText.text = skillDescriptionShiroko;
            return;
        }
    }
}
