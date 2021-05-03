using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> ItemPool;
    void Start()
    {
        Setting();
    }
    // Update is called once per frame
    void Update()
    {
       
    }


    public void MoveItem(GameObject Item)
    {
        Item.transform.position = this.transform.position;
    }


    public void AddItem(GameObject Item) 
    {
        ItemPool.Add(Item);
        Setting();
    }
    public void DeleteItem(GameObject Item)
    {
        ItemPool.Remove(Item);
        Setting();
    }

    public void UseItem(GameObject Item) 
    {
        //아이템 사용시 pool에서 삭제
    }
    public void Setting() 
    {
        for (int j = 0; j < ItemPool.Count; j++)
        {
            Vector3 temp = new Vector3(1.021f * j, 0, 0);
            ItemPool[j].gameObject.transform.position= this.transform.position + temp;
           


        }
    }



        public void ChangeType() { }

    public void Upgrade() { }
}
