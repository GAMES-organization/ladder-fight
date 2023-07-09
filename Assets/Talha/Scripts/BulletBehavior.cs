using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
//using MoreMountains.NiceVibrations;
using UnityEngine;

using UnityEngine.AI;
using System.Threading.Tasks;
public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private ParticleSystem bulletImpact;
    [SerializeField] Vector3 offset;
    public Material deadZombieMat;
    public Color hitColor;
    public float bulletDamage;
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.position = PlayerManager.instance.transform.position+offset;
        ShootBullet();
    }

    private void Update()
    {
        //transform.rotation = MyLevelManager.instance.playerBehavior.transform.rotation;
        transform.rotation = PlayerManager.instance.transform.rotation;
    }

    void ShootBullet()
    {
        DOTween.Kill(transform);
        //Vector3 endPosition = transform.position - transform.forward * 100;

        Vector3 endPosition = transform.position + PlayerManager.instance.transform.forward * 100;

        transform.DOMove(endPosition, 2).SetEase(Ease.Linear).OnComplete(() =>
        {
            //Destroy(gameObject);
            //GetComponent<MeshRenderer>().material.color = Color.red;
            ObjectPooler.Instance.GetObjectBackInPool(this.gameObject);

        });
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyController>())
        {
            DestroyEnemy(other.gameObject.GetComponent<EnemyController>());
        }
    }*/

    /*public void DestroyEnemy(EnemyController zombie)
    {
        //bulletImpact.Play();
        //zombie.enemyHealth--;
        zombie.enemyHealth = zombie.enemyHealth - bulletDamage;
        //zombie.bloodParticles.Play();

        if (*//*!zombie.isDead && *//*zombie.enemyHealth > 0)
        {
            zombie.zombieSkin.material.DOColor(hitColor , 0.15f).OnComplete(() =>
            {
                if (zombie.enemyHealth > 0)
                {
                    zombie.zombieSkin.material.DOColor(Color.white, 0.15f);
                }
            });
        }
        if (zombie.enemyHealth<=0)
        {
            Vector3 endPosition = zombie.transform.position - zombie.transform.forward * 3;
            zombie.zombieSkin.material = deadZombieMat;
            //zombie.transform.DOJump(new Vector3(zombie.transform.position.x,zombie.transform.position.y-0.5f,zombie.transform.position.z-3),1,1,1);
            zombie.transform.DOJump(endPosition,1,1,1).OnComplete(() =>
            {
                zombie.transform.parent = DeadZombiesCollector.instance.transform;
            });
            //zombie.ZombieSound();
            *//*if(SoundManager.instance.canPlayHaptic) 
                MMVibrationManager.Haptic(HapticTypes.RigidImpact);*//*
            zombie.zombieManager.zombieCount--;
            if (zombie.zombieManager.zombieCount<=0 && MyLevelManager.instance.playerBehavior.pathEnded)
            {
                MyLevelManager.instance.playerBehavior.Win();
            }

            zombie.isDead = true;
            zombie.GetComponent<Collider>().enabled = false;
            //other.GetComponent<NavMeshAgent>().isStopped = true;
            zombie.enemyAgent.enabled = false;
            zombie.zombieAnimator.SetBool("Dead",true);
            //StartCoroutine(DisableZombie(zombie.gameObject));
            //Task.Delay(2000);
            //zombie.gameObject.SetActive(false);
            //Destroy(zombie.gameObject,4f);

        }
       
        *//*yield return new WaitForSeconds(2);
        zombie.gameObject.SetActive(false);*//*
        gameObject.SetActive(false);
        ObjectPooler.Instance.GetObjectBackInPool(gameObject);
    }*/

    IEnumerator DisableZombie(GameObject zombie)
    {
        yield return new WaitForSecondsRealtime(2);
        //zombie.transform.parent = DeadZombiesCollector.instance.transform;

    }
    /*public IEnumerator DestroyZombie(GameObject zombie)
    {
        yield return new WaitForSeconds(1.5f);
        zombie.SetActive(false);
        Destroy(zombie,1.5f);
    }*/
}
