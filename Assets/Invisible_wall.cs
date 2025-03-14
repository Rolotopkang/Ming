using UnityEngine;

public class Invisible_wall : MonoBehaviour
{

        void Start()
        {
            // 获取 MeshRenderer 组件并禁用它
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }

            // 确保 Collider 组件仍然启用
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
            }
        }
}



