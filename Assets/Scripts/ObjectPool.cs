using UnityEngine;
using System.Collections.Generic;
 
[System.Serializable]
public class PooledObject
{
    public GameObject obj = null;
    public int amount = 0;
}
 
public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance = null;
    public PooledObject[] objects = null;
    private List<GameObject>[] _pool = null;
 
    private void Awake()
    {
        Instance = this;
    }
    
    private void Start()
    {
        _pool = new List<GameObject>[objects.Length];
 
        for (var count = 0; count < objects.Length; count++)
        {
            _pool[count] = new List<GameObject>();
            for (var num = 0; num < objects[count].amount; num++)
            {
                var temp = Instantiate(objects[count].obj, new Vector3(0.0f, 1000.0f, 0.0f), Quaternion.identity);
                temp.SetActive(false);
                temp.transform.parent = transform;
                _pool[count].Add(temp);
            }
        }
    }
    
    public GameObject Activate(int id, Vector3 position, Quaternion rotation)
    {
        for (var count = 0; count < _pool[id].Count; count++)
        {
            if (!_pool[id][count].activeSelf)
            {
                var currObj = _pool[id][count];
                var currTrans = currObj.transform;
 
                currObj.SetActive(true);
                currTrans.position = position;
                currTrans.rotation = rotation;
                return currObj;
            }
        }
        var newObj = Instantiate(objects[id].obj) as GameObject;
        var newTrans = newObj.transform;
        newTrans.position = position;
        newTrans.rotation = rotation;
        newTrans.parent = transform;
        _pool[id].Add(newObj);
        return newObj;
    }
     
    public static void Deactivate(GameObject obj)
    {
        obj.SetActive(false);
    }
}