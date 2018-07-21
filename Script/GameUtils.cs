using System;
using System.IO;
using UnityEngine;

public class GameUtils
{
    /// <summary>
    /// 使用自定义Loader读取其他目录下Lua文件。通过require加载一个文件的时候，会访问每一个Loader（先访问自定的Loader后访问内置的Loader）
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static byte[] MyLoader(ref string filepath)
    {
        Debug.Log(filepath);
        string _absPath = Application.streamingAssetsPath + "/" + filepath + ".lua.txt";
        Debug.Log("绝对路径：" + _absPath);
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(_absPath));
    }
}