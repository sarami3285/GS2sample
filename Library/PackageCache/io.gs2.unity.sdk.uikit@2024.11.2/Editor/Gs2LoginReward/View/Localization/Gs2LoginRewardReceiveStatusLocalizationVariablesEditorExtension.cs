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

using Gs2.Unity.Gs2LoginReward.ScriptableObject;
using Gs2.Unity.UiKit.Gs2LoginReward.Context;
using Gs2.Unity.UiKit.Gs2LoginReward.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2LoginReward.Localization.Editor
{
    [CustomEditor(typeof(Gs2LoginRewardReceiveStatusLocalizationVariables))]
    public class Gs2LoginRewardReceiveStatusLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2LoginRewardReceiveStatusLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2LoginRewardOwnReceiveStatusFetcher>() ?? original.GetComponentInParent<Gs2LoginRewardOwnReceiveStatusFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2LoginRewardOwnReceiveStatusFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2LoginRewardOwnReceiveStatusFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2LoginRewardOwnReceiveStatusList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2LoginRewardOwnReceiveStatusFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("ReceiveStatus is auto assign from Gs2LoginRewardOwnReceiveStatusList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2LoginRewardOwnReceiveStatusContext>() ?? original.GetComponentInParent<Gs2LoginRewardOwnReceiveStatusContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2LoginRewardOwnReceiveStatusFetcher), false);
                    EditorGUI.indentLevel++;
                    context.ReceiveStatus = EditorGUILayout.ObjectField("ReceiveStatus", context.ReceiveStatus, typeof(OwnReceiveStatus), false) as OwnReceiveStatus;
                    if (context.ReceiveStatus != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.ReceiveStatus?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("BonusModelName", context.ReceiveStatus?.BonusModelName?.ToString());
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