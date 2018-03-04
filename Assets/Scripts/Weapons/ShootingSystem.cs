using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour {

    public float weaponRange = 100f;
    public Camera FPSCamera;
    private AdvancedEnemyAI enemy;
    private int minDmg = 15;
    private int maxDmg = 30;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = FPSCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hitInfo;

        Debug.DrawRay(ray.origin, ray.direction * weaponRange, Color.blue);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(ray, out hitInfo, weaponRange))
            {
                if(hitInfo.collider.tag == "Enemy") {
                    enemy = hitInfo.collider.GetComponent<AdvancedEnemyAI>();
                    enemy.TakeDamage(Damage());
                }

            }
        }
	}
    private int Damage()
    {
        return Random.Range(minDmg, maxDmg);
    }
}
