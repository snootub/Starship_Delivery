using UnityEngine;

public class ActiveIngredient : MonoBehaviour
{
    [SerializeField]
    private GameObject OrderSource;
    [SerializeField]
    private Material activeTexture;
    [SerializeField]
    private Material inactiveTexture;
    private MeshRenderer meshRenderer;
    private bool isRequested = false;
    private bool resetTexture = false;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (!resetTexture){
            if (isRequested){
                meshRenderer.material = activeTexture;
            } else {
                meshRenderer.material = inactiveTexture;
            }
            resetTexture = true;
        }
        if (!isRequested && OrderSource.GetComponent<OrderHandler>().currentOrder == gameObject){
            isRequested = true;
            resetTexture = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject == OrderSource) && isRequested){
            isRequested = false;
            resetTexture = false;
        }
    }
}
