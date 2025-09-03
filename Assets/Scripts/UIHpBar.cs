using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class UIHpBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public Text txt;
    public GameObject follow;

    public void SetVal(int hp, int hpMax)
    {
        slider.value = hp * 1.0f / hpMax;
        txt.text = hp + "/" + hpMax;
    }

    public void SetFollow(GameObject follow)
    {
        this.follow = follow;
    }

    public void DoFollow()
    {
        if (follow == null) return;

        // 默认跟随对象的中心
        Vector3 targetPos = follow.transform.position;

        // 优先用 Collider 算底部
        Collider col = follow.GetComponent<Collider>();
        if (col != null)
        {
            targetPos.y = col.bounds.min.y;  // 底部 Y
        }
        else
        {
            // 如果没有 Collider，就尝试用 Renderer
            Renderer renderer = follow.GetComponent<Renderer>();
            if (renderer != null)
            {
                targetPos.y = renderer.bounds.min.y;
            }
        }

        // 设置血条位置（世界坐标）
        transform.position = targetPos;
    }

}
