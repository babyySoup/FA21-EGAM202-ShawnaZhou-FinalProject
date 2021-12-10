using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanKnife : MonoBehaviour
{
    public Transform knifePrefab;
    [Range(1, 100)]

    public int knifeAmt;
    public float KnifeRadius;
    public float DestroyTimer;

    //make a List of the knives 
    List<Transform> knives = new List<Transform>();


    void Start()
    {
        SpawnKnives();
    }

    // Update is called once per frame
    void Update()
    {
        if (DestroyTimer > 0)
        {
            DestroyTimer -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //spwan knife with loop 
    private void SpawnKnives()
    {
        if (knifeAmt > knives.Count)
        {
            for (int i = knives.Count; i < knifeAmt; i++)
            {
                var myKnives = Instantiate(knifePrefab, new Vector3(0, 10, 0), Quaternion.identity);
                myKnives.SetParent(transform);
                knives.Add(myKnives);
            }
        }

        Quaternion quaternion = Quaternion.AngleAxis(360f / (float)(knifeAmt), transform.up);
        Vector3 shootPos = transform.forward * KnifeRadius;
        for(int index = 0; index < knifeAmt; ++index)
        {
            knives[index].position = transform.position + shootPos;
            knives[index].LookAt(transform);
            //new knife position 
            shootPos = quaternion * shootPos;
        }
    }


}
