using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using XLua;
public class CSharpCallLua:MonoBehaviour
{
    private LuaEnv _luaEnv;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _luaEnv = new LuaEnv();
        _luaEnv.AddLoader(GameUtils.MyLoader);
        _luaEnv.DoString("require 'CSharpCallLua1'");

        Debug.Log("/*获取基本变量*/");
        /*获取基本变量*/
        int intA = _luaEnv.Global.Get<int>("int_Age");
        Debug.Log(intA);
        float floB = _luaEnv.Global.Get<float>("flo_Stature");
        Debug.Log(floB);
        bool isHandsome = _luaEnv.Global.Get<bool>("isHandsome");
        Debug.Log(isHandsome);
        string name = _luaEnv.Global.Get<string>("strName");
        Debug.Log(name);

        Debug.Log("/*获取table的方式一，通过类Class(Struct) */");
        /*获取table的方式一，通过类Class(Struct) */
        Person _per = _luaEnv.Global.Get<Person>("person");
        Debug.Log(_per.name);
        Debug.Log(_per.age);

        Debug.Log("/*获取table的方式二，通过接口interface（推荐） */");
        /*获取table的方式二，通过接口interface（推荐） */
        IPerson _iPer = _luaEnv.Global.Get<IPerson>("person");
        Debug.Log(_iPer.name);
        Debug.Log(_iPer.age);
        _iPer.eat(_iPer.name,"鸡腿");

        Debug.Log("/*获取table的方式三，通过键值对Dictionary、集合List（推荐）*/");
        /*获取table的方式三，通过键值对Dictionary、集合List（推荐） */
        //Dictionary--只能映射table里面有键的元素
        Debug.Log("Dictionary>>>");
        Dictionary<string,object> _dic = _luaEnv.Global.Get<Dictionary<string,object>>("person");
        foreach(string key in _dic.Keys)
        {
            Debug.Log(key + "-" + _dic[key]);
        }

        //List--只能映射table里面没有的元素
        Debug.Log("List>>>");
        List<object> _list = _luaEnv.Global.Get<List<object>>("person");
        foreach(object item in _list)
        {
            Debug.Log(item);
        }

        /*通过LuaTable类（不推荐，性能较差） */
        Debug.Log("LuaTable>>>");
        LuaTable _table = _luaEnv.Global.Get<LuaTable>("person");
        print(_table.Get<string>("name"));
        print(_table.Get<int>("age"));
        print(_table.Length);

        Debug.Log("访问Lua中的全局函数>>>");
        /*访问Lua中的全局函数 */
        //方法一：映射到delegate
        Debug.Log("delegate>>>");
        Add _act = _luaEnv.Global.Get<Add>("add");
        int _resa,_resb;
        print(_act(1,2,out _resa,out _resb));
        print(_resa);
        print(_resb);

        //方法二：映射到LuaFunction（不推荐）
        Debug.Log("LuaFunction>>>");
        LuaFunction _lf = _luaEnv.Global.Get<LuaFunction>("add");
        object[] _os = _lf.Call(3,4);
        foreach(object item in _os)
        {
            print(item);
        }
    }
}
//Lua全局函数里面的第一个返回值会给C#自身的返回值，第二个返回值会给第一个out，第三个返回值会给第二个out
[CSharpCallLua]
delegate int Add(int a,int b,out int resa,out int reab);
[CSharpCallLua]
interface IPerson
{
    object name { get; set; }
    object age { get; set; }

    void eat(string foodName);
    void eat(IPerson iPer, string v);
    void eat(object name, string v);
}

public struct Person
{
    public object name;
    public object age;
}