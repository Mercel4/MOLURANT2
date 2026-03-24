using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;


public class StoreUI : MonoBehaviour
{
    [FoldoutGroup("Main")] public GameObject SkillStorePanel;
    [FoldoutGroup("Main")] public Text skillName;
    [FoldoutGroup("Main")] public Text skillDescription;
    [FoldoutGroup("Main")] public Image skillImageC;
    [FoldoutGroup("Main")] public Image skillImageQ;
    [FoldoutGroup("Main")] public Sprite shirkoSkillC_IMG;
    [FoldoutGroup("Main")] public Sprite shirkoSkillQ_IMG;
    [FoldoutGroup("Main")] public Sprite AL1SSkillC_IMG;
    [FoldoutGroup("Main")] public Sprite AL1SSkillQ_IMG;

    [FoldoutGroup("Shiroko")] public string shirkoSkillC_Name;
    [FoldoutGroup("Shiroko"), TextArea(2, 10)] public string shirkoSkillC_Description;

    [FoldoutGroup("AL1S")] public string AL1SSkillC_Name;
    [FoldoutGroup("AL1S"), TextArea(2, 10)] public string AL1SSkillC_Description;

    public static bool isStoreOpen = false;

    private void Start()
    {
        SkillStorePanel.SetActive(false);
        SetSkillIMG();
    }

    private void Update()
    {
        if (TimeKeeperManager.currentPhase == TimeKeeperManager.Phase.Buy)
        {
            OpenAndCloseStoreUI();
        } 
        else
            OpenAndCloseStoreUI(); // 테스트 용
    }

    private void SetSkillIMG()
    {
        if (STSelectManager.currentCharacter == "Shiroko" || STSelectManager.currentCharacter == null)
        {
            skillImageC.sprite = shirkoSkillC_IMG;
            skillImageQ.sprite = shirkoSkillQ_IMG;

            skillName.text = shirkoSkillC_Name;
            skillDescription.text = shirkoSkillC_Description;
        }
        else if (STSelectManager.currentCharacter == "AL1S")
        {
            skillImageC.sprite = AL1SSkillC_IMG;
            skillImageQ.sprite = AL1SSkillQ_IMG;

            skillName.text = AL1SSkillC_Name;
            skillDescription.text = AL1SSkillC_Description;
        }
    }

    private void OpenAndCloseStoreUI()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            SkillStorePanel.SetActive(!SkillStorePanel.activeSelf);
            isStoreOpen = SkillStorePanel.activeSelf;
            if (isStoreOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}