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

using Gs2.Unity.Gs2Money.ScriptableObject;
using Gs2.Unity.UiKit.Gs2Money.Context;
using Gs2.Unity.UiKit.Gs2Money.Fetcher;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace Gs2.Unity.UiKit.Gs2Money.Localization.Editor
{
    [CustomEditor(typeof(Gs2MoneyWalletLocalizationVariables))]
    public class Gs2MoneyWalletLocalizationVariablesEditorExtension : UnityEditor.Editor
    {
        public override void OnInspectorGUI() {
            var original = target as Gs2MoneyWalletLocalizationVariables;

            if (original == null) return;

            var fetcher = original.GetComponent<Gs2MoneyOwnWalletFetcher>() ?? original.GetComponentInParent<Gs2MoneyOwnWalletFetcher>(true);
            if (fetcher == null) {
                EditorGUILayout.HelpBox("Gs2MoneyOwnWalletFetcher not found.", MessageType.Error);
                if (GUILayout.Button("Add Fetcher")) {
                    original.gameObject.AddComponent<Gs2MoneyOwnWalletFetcher>();
                }
            }
            else {
                var context = original.GetComponent<Gs2MoneyOwnWalletContext>() ?? original.GetComponentInParent<Gs2MoneyOwnWalletContext>(true);
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.ObjectField("Fetcher", fetcher.gameObject, typeof(Gs2MoneyOwnWalletFetcher), false);
                EditorGUI.indentLevel++;
                context.Wallet = EditorGUILayout.ObjectField("Wallet", context.Wallet, typeof(OwnWallet), false) as OwnWallet;
                if (context.Wallet != null) {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.TextField("NamespaceName", context.Wallet?.NamespaceName?.ToString());
                    EditorGUILayout.TextField("Slot", context.Wallet?.Slot.ToString());
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
            }

            serializedObject.Update();
            original.target = EditorGUILayout.ObjectField("Target", original.target, typeof(LocalizeStringEvent), true) as LocalizeStringEvent;

            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif