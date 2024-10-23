using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public float interval;
    public GameObject enemy;
    public Camera cam;

    public Transform Player;

    void Start()
    {
        StartCoroutine(spawner(interval, enemy));

    }

    public IEnumerator spawner(float interval, GameObject enemy)
    {
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(cam.transform.position.x - 10, cam.transform.position.x + 10), Random.Range(cam.transform.position.y + 10, cam.transform.position.y - 10), 0), Quaternion.identity);
        newEnemy.GetComponent<AIDestinationSetter>().target = Player;
        yield return new WaitForSeconds(interval); ;
        StartCoroutine(spawner(interval, enemy));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
