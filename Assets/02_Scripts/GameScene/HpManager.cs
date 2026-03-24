using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class HpManager : MonoBehaviourPun
{
    [Header("기본 HP")]
    public float hp = 100f;

    [Header("UI 연결")]
    public Text hpText; // 각 플레이어 UI에 연결

    private void Start()
    {
        UpdateHpUI();
    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0) hp = 0;
        Debug.Log($"{photonView.Owner.NickName} took {damage} damage. Current HP: {hp}");

        UpdateHpUI();

        if (hp <= 0)
        {
            Debug.Log($"{photonView.Owner.NickName} is dead!");
            // 죽었을 때 추가 로직 가능 (리스폰 등)
        }
    }

    private void UpdateHpUI()
    {
        if (hpText != null)
            hpText.text = $"HP: {hp}";
    }

    // 공격 함수 (원할 경우 다른 스크립트에서 호출 가능)
    public void Attack(GameObject target, int damage)
    {
        if (target.TryGetComponent(out HpManager targetHP))
        {
            // 맞은 사람 클라이언트에서만 데미지 적용
            targetHP.photonView.RPC("TakeDamage", targetHP.photonView.Owner, damage);
        }
    }
}
