using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyListScript : MonoBehaviour
{

    public static List<EnemyHealth> enemyList = new List<EnemyHealth>();

    public static List<EnemyHealth> GetEnemyList()
    {
        return enemyList;
    }

}
