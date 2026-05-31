using UnityEngine;

public class ForceGPUInstancing : MonoBehaviour
{
    void Start()
    {
        // Создаем пустой блок свойств. 
        // Само его наличие заставляет URP использовать GPU Instancing вместо SRP Batcher
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        GetComponent<Renderer>().SetPropertyBlock(props);
    }
}