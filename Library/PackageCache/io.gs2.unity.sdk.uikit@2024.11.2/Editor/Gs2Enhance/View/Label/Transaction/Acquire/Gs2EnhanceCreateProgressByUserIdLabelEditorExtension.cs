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

using Gs2.Unity.UiKit.Gs2Enhance.Fetcher;
using UnityEditor;
using UnityEngine;

namespace Gs2.Unity.UiKit.Gs2Enhance.Label.Editor
{
    [CustomEditor(typeof(Gs2EnhanceCreateProgressByUserIdLabel))]
    public class Gs2EnhanceCreateProgressByUserIdLabelEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2EnhanceCreateProgressByUserIdLabel;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2EnhanceCreateProgressByUserIdFetcher>() ?? original.GetComponentInParent<Gs2EnhanceCreateProgressByUserIdFetcher>(true);
             var userDataFetcher = original.GetComponent<Gs2EnhanceOwnProgressFetcher>() ?? original.GetComponentInParent<Gs2EnhanceOwnProgressFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2EnhanceCreateProgressByUserIdFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2EnhanceCreateProgressByUserIdFetcher>();
                }
            }
            if (userDataFetcher == null) {
                EditorGUILayout.HelpBox("Gs2EnhanceOwnProgressFetcher not found. Adding a Fetcher allows more values to be used.", MessageType.Warning);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2EnhanceOwnProgressFetcher>();
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
            if (GUILayout.Button("RateName")) {
                original.format += "{rateName}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("TargetItemSetId")) {
                original.format += "{targetItemSetId}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Materials")) {
                original.format += "{materials}";
                GUI.FocusControl("");
                EditorUtility.SetDirty(original);
            }
            if (GUILayout.Button("Force")) {
                original.format += "{force}";
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
                if (GUILayout.Button("UserData:RateName")) {
                    original.format += "{userData:rateName}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
                if (GUILayout.Button("UserData:PropertyId")) {
                    original.format += "{userData:propertyId}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
                if (GUILayout.Button("UserData:ExperienceValue")) {
                    original.format += "{userData:experienceValue}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
                if (GUILayout.Button("UserData:Rate")) {
                    original.format += "{userData:rate}";
                    GUI.FocusControl("");
                    EditorUtility.SetDirty(original);
                }
            }
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onUpdate"), true);
            serializedObject.ApplyModifiedProperties();
        }
    }
}