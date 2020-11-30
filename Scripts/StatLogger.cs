using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;

public class StatLogger
{

    private static volatile StatLogger instance;
    private string filename;
    private static Mutex mutex = new Mutex();

    private StatLogger()
    {

    }

    public static StatLogger GetInstance()
    {
        // Lazily loaded singleton means statlogger created when first accessed
        if (instance == null)
        {
            lock (mutex)
            {
                if (instance == null)
                {
                    instance = new StatLogger();
                }
            }
        }
        return instance;
    }

    public void SetFileName(string newName)
    {
        filename = newName;
    }

    public void WriteToFile(string text)
    {
        using (StreamWriter sw = File.AppendText(filename + ".txt"))
        {
            sw.WriteLine(text);
        }
    }
}
