using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConfigMaster {
    private static ConfigMaster master;
    private Config[] configurations;
    private int length;

    private ConfigMaster() {
        configurations = new Config[10];
        length = 0;
    }

    public static ConfigMaster WakeUp() {
        if (master == null) master = new ConfigMaster(); 
        return master;
}
    public static void Add(int[,] config) {
        string name = "" + (char) (97 + master.length);
        Config newConfig = new Config(config, name);
        master.configurations[master.length] = newConfig;
        master.length++;
    }

    public static int Length() {
        return master.length;
    }

    public static int[,] Get(int index) {
        return master.configurations[index].toArray();
    }

    public static int Get(int index, int i, int j) {
        return master.configurations[index].Get(i, j);
    }
}

class Config {
    
    private int[,] config;
    private string cName;

    public Config() {
        config = new int[4,5];
        cName = "noName";
    }

    public Config(int[,] c, string name) {
        config = (int[,])c.Clone();
        cName = name;
    }

    
    public int Get(int i, int j) {
        return config[i, j];
    }
    /*
    public string GetString(int i, int j) {
        string data = config[i, j].ToString();
        if (data.Length < 2) data = "0" + data;
        return data;
    }
    */

    public int[,] toArray() {
        return (int[,])config.Clone();
    }

    public string GetName() {
        return (string)cName.Clone();
    }
}


