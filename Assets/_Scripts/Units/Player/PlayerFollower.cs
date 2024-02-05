using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    GameObject player;

    [SerializeField]
    private float speed;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        LookToPlayer();

        Move();
    }

    public void LookToPlayer()
    {
        Vector3 playerPos = player.transform.position;
        playerPos.z = 0f;

        Vector3 lookDir = playerPos - transform.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Move()
    {
        transform.Translate(speed * new Vector3(1, 0, 0) * Time.deltaTime);
    }
}
