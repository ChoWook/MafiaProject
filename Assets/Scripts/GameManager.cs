using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.Pun.MonoBehaviourPun
{
    [SerializeField] private GameObject playerPrefab = null;
    [SerializeField] private GameObject spwanPoint;

    private void Start()
    {
        
        if (playerPrefab != null)
        {
            // 포톤네트워크에 있는 인스턴시에이트, 방에 있는 모든 유저에게 프리팹 만들어줌
            GameObject go =
            //     PhotonNetwork.Instantiate(
            //       "Prefabs\\PlayerPrefab", Resources 경로에 접근하는것이기 때문에
            //        playerPrefab.name, new Vector3(Random.Range(-10.0f, 10.0f), 0.0f, Random.Range(-10.0f, 10.0f)), Quaternion.identity, 0);
            PhotonNetwork.Instantiate(playerPrefab.name, spwanPoint.transform.position, spwanPoint.transform.rotation);
            // 플레이어 색상 정함, 동기화 안되어있어서 자신의 색만 바뀜
            go.GetComponent<PlayerControl>().SetMaterial(PhotonNetwork.PlayerList.Length);
            go.GetComponent<PlayerControl>().NicknameDisplay(PhotonNetwork.NickName);

        }
    }
}
