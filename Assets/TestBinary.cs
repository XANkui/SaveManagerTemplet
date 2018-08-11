using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveManager_XAN;
using System;

[Serializable]
public class BinaryClass
{
    
    public string Sex;
    public int Height;

}

public class TestBinary : MonoBehaviour {

	// Use this for initialization
	void Start () {

        BinaryClass BinaryData = new BinaryClass();
        BinaryData.Height = 3;
        BinaryData.Sex = "男";

        if (SaveManager.SaveDataByBinary<BinaryClass>(BinaryData, "BinaryTest"))
        {
            Debug.Log("保存成功");
        }
        else
        {
            Debug.Log("保存失败");
        }

        BinaryData = SaveManager.LoadDataFromBinary<BinaryClass>("BinaryTest");
        Debug.Log("Sex:"+BinaryData.Sex);
        Debug.Log("Height:"+BinaryData.Height.ToString());

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
