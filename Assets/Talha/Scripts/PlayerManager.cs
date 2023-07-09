using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [SerializeField] float shootTime;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootTime += Time.deltaTime;
        if (shootTime > 0.15f)
        {
            shootTime = 0;
            var bullet = ObjectPooler.Instance.FetchPooledObject("Bullet") as GameObject;
            bullet.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            bullet.SetActive(true);
        }
    }
}
