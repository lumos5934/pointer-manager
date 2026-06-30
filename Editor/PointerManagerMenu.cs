using UnityEditor;
using UnityEngine;

namespace LLib.Editor
{
    public static class PointerManagerMenu
    {
        [MenuItem("GameObject/Input/Pointer Manager", false, 10)]
        public static void CreatePointerManager()
        {
            CreateObject<PointerManager>("PointerManager");
        }
        
        private static void CreateObject<T>(string objectName) where T : Component
        {
            GameObject audioManagerObject = new GameObject(objectName);
            audioManagerObject.AddComponent<T>();

            GameObjectUtility.EnsureUniqueNameForSibling(audioManagerObject);
            
            Undo.RegisterCreatedObjectUndo(audioManagerObject, objectName);
            Selection.activeGameObject = audioManagerObject;
        }
    }
}