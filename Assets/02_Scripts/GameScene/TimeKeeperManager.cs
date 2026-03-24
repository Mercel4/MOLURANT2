using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;
using UnityEngine.UI;

public class TimeKeeperManager : MonoBehaviourPunCallbacks
{
    // static으로 선언해서 다른 스크립트에서 접근 가능
    public static double startTime; 
    public static double buyPhaseDuration = 20.0; 
    public static double battlePhaseDuration = 90.0; 
    public static double fadeDuration = 3.0; 
    public static Phase currentPhase = Phase.Buy;

    public Text timeDisplayText;

    public enum Phase { Buy, Battle, Fade, End } // 게임의 현재 페이즈를 나타내는 열거형

    private void Start()
    {
        // MasterClient는 방 시작 시간과 지속 시간 설정
        if (PhotonNetwork.IsMasterClient)
        {
            startTime = PhotonNetwork.Time;
            Hashtable timeProps = new Hashtable // 딕셔너리 형태로 커스텀 프로퍼티 설정
            {
                { "StartTime", startTime },
                { "BuyPhaseDuration", buyPhaseDuration },
                { "BattlePhaseDuration", battlePhaseDuration },
                { "FadeDuration", fadeDuration }
            };
            PhotonNetwork.CurrentRoom.SetCustomProperties(timeProps); // 현재 방에 데이터를 저장
        }
        else
        {
            if (PhotonNetwork.CurrentRoom != null &&
                PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("StartTime"))
            {
                startTime = (double)PhotonNetwork.CurrentRoom.CustomProperties["StartTime"];
                buyPhaseDuration = (double)PhotonNetwork.CurrentRoom.CustomProperties["BuyPhaseDuration"];
                battlePhaseDuration = (double)PhotonNetwork.CurrentRoom.CustomProperties["BattlePhaseDuration"];
                fadeDuration = (double)PhotonNetwork.CurrentRoom.CustomProperties["FadeDuration"];
            }
            else
            {
                Debug.LogWarning("CustomProperties가 아직 준비되지 않았습니다!");
                startTime = PhotonNetwork.Time; // 임시 초기화
            }
        }
    }

    private void Update()
    {
        if (startTime == 0) return;

        double elapsedTime = PhotonNetwork.Time - startTime;
        double remainingTime;

        Phase newPhase = GetCurrentPhase(elapsedTime, out remainingTime);

        // 페이즈가 바뀌면 static 변수 갱신
        if (newPhase != currentPhase)
        {
            currentPhase = newPhase;
            Debug.Log("현재 페이즈: " + currentPhase.ToString());
        }

        // 남은 시간을 00:00 형식으로 표시
        if (timeDisplayText != null)
        {
            int minutes = Mathf.FloorToInt((float)remainingTime / 60f);
            int seconds = Mathf.FloorToInt((float)remainingTime % 60f);
            timeDisplayText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private Phase GetCurrentPhase(double elapsed, out double remainingTime)
    // elapsed: 경과 시간
    // remainingTime: 현재 페이즈에서 남은 시간
    {
        if (elapsed < buyPhaseDuration) // 경과 시간이 구매 페이즈 지속 시간보다 짧으면
        {
            remainingTime = buyPhaseDuration - elapsed;
            return Phase.Buy;
        }
        else if (elapsed < buyPhaseDuration + battlePhaseDuration) // 경과 시간이 구매 + 전투 페이즈 지속 시간보다 짧으면
        {
            remainingTime = buyPhaseDuration + battlePhaseDuration - elapsed;
            return Phase.Battle;
        }
        else if (elapsed < buyPhaseDuration + battlePhaseDuration + fadeDuration) // 경과 시간이 구매 + 전투 + 페이드 페이즈 지속 시간보다 짧으면
        {
            remainingTime = buyPhaseDuration + battlePhaseDuration + fadeDuration - elapsed;
            return Phase.Fade;
        }
        else
        {
            remainingTime = 0;
            return Phase.End;
        }
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    // CustomProperties가 바뀌면 자동으로 호출
    {
        if (propertiesThatChanged.ContainsKey("StartTime"))
            startTime = (double)propertiesThatChanged["StartTime"];
    }
}
