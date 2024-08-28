using UnityEngine;
using System.Collections;
public class PlayerCollision : MonoBehaviour
{

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            HealthManager.health--;
            if(HealthManager.health <=0){
                PlayerManager.isGameOver = true;
                AudioManager.instance.Play("GameOver");
                gameObject.SetActive(false);
            }else{
                StartCoroutine(GetHurt());
            }
        }
    }
    IEnumerator GetHurt(){ 
        Physics2D.IgnoreLayerCollision(6,8);
        GetComponent<Animator>().SetLayerWeight(1, 1);
        yield return new WaitForSeconds(3);
        GetComponent<Animator>().SetLayerWeight(1, 0);
        Physics2D.IgnoreLayerCollision(6,8, false);
    }
    */


    private bool isTouchingZombie = false;
    private Coroutine zombieDamageCoroutine;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {

            Debug.Log("Ax");
            HealthManager.health--;
            if (HealthManager.health <= 0)
            {
                PlayerManager.isGameOver = true;
                AudioManager.instance.Play("GameOver");
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(GetHurt());
            }
        }
        else if (collision.transform.tag == "Zombie")
        {
            Debug.Log("A0");
            if (!isTouchingZombie)
            {
                isTouchingZombie = true;
                zombieDamageCoroutine = StartCoroutine(ZombieDamage());

                Debug.Log("A");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Zombie")
        {
            if (isTouchingZombie)
            {
                Debug.Log("B");
                isTouchingZombie = false;
                if (zombieDamageCoroutine != null)
                {
                    StopCoroutine(zombieDamageCoroutine);
                }
            }
        }
    }

    IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(6, 8);
        GetComponent<Animator>().SetLayerWeight(1, 1);
        yield return new WaitForSeconds(3);
        GetComponent<Animator>().SetLayerWeight(1, 0);
        Physics2D.IgnoreLayerCollision(6, 8, false);
    }

    IEnumerator ZombieDamage()
    {
        while (isTouchingZombie)
        {

            Debug.Log("C");
            HealthManager.health--;
            if (HealthManager.health <= 0)
            {
                PlayerManager.isGameOver = true;
                AudioManager.instance.Play("GameOver");
                gameObject.SetActive(false);
                yield break; // Exit the coroutine if the game is over
            }
            yield return new WaitForSeconds(5);
        }
    }
}
