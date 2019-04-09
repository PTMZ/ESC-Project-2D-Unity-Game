using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineAttack : MonoBehaviour
{

    public bool isActivated = false;
    public GameObject player;
    private PlayerAvatar pAvatar;

    public GameObject targetPrefab;
    public GameObject laserHitPointPrefab;
    public float cooldown;
    public float timeout;
    public float randRadius;
    public static float damage = 10;
    private float cooldownTimeStamp;

    private Vector3[] targets = new Vector3[3];
    public LineRenderer[] lineRenderers;
    private Vector3 offsetY;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        cooldownTimeStamp = Time.time;
        Debug.Log(player.transform.position);
        offsetY = new Vector3(0, 0.5f, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isActivated){
            return;
        }
        if(Time.time > cooldownTimeStamp){
            Debug.Log("START SHOOT");
            cooldownTimeStamp = Time.time + cooldown;
            SpawnTargets();
            DelayLaserAttack();
        }
    }

    void SpawnTargets(){
        float angle = Random.Range(-Mathf.PI, Mathf.PI);
        float dist = Random.Range(0.5f, randRadius);

        float angle2 = Random.Range(-Mathf.PI, Mathf.PI);
        float dist2 = Random.Range(0.5f, randRadius);

        Debug.Log(player.transform.position);
        targets[0] = player.transform.position + offsetY;
        targets[1] = targets[0] + new Vector3(Mathf.Cos(angle) * dist, Mathf.Sin(angle) * dist, 0);
        targets[2] = targets[0] + new Vector3(Mathf.Cos(angle2) * dist2, Mathf.Sin(angle2) * dist2, 0);

        for(int i=0; i<3; i++){
            GameObject t = Instantiate(targetPrefab, targets[i], Quaternion.identity);
            Destroy(t, timeout);
        }
    }

    void DelayLaserAttack(){
        StartCoroutine(WaitAndShoot());
    }

    private IEnumerator WaitAndShoot()
    {
        yield return new WaitForSeconds(timeout);

        for(int i=0; i<3; i++){
            lineRenderers[i].SetPosition(0, transform.position);
            lineRenderers[i].SetPosition(1, targets[i]);
            lineRenderers[i].enabled = true;
            Instantiate(laserHitPointPrefab, targets[i], Quaternion.identity);
        }

        yield return new WaitForSeconds(0.1f);

        for(int i=0; i<3; i++){
            lineRenderers[i].enabled = false;
        }

    }

}
