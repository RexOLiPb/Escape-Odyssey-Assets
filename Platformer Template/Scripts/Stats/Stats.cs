using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Platformer
{
    public class Stats : MonoBehaviour
    {
        //Death event call when player or somebody hp <= 0 
        public delegate void DeathEvent();
        public DeathEvent OnDeath;

        public StatsData statsData = new StatsData();

/*        private void Update()
        {
            if (statsData.HP > 0)
            {
                statsData.HP -= 0.1f * Time.deltaTime;

                if (statsData.HP <= 0)
                {
                    // 触发角色死亡或其他相关事件
                    Death();
                    statsData.HP = 0; // 确保HP不会变成负数
                }
            }
        }*/

        //Damage method
        public void GetDamage(float damage)
        {
            statsData.HP -= damage; //Subtract damage from hp

            if (gameObject.tag == "Player") //if player
                UIManager.Instance.UpdateHP(statsData.HP); //Update UI

            if (statsData.HP <= 0) 
            {
                Death();
            }
        }

        //Death method
        public void Death()
        {
            OnDeath();
        }

    }

    [Serializable]
    public struct StatsData
    {
        public float HP;
    }
}