using System.Collections.Generic;
using UnityEngine;

public class OrderHandler : MonoBehaviour
{
    public GameObject currentOrder;
    [SerializeField]
    private List<GameObject> orderList;
    [SerializeField]
    private GameObject deliveryPoint;
    void Start()
    {
        setTarget();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == currentOrder){
            setTarget();
        }
    }
    void setTarget(){
        if (currentOrder == null || currentOrder == deliveryPoint){
            if (orderList.Count > 0){
                currentOrder = orderList[Random.Range(0, orderList.Count)];
            }
        } else {
            currentOrder = deliveryPoint;
        }
    }

    public string GetOrder()
    {
        if (currentOrder != null)
        {
            return currentOrder.name;
        }
        return "No current order";
    }
}
