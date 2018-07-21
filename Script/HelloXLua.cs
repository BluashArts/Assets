using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;

public class HelloXLua : MonoBehaviour
{
    private LuaEnv _luaEnv;
    // Use this for initialization
    void Start()
    {
        _luaEnv = new LuaEnv();
        
        _luaEnv.DoString("CS.UnityEngine.Debug.Log('Hello XLua!')");

        //读取Lua文件的代码并运行
        //1.
        TextAsset _lua = Resources.Load<TextAsset>("HelloXLua.lua");
        _luaEnv.DoString(_lua.text);
        //2.使用xLua自带的Loader加载和执行
        _luaEnv.DoString("require 'HelloXLua'");
        //3.使用自定义Loader加载
        _luaEnv.AddLoader(GameUtils.MyLoader);
        _luaEnv.DoString("require 'DefineLoaderTest'");
    }

    private void OnDestroy()
    {
        _luaEnv.Dispose();
    }

}
