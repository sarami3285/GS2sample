using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Editor.Core
{
    public class UiKitIcon
    {
        private const int WIDTH = 16;
    
        private static Texture texture;
        private static Texture errorTexture;
    
        [InitializeOnLoadMethod]
        private static void OnLoad() {
            texture = Resources.Load("gs2-logo") as Texture;
            errorTexture = Resources.Load("gs2-logo-error") as Texture;
            EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
        }
    
        private static void OnGUI( int instanceID, Rect selectionRect )
        {
            var gameObject = EditorUtility.InstanceIDToObject( instanceID ) as GameObject;
            if ( gameObject == null )
            {
                return;
            }

            bool includeUiKit = false;
            bool hasError = false;
            foreach (var component in gameObject.GetComponents<Component>())
            {
                if (component != null && component.GetType() != null && component.GetType().Module != null) {
                    if (component.GetType().Module.Name == "Gs2.Unity.UIKit.dll") {
                        var method = component.GetType().GetMethod("HasError");
                        if (method != null) {
                            if ((bool) method.Invoke(component, null)) {
                                hasError = true;
                            }
                        }
                        includeUiKit = true;
                        break;
                    }
                }
            }
            foreach (var component in gameObject.GetComponentsInChildren<Component>(true)) {
                if (component != null && component.GetType() != null && component.GetType().Module != null) {
                    if (component.GetType().Module.Name == "Gs2.Unity.UIKit.dll") {
                        var method = component.GetType().GetMethod("HasError");
                        if (method != null) {
                            if ((bool) method.Invoke(component, null)) {
                                hasError = true;
                                break;
                            }
                        }
                    }
                }
            }

            if (!hasError && !includeUiKit) {
                return;
            }
            if ( texture == null || errorTexture == null )
            {
                return;
            }
        
            var pos = selectionRect;
            pos.x = pos.xMax - WIDTH;
            pos.width = WIDTH;
        
            GUI.DrawTexture( pos, hasError ? errorTexture : texture, ScaleMode.ScaleToFit, true );
        }
    }
}
