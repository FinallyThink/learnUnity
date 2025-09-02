using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;

   void Start()
    {
        player = GameManager.Instance.player.transform;
    }
    private void LateUpdate()
    {
        if (player == null) return;

        // 锁 z 轴为原来的 z（2D 游戏一般 z = -10）
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
