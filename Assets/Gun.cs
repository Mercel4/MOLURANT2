using UnityEngine;
using System.Collections;
using Photon.Pun;

public class Gun : MonoBehaviourPun
{
    [Header("기본 설정")]
    public Transform firePoint;
    public TrailRenderer bulletTrailPrefab;

    [Header("발사 설정")]
    public float range = 100f;
    public float trailSpeed = 400f;
    public static float fireRate = 0.1f;

    [Header("탄퍼짐 설정")]
    public float normalSpread = 0.01f;      // 평상시 탄퍼짐
    public float runSpread = 0.1f;          // 달릴 때 탄퍼짐

    [Header("데미지 설정")]
    public int damage = 20;  // 인스펙터에서 조절 가능


    public static bool isRunning = false;   // 외부에서 달리는 상태 받음
    private float lastFireTime;
    public static bool isFiring = false;    // 외부에서 발사 상태 받음

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= lastFireTime + fireRate && StoreUI.isStoreOpen == false)
        {
            Shoot();
            lastFireTime = Time.time;
        }
        else
        {
            isFiring = false;
        }
    }

    void Shoot()
    {
        isFiring = true;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Vector3 start = firePoint.position;
        Vector3 end;

        float spread = isRunning ? runSpread : normalSpread;

        Vector3 spreadDir = ray.direction;
        spreadDir += new Vector3(
            Random.Range(-spread, spread),
            Random.Range(-spread, spread),
            0
        );
        spreadDir.Normalize();

        if (Physics.Raycast(ray.origin, spreadDir, out hit, range))
        {
            end = hit.point;

            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("맞은 대상: " + hit.collider.name);

                // PhotonView ID 사용
                PhotonView targetPV = hit.collider.GetComponent<PhotonView>();
                PhotonView managerPV = GameObject.Find("HP_Manager").GetComponent<PhotonView>();
                managerPV.RPC("ApplyDamage", RpcTarget.All, targetPV.ViewID, damage, photonView.Owner.NickName);
            }

            Debug.DrawLine(ray.origin, hit.point, Color.green, 1f);
        }
        else
        {
            end = ray.origin + spreadDir * range;
            Debug.DrawLine(ray.origin, end, Color.red, 1f);
        }

        StartCoroutine(PlayTrail(start, end));
    }



    IEnumerator PlayTrail(Vector3 start, Vector3 end)
    {
        TrailRenderer trail = Instantiate(bulletTrailPrefab, start, Quaternion.identity);

        float distance = Vector3.Distance(start, end);
        float duration = distance / trailSpeed;

        float time = 0f;
        while (time < duration)
        {
            trail.transform.position = Vector3.Lerp(start, end, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        trail.transform.position = end;
        yield return new WaitForSeconds(trail.time);
        Destroy(trail.gameObject);
    }
}
