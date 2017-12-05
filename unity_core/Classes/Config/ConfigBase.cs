using UnityEngine;
using System.Collections;
using System.Xml;
using System;

/// <summary>
/// 配置表基类
/// @author hannibal
/// @time 2014-12-9
/// </summary>
public abstract class ConfigBase
{
    protected string m_ConfigPathFileName;
    
    public void SetConfigPath(string pathFile)
    {
        m_ConfigPathFileName = pathFile;
    }
    //子类必须继承该加载和卸载函数
    public abstract bool LoadConfig();
    public virtual void Unload() { }

    /// <summary>
    /// 读取文本文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="handler"></param>
    /// <param name="split"></param>
    /// <returns></returns>
    protected virtual bool ReadTxtConfig(string fileName, Action<string[]> handler, string split="\n")
    {
        Log.Info("ReadTxtConfig:" + fileName);
        TextAsset textAsset = ResourceLoaderManager.Instance.LoadTextAsset(m_ConfigPathFileName + fileName);
        if (textAsset == null)
        {
            Log.Error("ConfigBase::ReadTxtConfig - load error:" + m_ConfigPathFileName + fileName);
            return false;
        }
        string[] arr_list = textAsset.text.Split(new string[] { split }, StringSplitOptions.None);
        handler(arr_list);
        textAsset = null;
        return true;
    }
    /// <summary>
    /// 读取Xml配置
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="handler"></param>
    /// <returns></returns>
    public virtual bool ReadXmlConfig(string fileName, Action<XmlDocument> handler)
    {
        Log.Info("ReadXmlConfig:" + fileName);
        TextAsset textAsset = ResourceLoaderManager.Instance.LoadTextAsset(m_ConfigPathFileName + fileName);
        if (textAsset == null)
        {
            Log.Error("ConfigBase::ReadXmlConfig - load error:" + m_ConfigPathFileName + fileName);
            return false;
        }
        else
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(textAsset.text);
            handler(xmlDoc);
            xmlDoc = null;
            ResourceLoaderManager.Instance.UnloadAsset(textAsset);
            textAsset = null;
        }
        return true;
    }
    /// <summary>
    /// 加载xml文件
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public virtual XmlDocument LoadXML(string fileName)
    {
        Log.Info("LoadXML:" + fileName);
        TextAsset textAsset = ResourceLoaderManager.Instance.LoadTextAsset(m_ConfigPathFileName + fileName);
        if (textAsset == null)
        {
            Log.Error("ConfigBase::LoadXML - load error:" + m_ConfigPathFileName + fileName);
            return null;
        }
        else
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(textAsset.text);
            ResourceLoaderManager.Instance.UnloadAsset(textAsset);
            textAsset = null;
            return xmlDoc;
        }
    }
    /// <summary>
    /// 读取CSV配置
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="handler"></param>
    /// <returns></returns>
    public virtual bool ReadCsvConfig(string fileName, Action<LoadCSVData> handler)
    {
        Log.Info("ReadCsvConfig:" + fileName);
        TextAsset textAsset = ResourceLoaderManager.Instance.LoadTextAsset(m_ConfigPathFileName + fileName);
        if (textAsset == null)
        {
            Log.Error("ConfigBase::ReadCsvConfig - load error:" + m_ConfigPathFileName + fileName);
            return false;
        }
        LoadCSVData csvDocument = new LoadCSVData();
        csvDocument.Load(textAsset.text);
        handler(csvDocument);
        csvDocument.Clear();
        ResourceLoaderManager.Instance.UnloadAsset(textAsset);
        textAsset = null;
        return true;
    }

}
