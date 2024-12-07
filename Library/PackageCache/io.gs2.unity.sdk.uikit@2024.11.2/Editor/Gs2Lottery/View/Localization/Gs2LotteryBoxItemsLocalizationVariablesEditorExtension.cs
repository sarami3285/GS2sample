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

using Gs2.Unity.Gs2Lottery.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Lottery.Context;
using Gs2.Unity.UiKit.Gs2Lottery.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Lottery.Localization.Editor
{
    [CustomEditor(typeof(Gs2LotteryBoxItemsLocalizationVariables))]
    public class Gs2LotteryBoxItemsLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2LotteryBoxItemsLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2LotteryOwnBoxItemsFetcher>() ?? original.GetComponentInParent<Gs2LotteryOwnBoxItemsFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2LotteryOwnBoxItemsFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2LotteryOwnBoxItemsFetcher>();
                }
            }
            else {
                if (fetcher.gameObject.GetComponentInParent<Gs2LotteryOwnBoxItemsList>(true) != null) {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2LotteryOwnBoxItemsFetcher), false);
                    EditorGUI.EndDisabledGroup();
                    EditorGUILayout.HelpBox("BoxItems is auto assign from Gs2LotteryOwnBoxItemsList.", MessageType.Info);
                }
                else {
                    var context = original.GetComponent<Gs2LotteryOwnBoxItemsContext>() ?? original.GetComponentInParent<Gs2LotteryOwnBoxItemsContext>(true);
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2LotteryOwnBoxItemsFetcher), false);
                    EditorGUI.indentLevel++;
                    context.BoxItems = EditorGUILayout.ObjectField("BoxItems", context.BoxItems, typeof(OwnBoxItems), false) as OwnBoxItems;
                    if (context.BoxItems != null) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.TextField("NamespaceName", context.BoxItems?.NamespaceName?.ToString());
                        EditorGUILayout.TextField("PrizeTableName", context.BoxItems?.PrizeTableName?.ToString());
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