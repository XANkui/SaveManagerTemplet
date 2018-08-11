using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveManager_XAN;

public class JsonClass {

    public string name;
    public int Age;

}

public class TestJson : MonoBehaviour {

	// Use this for initialization
	void Start () {
        JsonClass JsonData = new JsonClass();
        JsonData.Age = 3;
        JsonData.name = "XAN";

        if (SaveManager.SaveDataByJson<JsonClass>(JsonData, "JsonTest"))
        {
            Debug.Log("保存成功");
        }
        else {
            Debug.Log("保存失败");
        }

        JsonClass fromJsonData = SaveManager.LoadDataFromJson<JsonClass>("XXX");
        if (fromJsonData == null) {
            DebuLog("文件不存在");
        }

        fromJsonData = SaveManager.LoadDataFromJson<JsonClass>("JsonTest");
        Debug.Log(fromJsonData.name);
        Debug.Log(fromJsonData.Age);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void DebuLog(string str) {
        Debug.Log(this.GetType().ToString() + ":" + new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().Name + ":" + str);
    }
}
