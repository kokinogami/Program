using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public partial class GameManager
{
    [HideInInspector] public SaveData data;

    static string filepath;
    string fileName = "Data.json";

    static bool firstStartGame = false;
    bool LoadGame = false;
    // Start is called before the first frame update
    void SaveAwake()
    {

        // パス名取得
        filepath = Application.dataPath + "/" + fileName;

        // ファイルがないとき、ファイル作成
        if (!File.Exists(filepath))
        {
            Save(data);
        }

        // ファイルを読み込んでdataに格納
        data = Load(filepath);

        LoadGame = true;

        if (firstStartGame == false)
        {
            LodeDataReflection();//GameManagerに受け渡し

            firstStartGame = true;
        }
    }
    void chack()
    {
        Debug.Log(stage1Clear);
    }
    // Update is called once per frame
    void LodeDataReflection()
    {
        tutorial1Clear = data.tutorial1Clear;
        tutorial2Clear = data.tutorial2Clear;
        stage1Clear = data.stage1Clear;
        stage2Clear = data.stage2Clear;
        stage3Clear = data.stage3Clear;
        BossClear = data.BossClear;

        tutorialUnlock = data.tutorialUnlock;
        MainmapUnlock = data.MainmapUnlock;
        isStage2Unlock = data.isStage2Unlock;
        isStage3Unlock = data.isStage3Unlock;
        isBossUnlock = data.isBossUnlock;
    }

    void SaveDataReflection()
    {
        data.tutorial1Clear = tutorial1Clear;
        data.tutorial2Clear = tutorial2Clear;
        data.stage1Clear = stage1Clear;
        data.stage2Clear = stage2Clear;
        data.stage3Clear = stage3Clear;
        data.BossClear = BossClear;

        data.tutorialUnlock = tutorialUnlock;
        data.MainmapUnlock = MainmapUnlock;
        data.isStage2Unlock = isStage2Unlock;
        data.isStage3Unlock = isStage3Unlock;
        data.isBossUnlock = isBossUnlock;
    }
    public void SaveDataReset()
    {
        tutorial1Clear = false;
        tutorial2Clear = false;
        stage1Clear = false;
        stage2Clear = false;
        stage3Clear = false;
        BossClear = false;


        tutorialUnlock = false;
        MainmapUnlock = false;
        isStage2Unlock = false;
        isStage3Unlock = false;
        isBossUnlock = false;

        data.tutorial1Clear = false;
        data.tutorial2Clear = false;
        data.stage1Clear = false;
        data.stage2Clear = false;
        data.stage3Clear = false;
        data.BossClear = false;

        data.tutorialUnlock = false;
        data.MainmapUnlock = false;
        data.isStage2Unlock = false;
        data.isStage3Unlock = false;
        data.isBossUnlock = false;

        data.t1Time = new float[5];
        data.t2Time = new float[5];
        data.s1Time = new float[5];
        data.s2Time = new float[5];
        data.s3Time = new float[5];
        data.BossTime = new float[5];

        NewGameSave();
    }
    void Save(SaveData data)
    {
        if (LoadGame) SaveDataReflection();//GameManagerからSaveDataに受け渡し

        string json = JsonUtility.ToJson(data);                 // jsonとして変換
        StreamWriter wr = new StreamWriter(filepath, false);    // ファイル書き込み指定
        wr.WriteLine(json);                                     // json変換した情報を書き込み
        wr.Close();                                             // ファイル閉じる
    }

    void NewGameSave()
    {
        string json = JsonUtility.ToJson(data);                 // jsonとして変換
        StreamWriter wr = new StreamWriter(filepath, false);    // ファイル書き込み指定
        wr.WriteLine(json);                                     // json変換した情報を書き込み
        wr.Close();                                             // ファイル閉じる
    }

    SaveData Load(string path)
    {
        StreamReader rd = new StreamReader(path);               // ファイル読み込み指定
        string json = rd.ReadToEnd();                           // ファイル内容全て読み込む
        rd.Close();                                             // ファイル閉じる

        return JsonUtility.FromJson<SaveData>(json);
    }
}
[System.Serializable]
public class SaveData
{
    public bool tutorial1Clear = false;
    public bool tutorial2Clear = false;
    public bool stage1Clear = false;
    public bool stage2Clear = false;
    public bool stage3Clear = false;
    public bool BossClear = false;

    public bool tutorialUnlock = false;
    public bool MainmapUnlock = false;
    public bool isStage2Unlock = false;
    public bool isStage3Unlock = false;
    public bool isBossUnlock = false;

    public float[] t1Time = new float[5];
    public float[] t2Time = new float[5];
    public float[] s1Time = new float[5];
    public float[] s2Time = new float[5];
    public float[] s3Time = new float[5];
    public float[] BossTime = new float[5];
}
