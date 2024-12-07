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

#if GS2_ENABLE_LOCALIZATION

using Gs2.Unity.Gs2Chat.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Chat.Context;
using Gs2.Unity.UiKit.Gs2Chat.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Chat.Localization.Editor
{
    [CustomEditor(typeof(Gs2ChatSubscribeLocalizationVariables))]
    public class Gs2ChatSubscribeLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2ChatSubscribeLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2ChatOwnSubscribeFetcher>() ?? original.GetComponentInParent<Gs2ChatOwnSubscribeFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2ChatOwnSubscribeFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2ChatOwnSubscribeFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2ChatOwnSubscribeList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ChatOwnSubscribeFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("Subscribe is auto assign from Gs2ChatOwnSubscribeList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2ChatOwnSubscribeContext>() ?? original.GetComponentInParent<Gs2ChatOwnSubscribeContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2ChatOwnSubscribeFetcher), false);
                    EditorGUI.indentLevel++;
                    context.Subscribe = EditorGUILayout.ObjectField("Subscribe", context.Subscribe, typeof(OwnSubscribe), false) as OwnSubscribe;
                    if (context.Subscribe != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.Subscribe?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("RoomName", context.Subscribe?.RoomName?.ToString());
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.indentLevel--;
                    EditorGUI.EndDisabledGroup();
                }
            }

            serializedObject.Update();
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif