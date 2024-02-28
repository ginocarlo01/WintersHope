using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    private const string PositionKey = "SavedPosition";

    [SerializeField]
    Room[] rooms;

    [SerializeField]
    GameObject player;

    // Mant�m o objeto persistente entre as cenas
    private void Awake()
    {
        rooms = FindObjectsOfType<Room>();

        if (PlayerPrefs.HasKey(PositionKey))
        {
            Vector3 savedPosition = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString(PositionKey));
            transform.position = savedPosition;
            //Debug.Log(savedPosition);
            player.transform.position = savedPosition;
        }

        



    }

    // Salva a posi��o antes de mudar de cena
    private void OnDisable()
    {
        /* Salva a posi��o no PlayerPrefs
        foreach (Room room in rooms)
        {
            if (room.active)
            {
                Debug.Log("There was an active room!");
                PlayerPrefs.SetString(PositionKey, JsonUtility.ToJson(room.SpawnPoint.position));
                PlayerPrefs.Save();
            }
        }
        */
        Room.SavePlayerPosAction -= SavePlayerPosition;
    }

    private void SavePlayerPosition(Vector3 position_)
    {
        PlayerPrefs.SetString(PositionKey, JsonUtility.ToJson(position_));
        PlayerPrefs.Save();
    }

    // Carrega a posi��o ao iniciar a cena
    private void OnEnable()
    {
        // Verifica se h� uma posi��o salva
        Room.SavePlayerPosAction += SavePlayerPosition;
    }

    
}
