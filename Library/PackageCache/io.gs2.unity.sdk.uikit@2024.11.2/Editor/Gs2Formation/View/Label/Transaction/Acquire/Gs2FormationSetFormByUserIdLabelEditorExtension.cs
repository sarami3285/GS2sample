/*
 * Copyright 2016 Game Server Services, Inc. or its affiliates. All Rights
 * Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable CheckNamespace
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantUsingDirective
// ReSharper disable Unity.NoNullPropagation
// ReSharper disable InconsistentNaming

#pragma warning disable CS0472

using Gs2.Unity.UiKit.Gs2Formation.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Formation.Label.Editor
{
    [CustomEditor(typeof(Gs2FormationSetFormByUserIdLabel))]
    public class Gs2FormationSetFormByUserIdLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2FormationSetFormByUserIdLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2FormationSetFormByUserIdFetcher>() ?? original.GetComponentInParent<Gs2FormationSetFormByUserIdFetcher>(true);
             var userDataFetcher = original.GetComponent<Gs2FormationOwnFormFetcher>() ?? original.GetComponentInParent<Gs2FormationOwnFormFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2FormationSetFormByUserIdFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2FormationSetFormByUserIdFetcher>();
                }
            }
            if (userDataFetcher == null) {
                EditorGUILayout.HelpBox("Gs2FormationOwnFormFetcher not found. Adding a Fetcher allows more values to be used.", MessageType.Warning);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2FormationOwnFormFetcher>();
                }
            }

            serializedObject.Update();
            original.format = EditorGUILayout.TextField("Format", original.format);

            GUILayout.Label("Add Format Parameter");
            if (GUILayout.Button("NamespaceName")) {
                original.format += "{namespaceName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("UserId")) {
                original.format += "{userId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("MoldModelName")) {
                original.format += "{moldModelName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Index")) {
                original.format += "{index}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Slots")) {
                original.format += "{slots}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("TimeOffsetToken")) {
                original.format += "{timeOffsetToken}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (userDataFetcher != null) {
                if (GUILayout.Button("UserData:Name")) {
                    original.format += "{userData:name}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
                if (GUILayout.Button("UserData:Index")) {
                    original.format += "{userData:index}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
                if (GUILayout.Button("UserData:Slots")) {
                    original.format += "{userData:slots}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}