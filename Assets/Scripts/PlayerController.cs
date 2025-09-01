using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3;
    public int PlayerHp = 100;

    void Update()
    {
        
        float h = Input.GetAxisRaw("Horizontal"); // A,D  
        float v = Input.GetAxisRaw("Vertical");   // W,S 

        Vector3 move = new Vector3(h, v, 0);

     
        transform.position += move * moveSpeed * Time.deltaTime;
    }
    private void OnCollisionStay2D(Collision2D coll)
    {
        EnemyController enemy = coll.gameObject.GetComponent<EnemyController>();
        if (enemy != null)
        {
            PlayerHp -= 1;
            Debug.Log("being hit by enemy");
        }
        if(PlayerHp <= 0)
        {
            Debug.Log("Player Dead");
            Destroy(this.gameObject);
        }

    }

}
