using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

public class localUIManager : MonoBehaviour
{
    [FoldoutGroup("Character")] public Image CharacterImageAlly;
    [FoldoutGroup("Character")] public Image CharacterImageEnemy;
    [FoldoutGroup("Character")] public Text CharacterNameAlly;
    [FoldoutGroup("Character")] public Text CharacterNameEnemy;
    [FoldoutGroup("Skill Store UI")] public Image SkillStoreC;
    [FoldoutGroup("Skill Store UI")] public Image SkillStoreQ;
    [FoldoutGroup("Main Skill UI")] public Image SkillC;
    [FoldoutGroup("Main Skill UI")] public Image SkillQ;
    [FoldoutGroup("Main Skill UI")] public Image SkillE;
    [FoldoutGroup("Main Skill UI")] public Image SkillX;
    [FoldoutGroup("Shiroko")] public Sprite Shiroko;
    [FoldoutGroup("Shiroko")] public Sprite Shiroko_SkillC;
    [FoldoutGroup("Shiroko")] public Sprite Shiroko_SkillQ;
    [FoldoutGroup("Shiroko")] public Sprite Shiroko_SkillE;
    [FoldoutGroup("Shiroko")] public Sprite Shiroko_SkillX;
    [FoldoutGroup("AL1S")] public Sprite AL1S;
    [FoldoutGroup("AL1S")] public Sprite AL1S_SkillC;
    [FoldoutGroup("AL1S")] public Sprite AL1S_SkillQ;
    [FoldoutGroup("AL1S")] public Sprite AL1S_SkillE;
    [FoldoutGroup("AL1S")] public Sprite AL1S_SkillX;

    private void Start()
    {
        UpdateCharacterUI();
        UpdateUpperCharacterUI();
    }

    private void UpdateCharacterUI()
    {
        string characterName = STSelectManager.currentCharacter;

        if (characterName == "Shiroko" || characterName == null)
        {
            SkillStoreC.sprite = Shiroko_SkillC;
            SkillStoreQ.sprite = Shiroko_SkillQ;
            SkillC.sprite = Shiroko_SkillC;
            SkillQ.sprite = Shiroko_SkillQ;
            SkillE.sprite = Shiroko_SkillE;
            SkillX.sprite = Shiroko_SkillX;
        }
        else if (characterName == "AL1S")
        {
            SkillStoreC.sprite = AL1S_SkillC;
            SkillStoreQ.sprite = AL1S_SkillQ;
            SkillC.sprite = AL1S_SkillC;
            SkillQ.sprite = AL1S_SkillQ;
            SkillE.sprite = AL1S_SkillE;
            SkillX.sprite = AL1S_SkillX;
        }
    }

    private void UpdateUpperCharacterUI()
    {
        CharacterNameAlly.text = MainGameManager.allyCharacter;
        CharacterNameEnemy.text = MainGameManager.enemyCharacter;

        // 아군 캐릭터 이미지 설정
        if (MainGameManager.allyCharacter == "Shiroko" || MainGameManager.allyCharacter == null)
        {
            CharacterImageAlly.sprite = Shiroko;
        }
        else if (MainGameManager.allyCharacter == "AL1S")
        {
            CharacterImageAlly.sprite = AL1S;
        }

        // 적군 캐릭터 이미지 설정
        if (MainGameManager.enemyCharacter == "Shiroko" )
        {
            CharacterImageEnemy.sprite = Shiroko;
        }
        else if (MainGameManager.enemyCharacter == "AL1S" || MainGameManager.enemyCharacter == null)
        {
            CharacterImageEnemy.sprite = AL1S;
        }
    }
}
