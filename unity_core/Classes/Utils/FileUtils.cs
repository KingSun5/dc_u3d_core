using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System;

/// <summary>
/// 文件+目录操作
/// @author hannibal
/// @time 2014-11-20
/// </summary>
public class FileUtils
{
	static public string MD5ByPathName(string pathName)
	{
		try
        {
            byte[] retVal;
			using(FileStream file = new FileStream(pathName, FileMode.Open))
            {
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                retVal = md5.ComputeHash(file);
            }
			
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < retVal.Length; i++)
			{
				sb.Append(retVal[i].ToString("x2"));
			}
			return sb.ToString();
		}
		catch (Exception ex)
		{
			throw new Exception("MD5ByPathName() fail,error:" + ex.Message);
		}
	}
    /// <summary>
    /// 遍历目录，获取所有文件
    /// </summary>
    /// <param name="dir">查找的目录</param>
    /// <param name="listFiles">文件列表</param>
    static public void GetDirectoryFiles(string dir_path, ref List<string> list_files)
    {
        if (!Directory.Exists(dir_path)) return;

        DirectoryInfo dir = new DirectoryInfo(dir_path);
        RecursiveDirectory(dir, dir_path + '/', ref list_files);
    }
    static private void RecursiveDirectory(DirectoryInfo dir, string parent_path, ref List<string> list_files)
    {
        FileInfo[] allFile = dir.GetFiles();
        foreach (FileInfo fi in allFile)
        {
            string ext = fi.Extension.ToLower();
            if (ext == ".meta" || ext == ".manifest" || ext == ".svn") continue;
            if ((fi.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;
            list_files.Add(parent_path + fi.Name);
        }
        DirectoryInfo[] allDir = dir.GetDirectories();
        foreach (DirectoryInfo d in allDir)
        {
            if (d.Name == "." || d.Name == "..") continue;
            if ((d.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) continue;
            RecursiveDirectory(d, parent_path + d.Name + '/', ref list_files);
        }
    }
	/// <summary>
	/// 拷贝目录
	/// </summary>
	static public void CopyXMLFolderTo(string srcDir, string dstDir)
	{
#if UNITY_WEBPLAYER
		//undo
		Log.Error("FileUtils::CopyFolderTo - cannot use in web");
#else
		if(!Directory.Exists(dstDir))
		{
			Directory.CreateDirectory(dstDir);
		}
		
		DirectoryInfo info = new DirectoryInfo(srcDir);
		FileInfo[] files = info.GetFiles();
		foreach (FileInfo file in files)
		{
			string srcFile = srcDir + file.Name;
			string dstFile = dstDir + file.Name;
			if(srcFile.Contains(".xml"))
			{
				Log.Debug("srcFile:" + srcFile + " dstFile:" + dstFile);
				File.Copy(srcFile, dstFile, true);
			}
		}
#endif
	}
    public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
    {
        // Get the subdirectories for the specified directory.
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);
        DirectoryInfo[] dirs = dir.GetDirectories();

        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException(
                "Source directory does not exist or could not be found: "
                + sourceDirName);
        }

        // If the destination directory doesn't exist, create it. 
        if (!Directory.Exists(destDirName))
        {
            Directory.CreateDirectory(destDirName);
        }

        // Get the files in the directory and copy them to the new location.
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            string temppath = Path.Combine(destDirName, file.Name);
            file.CopyTo(temppath, true);
        }

        // If copying subdirectories, copy them and their contents to new location. 
        if (copySubDirs)
        {
            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath, copySubDirs);
            }
        }
    }
    /// <summary>
    /// 拷贝目录
    /// </summary>
    /// <param name="sourceDirName"></param>
    /// <param name="destDirName"></param>
    /// <param name="copySubDirs"></param>
    public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
    {
        // Get the subdirectories for the specified directory.
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);
        DirectoryInfo[] dirs = dir.GetDirectories();

        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException(
                "Source directory does not exist or could not be found: "
                + sourceDirName);
        }

        // If the destination directory doesn't exist, create it. 
        if (!Directory.Exists(destDirName))
        {
            Directory.CreateDirectory(destDirName);
        }

        // Get the files in the directory and copy them to the new location.
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            string temppath = Path.Combine(destDirName, file.Name);
            file.CopyTo(temppath, true);
        }

        // If copying subdirectories, copy them and their contents to new location. 
        if (copySubDirs)
        {
            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath, copySubDirs);
            }
        }
    }

    /// <summary>
    /// 删除目录下所有文件
    /// </summary>
    /// <param name="srcPath"></param>
    public static void DelectDir(string srcPath)
    {
        try
        {
            DirectoryInfo dir = new DirectoryInfo(srcPath);
            FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
            foreach (FileSystemInfo i in fileinfo)
            {
                if (i is DirectoryInfo)            //判断是否文件夹
                {
                    DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                    subdir.Delete(true);          //删除子目录和文件
                }
                else
                {
                    File.Delete(i.FullName);      //删除指定文件
                }
            }
        }
        catch (Exception e)
        {
            Log.Exception(e);
        }
    }
    /// <summary>
    /// 存储二进制到文件
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="vTargetPath"></param>
    /// <returns></returns>
    public static bool StoreBufferToTargetPath(byte[] bytes, string vTargetPath)
    {
        try
        {
            string dir = Path.GetDirectoryName(vTargetPath);

            if (File.Exists(vTargetPath))
                File.Delete(vTargetPath);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            using (FileStream stream = new FileStream(vTargetPath, FileMode.Create))
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
                stream.Close();
            }
            return true;
        }
        catch(Exception e)
        {
            Log.Exception(e);
            return false;
        }
    }
}