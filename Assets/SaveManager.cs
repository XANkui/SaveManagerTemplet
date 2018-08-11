using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SaveManager_XAN
{
    /// <summary>
    /// 保存管理类
    /// </summary>
    public class SaveManager 
    {

#region Json 保存与读取数据

        /// <summary>
        /// 使用 Json 方式保存文件
        /// </summary>
        /// <param name="data">保存的数据对象</param>
        /// <param name="fileName">保存的文件名</param>
        /// <returns>返回文件是否保存成功，false 表示保存失败</returns>
        public static bool SaveDataByJson <T> (T data, string fileName) where T : class
        {

            // 设置保存路径
            string filePath = Application.dataPath + "/StreamingFiles/" + fileName +".json";

            // 判断文件是否已经存在
            if (File.Exists(filePath) == true) {

                Debug.Log("文件已存在");
                return false;
            }            

            // 数据转换为 Json 字符串
            string saveJsonString = JsonMapper.ToJson(data);

            // 开一个文件流、写入流写入 Json 字符串到路径文件中 ：光写写入流，保存不带指定文件夹
            // 写完关闭写入流、文件流
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fileStream);
            sw.Write(saveJsonString);
            
            sw.Close();
            fileStream.Close();

            // 文件是否保存成功，根据文件是否新建成功粗略判断
            return File.Exists(filePath);
        }

        /// <summary>
        ///  加载Json数据
        /// </summary>
        /// <param name="fileName">加载文件名称</param>
        /// <returns>返回加载结果，为 null 表示加载失败</returns>
        public static T LoadDataFromJson <T> (string fileName) where T : class
        {
            // 保存路径
            string filePath = Application.dataPath + "/StreamingFiles" + "/" + fileName + ".json";

            // 判断文件是否已经存在
            if (File.Exists(filePath) == true)
            {
                // 新建一个读取流，读取数据，然后关闭读取流
                StreamReader sr = new StreamReader(filePath);
                string loadJsonString =  sr.ReadToEnd();
                sr.Close();

                // Json 数据转位指定数据类型
                T loadJsonT = JsonMapper.ToObject<T>(loadJsonString);
                return loadJsonT;
            }
            else {

                Debug.Log("文件不存在");
                return null;
            }

        }

        #endregion


#region 二进制 保存与读取数据

        /// <summary>
        /// 使用二进制形式保存数据
        /// </summary>
        /// <typeparam name="T">泛型数据</typeparam>
        /// <param name="data">数据体</param>
        /// <param name="fileName">文件名</param>
        /// <returns>返回布尔值，false表示存储失败</returns>
        public static bool SaveDataByBinary<T>(T data, string fileName) where T : class{

            // 设置保存路径
            string filePath = Application.dataPath + "/StreamingFiles/" + fileName + ".binary";

            // 判断文件是否已经存在
            if (File.Exists(filePath) == true)
            {

                Debug.Log("文件已存在");
                return false;
            }

            // 创建一个二进制格式化体
            BinaryFormatter bf = new BinaryFormatter();

            // 打开一个文件流
            // 二进制格式化写入对应数据
            FileStream fs = new FileStream(filePath, FileMode.Create);
            bf.Serialize(fs, data);

            // 关闭文件流
            fs.Close();

            return (File.Exists(filePath));

        }

        /// <summary>
        /// 二进制 加载文件
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="fileName">文件名</param>
        /// <returns>返回得到的二进制反序列数据</returns>
        public static T LoadDataFromBinary<T >(string fileName) where T : class {

            // 设置保存路径
            string filePath = Application.dataPath + "/StreamingFiles/" + fileName + ".binary";

            // 判断文件是否已经存在
            if (File.Exists(filePath) == true)
            {
                // 创建一个二进制格式化体
                // 打开一个读文件流
                // 二进制反序列化文件流
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = File.OpenRead(filePath);
                T data = (T)bf.Deserialize(fs);

                // 关闭文件流
                fs.Close();

                return data;
            }
            else
            {

                Debug.Log("文件不存在");
                return null;
            }
        }

#endregion

    }
}
