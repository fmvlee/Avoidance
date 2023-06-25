using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField]
    private float immuneTime = 3f;
    [SerializeField]
    private bool isImmune = false;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy" && !isImmune && GameManager.Instance.currentState == GameManager.GameState.Playing)
        {
            Debug.Log("Collided with Enemy: " + collision.gameObject.name);
            GameManager.Instance.lifeManager.RemoveLife();
            StartImmunity();
        }
    }

    private void StartImmunity()
    {
        isImmune = true;
        animator.SetBool("Immune", true);
        Debug.Log("Immunity Started");
        StartCoroutine(EndImmunity());
    }

    IEnumerator EndImmunity()
    {
        yield return new WaitForSeconds(immuneTime);
        isImmune = false;
        animator.SetBool("Immune", false);
        Debug.Log("Immunity Finished");
    }
}
