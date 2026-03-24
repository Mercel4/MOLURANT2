using UnityEngine;
using Photon.Pun;
using Sirenix.OdinInspector;

public class useSkill : MonoBehaviourPunCallbacks
{
    [FoldoutGroup("Shiroko Skill OBJ")] public GameObject shiroko_Gun;
    [FoldoutGroup("Shiroko Skill OBJ")] public GameObject shirokoC_Skill_OBJ;

    private void Start()
    {
        shirokoC_Skill_OBJ.SetActive(false);
        shiroko_Gun.SetActive(true);
    }

    private void Update()
    {
        if (STSelectManager.currentCharacter == "Shiroko" || STSelectManager.currentCharacter == null)
        {
            if (BuySkills.isBuydC && Input.GetKeyDown(KeyCode.C))
            {
                // photonView.RPC("UseSkillC_Shikoro", RpcTarget.All);
                Debug.Log("Shiroko C Skill Used");
                shirokoC_Skill_OBJ.SetActive(true);
                BuySkills.isBuydC = false; // 스킬 사용 후 구매 상태 초기화
                shiroko_Gun.SetActive(false);
            }
        }
    }

    // [PunRPC]
    // private void UseSkillC_Shikoro()
    // {
    //     // Shiroko의 C 스킬 사용 로직
    //     Debug.Log("Shiroko C Skill Used");
    //     shirokoC_Skill_OBJ.SetActive(true);
    // }
}