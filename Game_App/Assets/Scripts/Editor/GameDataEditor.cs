using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class GameDataEditor : EditorWindow
{
    private string gameDataProjectFilePath = "/StreamingAssets/data.json";
    public GameData gameData;

    [MenuItem("Window/Game Data Editor")]
    static void init(){
        GameDataEditor window = (GameDataEditor)EditorWindow.GetWindow(typeof(GameDataEditor));
        window.Show();
    }

    void OnGUI(){
            
        if(gameData != null){

            SerializedObject serializedObject =  new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("gameData");

            EditorGUILayout.PropertyField(serializedProperty,true);
            serializedObject.ApplyModifiedProperties();

            if(GUILayout.Button("Save Data")){
                SaveData();
            }
            
        }
        if(GUILayout.Button("Load Data")){
                LoadGameData();
            }


    }

    private void LoadGameData(){

        string filePath = Application.dataPath + gameDataProjectFilePath;

        if(File.Exists(filePath)){

            string dataAsJson = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(dataAsJson);
        }
        else{
            gameData = new GameData();
        }
    }

    private void SaveData(){

        string dataAsJson = JsonUtility.ToJson(gameData);
        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath,dataAsJson);
    }
}
