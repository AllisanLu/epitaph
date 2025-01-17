using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnerController : MonoBehaviour
{
    public GameObject lich;
    private Animator animator;
    // Start is called before the first frame update
    private bool spawning = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && !spawning) {
            StartCoroutine(playAnimationThenSpawn());
            spawning = true;
        }
    }

    IEnumerator playAnimationThenSpawn() {
        animator.SetBool("spawn", true);
        yield return new WaitForSeconds(4);
        //please don't question the math, i didn't have time to figure out why it wouldn't spawn on top T u T
        GameObject boss = Instantiate(lich, this.transform.position + new Vector3(2f, 4.787f, 0f), Quaternion.identity);
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sortingOrder = 0;
        yield return new WaitForSeconds(1);
        LichController lc = boss.GetComponentInChildren<LichController>();
        lc.ActivateCrystals();
        lc.start = true;
        Destroy(this.gameObject);
    }
}
