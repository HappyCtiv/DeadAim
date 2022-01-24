using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject TheEnemy;
    public float XPos;
    public float YPos;
    public int enemyCount;

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while (enemyCount<10)
        {
            XPos = Random.Range(-6.0f,6.0f);
            YPos = Random.Range(-2.0f, 4.0f);
            Instantiate(TheEnemy, new Vector2(XPos,YPos), Quaternion.identity);
            yield return new WaitForSeconds(0.9f);
            enemyCount+=1;
        }
    }
}
